using nextbit.Databases;
using MongoDB.Bson.Serialization.Conventions;
using nextbit.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Logging;
using RestSharp;
using Newtonsoft.Json;

var pack = new ConventionPack();
pack.Add(new IgnoreExtraElementsConvention(true));
ConventionRegistry.Register("Ignore Extra Conventions", pack, t => true);

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers()
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddSwaggerGen(c =>
{
    //c.EnableAnnotations();
    c.UseAllOfForInheritance();

    //c.SchemaFilter<SchemaDescriptionFilter>();
    //c.ParameterFilter<ParameterDescriptionFilter>();

    c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        [new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "bearer",
            }
        }] = new List<string>(),
    });

    c.TagActionsBy(api =>
    {
        var action = api.ActionDescriptor;
        if (action is ControllerActionDescriptor controllerAction)
        {
            var names = controllerAction.ControllerTypeInfo.FullName
                .Split(".")
                .ToList();
            var index = names.IndexOf("Controllers");
            var result = names.Skip(index + 1)
                .Select(a => a.Replace("Controller", string.Empty));
            var name = string.Join("/", result);

            return new List<string>()
            {
                name,
            };
        }

        return new List<string>()
        {
            action.DisplayName,
        };
    });


    var regex = new Regex("(.+)`1\\[\\[(.+)\\]\\]");
    c.CustomSchemaIds(type =>
    {
        var result = type.FullName?.Replace("+", ".") ?? string.Empty;
        if (regex.IsMatch(result))
        {
            result = regex.Replace(result, (m) => $"{m.Groups[1]}<{m.Groups[2].Value.Split(",").First()}>");
        }

        return result;
    });

    //c.OperationFilter<ReApplyOptionalRouteParameterOperationFilter>();

    c.MapType<TimeSpan>(() => new OpenApiSchema
    {
        Type = "string",
        Example = new OpenApiString("00:00:00")
    });
});

builder.Services
    .AddAuthorization(options =>
    {
        options.DefaultPolicy = new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();
    });

builder.Services
    .AddAuthentication(
        //options =>
        //{
        //    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
        //}
        )
    .AddCookie()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = issuer,
            ValidAudience = audience,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration[$"Jwt:SecretKey"])),
            NameClaimType = JwtRegisteredClaimNames.Sid,
        };

        //builder.Services.AddMvc();
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<MongoContext>();
builder.Services.AddCustomServices();
builder.Services.AddSwaggerGen();

builder.Services.AddRouting();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    IdentityModelEventSource.ShowPII = true;
}

var options = new StaticFileOptions()
{
    ServeUnknownFileTypes = true,
};
app.UseStaticFiles(options);
app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

////=============================================
//// upbit
//var payload = new JwtPayload
//        {
//            { "access_key", configuration["Upbit:AccessKey"] },
//            { "nonce", Guid.NewGuid().ToString() },
//            //{ "query_hash", queryHash },
//            { "query_hash_alg", "SHA512" }
//        };

////byte[] keyBytes = Encoding.Default.GetBytes("발급받은 Secret Key");
//byte[] keyBytes = Encoding.Default.GetBytes(configuration["Upbit:SecretKey"]);
//var securityKey = new SymmetricSecurityKey(keyBytes);
//var credentials = new SigningCredentials(securityKey, "HS256");
//var header = new JwtHeader(credentials);
//var secToken = new JwtSecurityToken(header, payload);

//var jwtToken = new JwtSecurityTokenHandler().WriteToken(secToken);
//var authorizationToken = "Bearer " + jwtToken;
//var client = new RestClient("https://api.upbit.com/v1/accounts");
//var request = new RestRequest();
//request.AddHeader("Accept", "application/json");
//request.AddHeader("Authorization", authorizationToken);
//RestResponse response = client.Execute(request);
//var data = JsonConvert.DeserializeObject<List<nextbit.Models.Upbit.AccountData>>(response.Content);

//Console.WriteLine("=================================================");
//Console.WriteLine(data);
//Console.WriteLine("=================================================");
////=============================================


app.Run();


