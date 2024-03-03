using Microsoft.EntityFrameworkCore;
using ChatApi.Data;
using ChatApi.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace ChatApi;

public static class FriendModelEndpoints
{
    public static void MapFriendModelEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/FriendModel").WithTags(nameof(FriendModel));

        group.MapGet("/", async (ChatApiContext db) =>
        {
            return await db.FriendModel.ToListAsync();
        })
        .WithName("GetAllFriendModels")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<FriendModel>, NotFound>> (int id, ChatApiContext db) =>
        {
            return await db.FriendModel.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is FriendModel model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetFriendModelById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, FriendModel friendModel, ChatApiContext db) =>
        {
            var affected = await db.FriendModel
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, friendModel.Id)
                    .SetProperty(m => m.username, friendModel.username)
                    .SetProperty(m => m.ownerusername, friendModel.ownerusername)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateFriendModel")
        .WithOpenApi();

        group.MapPost("/", async (FriendModel friendModel, ChatApiContext db) =>
        {
            db.FriendModel.Add(friendModel);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/FriendModel/{friendModel.Id}",friendModel);
        })
        .WithName("CreateFriendModel")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, ChatApiContext db) =>
        {
            var affected = await db.FriendModel
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteFriendModel")
        .WithOpenApi();
    }
}
