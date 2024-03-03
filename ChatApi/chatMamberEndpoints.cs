using Microsoft.EntityFrameworkCore;
using ChatApi.Data;
using ChatApi.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace ChatApi;

public static class chatMamberEndpoints
{
    public static void MapchatMamberEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/chatMamber").WithTags(nameof(chatMamber));

        group.MapGet("/", async (ChatApiContext db) =>
        {
            return await db.chatMamber.ToListAsync();
        })
        .WithName("GetAllchatMambers")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<chatMamber>, NotFound>> (string username, ChatApiContext db) =>
        {
            return await db.chatMamber.AsNoTracking()
                .FirstOrDefaultAsync(model => model.username == username)
                is chatMamber model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetchatMamberById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (string username, chatMamber chatMamber, ChatApiContext db) =>
        {
            var affected = await db.chatMamber
                .Where(model => model.username == username)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.username, chatMamber.username)
                    .SetProperty(m => m.password, chatMamber.password)
                    .SetProperty(m => m.email, chatMamber.email)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdatechatMamber")
        .WithOpenApi();

        group.MapPost("/", async (chatMamber chatMamber, ChatApiContext db) =>
        {
            db.chatMamber.Add(chatMamber);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/chatMamber/{chatMamber.username}",chatMamber);
        })
        .WithName("CreatechatMamber")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (string username, ChatApiContext db) =>
        {
            var affected = await db.chatMamber
                .Where(model => model.username == username)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeletechatMamber")
        .WithOpenApi();
    }
}
