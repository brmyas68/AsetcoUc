

using System.Reflection;
using System.Text;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Annex.DataLayer.Contex;
using UC.DataLayer.Contex;
using UC.InterfaceService.InterfacesBase;
using UC.Service.ServiceBase;
using Annex.InterfaceService.InterfacesBase;
using Annex.Service.ServiceBase;

var builder = WebApplication.CreateBuilder(args);



var SQlUC = builder.Configuration.GetConnectionString("AppDbUC");
builder.Services.AddDbContext<ContextUC>(x => x.UseSqlServer(SQlUC,
    sqlServerOptionsAction: sqlOptions => { sqlOptions.EnableRetryOnFailure(); }).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));


var SQlAnnex = builder.Configuration.GetConnectionString("AppDbAnnex");
builder.Services.AddDbContext<ContextAnnex>(x => x.UseSqlServer(SQlAnnex,
    sqlServerOptionsAction: sqlOptions => { sqlOptions.EnableRetryOnFailure(); }).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());



// Add services to the container.

builder.Services.AddTransient<IUnitOfWorkUCService, UnitOfWorkUCService>();
builder.Services.AddTransient<IUnitOfWorkAnnexService, UnitOfWorkAnnexService>();




builder.Services.AddControllers();



builder.Services.AddEndpointsApiExplorer();





builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder
           //.SetIsOriginAllowed( _ => true)
           //.AllowAnyOrigin()
           .WithOrigins("http://localhost:3000", "https://www.asetcoyadak.com", "ftp://107.181.112.145", "http://107.181.112.145", "https://uc.asetcoyadak.com", "https://api.asetcoyadak.com", "https://asetcoyadak.com", "https://god-asetco.netlify.app", "https://asetco.ir")
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials()
    .Build());
});


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "AsetCo Web API" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization ",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"

                            },
                        },
                        new String[]{}
                    }
                });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "AsetCo Web API");

    });

}


app.UseDeveloperExceptionPage();

app.UseCors("AllowOrigin");



app.UseHttpsRedirection();

app.MapControllers();


app.UseAuthentication();
app.UseAuthorization();




app.Run();



