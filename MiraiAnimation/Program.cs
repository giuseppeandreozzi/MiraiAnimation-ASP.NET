using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment()) {
	builder.Configuration.AddUserSecrets<Program>();
}

var mongoClient = new MongoClient(builder.Configuration["DB_URI"]);
var dbContextOptions = new DbContextOptionsBuilder<MyDbContext>().UseMongoDB(mongoClient, builder.Configuration["DB_NAME"]);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton(new MyDbContext(dbContextOptions.Options));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();