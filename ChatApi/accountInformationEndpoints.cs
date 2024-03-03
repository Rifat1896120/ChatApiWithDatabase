using Microsoft.EntityFrameworkCore;
using ChatApi.Data;
using ChatApi.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace ChatApi;

public static class accountInformationEndpoints
{
    public static void MapaccountInformationEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/accountInformation").WithTags(nameof(accountInformation));

        group.MapGet("/", async (ChatApiContext db) =>
        {
            return await db.accountInformation.ToListAsync();
        })
        .WithName("GetAllaccountInformations")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<accountInformation>, NotFound>> (string username, ChatApiContext db) =>
        {
            return await db.accountInformation.AsNoTracking()
                .FirstOrDefaultAsync(model => model.username == username)
                is accountInformation model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetaccountInformationById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (string username, accountInformation accountInformation, ChatApiContext db) =>
        {
            var affected = await db.accountInformation
                .Where(model => model.username == username)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.username, accountInformation.username)
                    .SetProperty(m => m.name, accountInformation.name)
                    .SetProperty(m => m.highlighttext, accountInformation.highlighttext)
                    .SetProperty(m => m.aboutme, accountInformation.aboutme)
                    .SetProperty(m => m.contactnum, accountInformation.contactnum)
                    .SetProperty(m => m.currEdu, accountInformation.currEdu)
                    .SetProperty(m => m.address, accountInformation.address)
                    .SetProperty(m => m.facebooklink, accountInformation.facebooklink)
                    .SetProperty(m => m.instragramlink, accountInformation.instragramlink)
                    .SetProperty(m => m.linkdinlink, accountInformation.linkdinlink)
                    .SetProperty(m => m.githublink, accountInformation.githublink)
                    .SetProperty(m => m.youtubelink, accountInformation.youtubelink)
                    .SetProperty(m => m.whatsappnum, accountInformation.whatsappnum)
                    .SetProperty(m => m.tiktoklink, accountInformation.tiktoklink)
                    .SetProperty(m => m.redditlink, accountInformation.redditlink)
                    .SetProperty(m => m.snapchartlink, accountInformation.snapchartlink)
                    .SetProperty(m => m.twitterlink, accountInformation.twitterlink)
                    .SetProperty(m => m.pinterestlink, accountInformation.pinterestlink)
                    .SetProperty(m => m.websitelink, accountInformation.websitelink)
                    .SetProperty(m => m.website2link, accountInformation.website2link)
                    .SetProperty(m => m.website3linl, accountInformation.website3linl)
                    .SetProperty(m => m.nationality, accountInformation.nationality)
                    .SetProperty(m => m.isactive, accountInformation.isactive)
                    .SetProperty(m => m.base64Image, accountInformation.base64Image)
                    .SetProperty(m => m.base64ImageHigh, accountInformation.base64ImageHigh)
                    .SetProperty(m => m.connectionId, accountInformation.connectionId)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateaccountInformation")
        .WithOpenApi();

        group.MapPost("/", async (accountInformation accountInformation, ChatApiContext db) =>
        {
            db.accountInformation.Add(accountInformation);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/accountInformation/{accountInformation.username}",accountInformation);
        })
        .WithName("CreateaccountInformation")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (string username, ChatApiContext db) =>
        {
            var affected = await db.accountInformation
                .Where(model => model.username == username)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteaccountInformation")
        .WithOpenApi();
    }
}
