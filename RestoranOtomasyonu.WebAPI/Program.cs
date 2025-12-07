using Microsoft.AspNetCore.SignalR;
using RestoranOtomasyonu.WebAPI.Hubs;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS - Tüm origin'lere izin ver (geliştirme için)
// SignalR için Credentials gerekli olduğundan, AllowAnyOrigin yerine WithOrigins kullanıyoruz
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.SetIsOriginAllowed(_ => true) // Tüm origin'lere izin ver
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // SignalR için gerekli
    });
});

// SignalR
builder.Services.AddSignalR();

// Connection String (appsettings.json'dan al)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Data source=(localdb)\\MSSQLLocalDB;Initial Catalog=Restoran;Integrated Security=true";
builder.Services.AddSingleton(connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS
app.UseCors("AllowAll");

// Static Files (QR kod sayfaları için)
app.UseStaticFiles();

// Routing
app.UseRouting();

app.UseAuthorization();

// QR kod sayfası route'u - /masa/{id}
app.MapGet("/masa/{id}", async (int id) =>
{
    var filePath = Path.Combine(app.Environment.WebRootPath ?? "", "masa", "index.html");
    if (System.IO.File.Exists(filePath))
    {
        var html = await System.IO.File.ReadAllTextAsync(filePath);
        // Masa ID'sini JavaScript'e aktar
        html = html.Replace("getMasaIdFromPath()", $"function getMasaIdFromPath() {{ return {id}; }}");
        return Results.Content(html, "text/html");
    }
    return Results.NotFound("Sayfa bulunamadı");
});

app.MapControllers();

// SignalR Hub
app.MapHub<SiparisHub>("/siparisHub");

// Health check endpoint (SignalR bağlantısını test etmek için)
app.MapGet("/health", () => Results.Ok(new { status = "OK", timestamp = System.DateTime.Now }));

// Database connection test endpoint
app.MapGet("/test-db", async (IConfiguration configuration) =>
{
    try
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? "Data source=(localdb)\\MSSQLLocalDB;Initial Catalog=Restoran;Integrated Security=true";
        
        using (var connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("SELECT COUNT(*) FROM Kullanicilar", connection))
            {
                var count = await cmd.ExecuteScalarAsync();
                return Results.Ok(new { 
                    status = "OK", 
                    message = "Veritabanı bağlantısı başarılı",
                    kullaniciSayisi = count,
                    connectionString = connectionString.Replace("Password=.*;", "Password=***;") // Güvenlik için parolayı gizle
                });
            }
        }
    }
    catch (Exception ex)
    {
        return Results.Problem($"Veritabanı bağlantı hatası: {ex.Message}");
    }
});

// Port'u sabitle (opsiyonel - development için)
// app.Urls.Add("http://localhost:5146");

app.Run();
