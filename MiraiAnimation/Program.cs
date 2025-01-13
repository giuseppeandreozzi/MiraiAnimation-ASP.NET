using Mailjet.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;
using MongoDB.Driver;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment()) {
	builder.Configuration.AddUserSecrets<Program>();
}

StripeConfiguration.ApiKey = builder.Configuration["SK_STRIPE"];

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
        options.LoginPath = "/";
    });

builder.Services.AddAuthorization(options => {
	options.AddPolicy("AdminRole", policy => {
		policy.RequireClaim("tipo", "admin");
	});
});

builder.Services.AddHttpClient<IMailjetClient, MailjetClient>(client => {
    client.SetDefaultSettings();

    client.UseBasicAuthentication(builder.Configuration["MJ_APIKEY_PUBLIC"], builder.Configuration["MJ_APIKEY_PRIVATE"]);
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

app.MapPost("/infoAnimazione", (IFormCollection formCollection, IDbService<Animation, string> animCollection) => {
    Animation _anim = animCollection.GetById(formCollection["codiceAnimazione"]);

    return new {
        anim = _anim,
        id = _anim.id.ToString()
    };
}).DisableAntiforgery(); // solo momentaneamente

app.MapPost("/infoBluRay", (IFormCollection formCollection, IDbService<BluRay, string> bluRayCollection) => {
    BluRay _bd = bluRayCollection.GetById(formCollection["id"]);

    return _bd;
}).DisableAntiforgery(); // solo momentaneamente

app.MapPost("/infoStaff", (IFormCollection formCollection, IDbService<Staff, string> staffCollection) => {
    Staff _staff = staffCollection.GetById(formCollection["id"]);

    return _staff;
}).DisableAntiforgery(); // solo momentaneamente

app.MapPost("/infoUser", (IFormCollection formCollection, IDbService<User, string> userCollection) => {
	User _user = userCollection.GetById(formCollection["id"]);

	return _user;
}).DisableAntiforgery(); // solo momentaneamente

app.Run();