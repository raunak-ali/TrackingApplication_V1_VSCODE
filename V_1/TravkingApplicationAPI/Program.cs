using Microsoft.EntityFrameworkCore;
using TravkingApplicationAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TravkingApplicationAPI.Interfaces;
using TravkingApplicationAPI.Repository;
using TravkingApplicationAPI.Services;
using OfficeOpenXml;


var builder = WebApplication.CreateBuilder(args);
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<TrackingApplicationDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // System.Text.Json configuration
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});

// Configure Newtonsoft.Json separately


builder.Services.AddCors(p => p.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod()));
 builder.Services.Configure<IISServerOptions>(options =>
    {
        options.MaxRequestBodySize = int.MaxValue; // Set maximum request body size to the maximum value
    });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(
    options=>
    {options.TokenValidationParameters=new TokenValidationParameters
    {
        ValidateIssuer=true,//Vlidates the server
        ValidateAudience=true,//Validates the 
        ValidateLifetime=true,
        ValidIssuer=builder.Configuration["Jwt:Issuer"],
        ValidAudience=builder.Configuration["Jwt:Audience"],
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    

    }
);
builder.Services.AddScoped<IUser,UserRepo>();
builder.Services.AddScoped<UserService,UserService>();

builder.Services.AddScoped<IBatch,BatchRepo>();
builder.Services.AddScoped<BatchService,BatchService>();

builder.Services.AddScoped<ITask,TaskRepo>();
builder.Services.AddScoped<TaskServices,TaskServices>();

builder.Services.AddScoped<ITaskSubmissions,TaskSubmissionsRepo>();
builder.Services.AddScoped<TaskSubmissionService,TaskSubmissionService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 100_000_000;
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();//For Authentication
// Configure Kestrel server options to adjust maximum request body size

app.UseRouting();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseCors();
    app.UseHttpsRedirection();


app.MapControllers();

app.Run();

