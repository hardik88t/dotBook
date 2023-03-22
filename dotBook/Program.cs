using dotBook.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using System.Security.Cryptography.X509Certificates;
using System.IO;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<DotDBcontext>(options => options.UseInMemoryDatabase("dotBookDB"));
builder.Services.AddDbContext<DotDBcontext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dotBookDBcs")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


//.UseKestrel(options =>
// {
//     options.ListenAnyIP(5000, listenOptions =>
//     {
//         listenOptions.UseHttps(new HttpsConnectionAdapterOptions
//         {
//             ServerCertificate = new X509Certificate2(Path.Combine("Certificates", "localhost.pfx"), "password")
//         });
//     });
// })
