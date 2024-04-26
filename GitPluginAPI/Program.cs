
using GitPlugin.Business;
using GitPlugin.Core.Business;
using GitPlugin.Core.Repository;
using GitPlugin.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();

var app = builder.Build();

app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();


/*
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mon API", Version = "v1" });

    // Configuration OAuth2 pour Swagger
    c.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("https://gitlab.com/oauth/authorize"),
                TokenUrl = new Uri("https://gitlab.com/oauth/token"),
                Scopes = new Dictionary<string, string>
                {
                    { "read_user", "Accès en lecture aux informations de l'utilisateur" },
                    { "api", "Accès à l'API" }
                }
            }
        }
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "OAuth2"
                },
                Scheme = "oauth2",
                Name = "OAuth2",
                In = ParameterLocation.Header,
            },
            new List<string> { "read_user", "api" }
        }
    });
});

builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();

var app = builder.Build();


app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mon API V1");
        c.OAuthClientId("d6d1923ce449f69d817a5d8c34d20b5d28683d2b9f6306712c561808a1297e78");
        c.OAuthAppName("TestGitplugin");
        c.OAuthScopeSeparator(" ");
        c.OAuth2RedirectUrl("http://localhost:5299/swagger/oauth2-redirect.html");
        c.OAuthClientSecret("gloas-429757005065800aa593fad006065c4b94a84bc9f5177c4c7e3e2ebbe69b6f2f");
        
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
*/