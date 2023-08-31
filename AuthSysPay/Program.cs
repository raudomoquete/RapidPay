using AuthSysPay.Core;
using AuthSysPay.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AuthSysPayContext>(x => x.UseSqlServer("name=AuthSysPayConnection"));

builder.Services.AddIdentity<User, IdentityRole>(cfg =>
{
    cfg.SignIn.RequireConfirmedEmail = false;
    //cfg.Password.RequireDigit = false;
    //cfg.Password.RequiredUniqueChars = 0;
    //cfg.Password.RequireNonAlphanumeric = false;
    //cfg.Password.RequireLowercase = false;
    //cfg.Password.RequireUppercase = false;
}).AddDefaultTokenProviders()
  .AddEntityFrameworkStores<AuthSysPayContext>();

//Dependencies
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
