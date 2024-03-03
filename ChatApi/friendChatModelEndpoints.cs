using Microsoft.EntityFrameworkCore;
using ChatApi.Data;
using ChatApi.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace ChatApi;

public static class friendChatModelEndpoints
{
    public static void MapfriendChatModelEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/friendChatModel").WithTags(nameof(friendChatModel));

        group.MapGet("/", async (ChatApiContext db) =>
        {
            return await db.friendChatModel.ToListAsync();
        })
        .WithName("GetAllfriendChatModels")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<friendChatModel>, NotFound>> (int id, ChatApiContext db) =>
        {
            return await db.friendChatModel.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is friendChatModel model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetfriendChatModelById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, friendChatModel friendChatModel, ChatApiContext db) =>
        {
            var affected = await db.friendChatModel
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, friendChatModel.Id)
                    .SetProperty(m => m.ownerusername, friendChatModel.ownerusername)
                    .SetProperty(m => m.username, friendChatModel.username)
                    .SetProperty(m => m.images, friendChatModel.images)
                    .SetProperty(m => m.sendusername, friendChatModel.sendusername)
                    .SetProperty(m => m.chat, friendChatModel.chat)
                    .SetProperty(m => m.seentext, friendChatModel.seentext)
                    .SetProperty(m => m.time, friendChatModel.time)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdatefriendChatModel")
        .WithOpenApi();

        group.MapPost("/", async (friendChatModel friendChatModel, ChatApiContext db) =>
        {
            db.friendChatModel.Add(friendChatModel);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/friendChatModel/{friendChatModel.Id}",friendChatModel);
        })
        .WithName("CreatefriendChatModel")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, ChatApiContext db) =>
        {
            var affected = await db.friendChatModel
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeletefriendChatModel")
        .WithOpenApi();
    }
}
