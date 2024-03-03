using Microsoft.EntityFrameworkCore;
using ChatApi.Data;
using ChatApi.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace ChatApi;

public static class RequestsEndpoints
{
    public static void MapRequestsEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Requests").WithTags(nameof(Requests));

        group.MapGet("/", async (ChatApiContext db) =>
        {
            return await db.Requests.ToListAsync();
        })
        .WithName("GetAllRequests")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Requests>, NotFound>> (int id, ChatApiContext db) =>
        {
            return await db.Requests.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Requests model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetRequestsById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Requests requests, ChatApiContext db) =>
        {
            var affected = await db.Requests
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, requests.Id)
                    .SetProperty(m => m.ownerusername, requests.ownerusername)
                    .SetProperty(m => m.username, requests.username)
                    .SetProperty(m => m.time, requests.time)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateRequests")
        .WithOpenApi();

        group.MapPost("/", async (Requests requests, ChatApiContext db) =>
        {
            db.Requests.Add(requests);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Requests/{requests.Id}",requests);
        })
        .WithName("CreateRequests")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, ChatApiContext db) =>
        {
            var affected = await db.Requests
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteRequests")
        .WithOpenApi();
    }
}
