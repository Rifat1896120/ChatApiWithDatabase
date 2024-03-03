using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ChatApi.Data;
using ChatApi;
using ChatApi.Hubs;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ChatApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ChatApiContext") ?? throw new InvalidOperationException("Connection string 'ChatApiContext' not found.")));

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapchatMamberEndpoints();

app.MapfriendChatModelEndpoints();

app.MapFriendModelEndpoints();

app.MapRequestsEndpoints();
app.MapHub<ChatHub>("/chathub");
app.MapaccountInformationEndpoints();

app.Run();
