using cts_twister_api.baseproject;
using cts_twister_api.common;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// 1. Fetch the string array from appsettings.json
var allowedOrigins = builder.Configuration
    .GetSection("CorsSettings:AllowedOrigins")
    .Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins, policy =>
    {
        if (allowedOrigins != null && allowedOrigins.Length > 0)
        {
            policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod();
        }
    });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseStaticFiles();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.InjectJavascript("/swagger-search.js");
        c.EnableFilter();
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        c.DisplayRequestDuration();
        c.EnableDeepLinking();
    });
}

var userArray = builder.Configuration.GetSection("UserSettings:user").Get<string[]>();
var passwordArray = builder.Configuration.GetSection("UserSettings:password").Get<string[]>();

string user = SecurityHelper.Decrypt(userArray[0], userArray[1]);
string pass = SecurityHelper.Decrypt(passwordArray[0], passwordArray[1]);

var conStringSplit = app.Configuration.GetConnectionString("conDB_TwisterSystem").Split(";");
var conString = $"{conStringSplit[0]};{conStringSplit[1]};{conStringSplit[2]}{user};{conStringSplit[3]}{pass};{conStringSplit[4]}";

app.RegisterEndpointDefinitions(conString);
app.Run();

