using FluxStore.Application.Interfaces;
using FluxStore.Infrastructure.Services;
using FluxStore.Infrastructure.Auth;
using FluxStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

// Add database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Load JWT settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()!;
builder.Services.AddSingleton(jwtSettings);

var mailSmtpSettings = builder.Configuration.GetSection("SmtpSettings").Get<SmtpSettings>()!;
builder.Services.AddSingleton(mailSmtpSettings);

// Dependency Injection
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Add Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure static files with custom MIME types
var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".apple-app-site-association"] = "application/json"; // Required for iOS

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider,
    ServeUnknownFileTypes = true, // Allow extensionless files
    DefaultContentType = "application/json"
});


// Enable Middleware
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


//dotnet ef migrations add <migration-name> --startup-project ../FluxStore.API
//dotnet ef database update --startup-project ../FluxStore.API