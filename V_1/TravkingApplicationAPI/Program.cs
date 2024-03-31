using Microsoft.EntityFrameworkCore;
using TravkingApplicationAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TravkingApplicationAPI.Interfaces;
using TravkingApplicationAPI.Repository;
using TravkingApplicationAPI.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<TrackingApplicationDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddCors(p => p.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod()));


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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();//For Authentication
    app.UseAuthorization();

app.UseHttpsRedirection();

app.UseCors();

app.MapControllers();

app.Run();

