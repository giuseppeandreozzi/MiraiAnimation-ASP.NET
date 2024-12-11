using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment()) {
	builder.Configuration.AddUserSecrets<Program>();
}

var settings = MongoClientSettings.FromConnectionString(builder.Configuration["DB_URI"]);
settings.ServerApi = new ServerApi(ServerApiVersion.V1);
var mongoClient = new MongoClient(settings);
var db = mongoClient.GetDatabase(builder.Configuration["DB_NAME"]);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton(db);
builder.Services.AddScoped<IDbService<Animation, string>, AnimationService>();
builder.Services.AddScoped<IDbService<Staff, string>, StaffService>();
builder.Services.AddScoped<IDbService<BluRay,string>, BluRayService>();
builder.Services.AddScoped<IDbService<User, string>, UserService>();
builder.Services.AddScoped<PasswordHasher<User>>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.AccessDeniedPath = "/Forbidden";
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();