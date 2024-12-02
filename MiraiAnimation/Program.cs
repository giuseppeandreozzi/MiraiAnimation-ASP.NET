using Microsoft.EntityFrameworkCore;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment()) {
	builder.Configuration.AddUserSecrets<Program>();
}

var mongoClient = new MongoClient(builder.Configuration["DB_URI"]);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<MyDbContext>(options => {
    options.UseMongoDB(mongoClient, builder.Configuration["DB_NAME"]);
});
builder.Services.AddScoped<IDbService<Animation>, AnimationService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();