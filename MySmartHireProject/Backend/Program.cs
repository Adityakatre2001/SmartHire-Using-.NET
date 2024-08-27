using SmartHire.Services;
using SmartHire.Repositories; 
using Microsoft.EntityFrameworkCore;
using SmartHire.Models; 

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IUserService, UserService>();  
builder.Services.AddScoped<IUserRepository, UserRepository>();  
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();  


// Register ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 31))));

// Register AutoMapper services
builder.Services.AddAutoMapper(typeof(Program));

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add CORS policy to allow all origins, methods, and headers
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Enable Swagger in development
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartHire API V1");
    });
}

// Enable CORS middleware before routing and endpoints
app.UseCors("AllowAll");

app.UseRouting();

app.UseAuthorization();

// Map controllers to the pipeline
app.MapControllers();

app.Run();


//*********************************************************************

