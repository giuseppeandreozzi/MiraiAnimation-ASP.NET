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
builder.Services.AddScoped<IDbService<Animation>, AnimationService>();
builder.Services.AddScoped<IDbService<Staff>, StaffService>();
builder.Services.AddScoped<IDbService<BluRay>, BluRayService>();

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