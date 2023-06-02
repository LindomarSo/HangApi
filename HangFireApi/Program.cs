using Hangfire;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfire(options =>
{
    options.UseSqlServerStorage(builder.Configuration.GetConnectionString("DbContext"));
});

builder.Services.AddHangfireServer();

builder.Services.AddSingleton<HangFireApi.Service.Action>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resouces")),
    RequestPath = "/Resouces"
});

app.MapControllers();

app.Run();
