using Proyecto2PDS.DTOs;
using Proyecto2PDS.DAOs;
using Proyecto2PDS.Services;
using Proyecto2PDS.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<CursosContext>(options => {
    var connectionString = builder.Configuration.GetConnectionString("CursosDB")??"";
    options.UseMySQL(connectionString);
}, ServiceLifetime.Scoped);

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".CursosUIMono.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<CursoDTO>();
builder.Services.AddScoped<ProfesorDTO>();
builder.Services.AddScoped<ParticipanteDTO>();
builder.Services.AddScoped<RespuestaPeticionDTO>();
builder.Services.AddScoped<RespuestaValidacionUsuarioDTO>();
builder.Services.AddScoped<PeticionInicioSesionDTO>();

builder.Services.AddScoped<CursosDAO>();
builder.Services.AddScoped<ProfesoresDAO>();
builder.Services.AddScoped<ParticipantesDAO>();
builder.Services.AddScoped<CursosImagenesDAO>();
builder.Services.AddScoped<UsuariosDAO>();
builder.Services.AddScoped<UsuarioSesionesDAO>();
builder.Services.AddScoped<UsuarioAccionesDAO>();

builder.Services.AddScoped<CursosService>();
builder.Services.AddScoped<ProfesoresService>();
builder.Services.AddScoped<ParticipantesService>();
builder.Services.AddScoped<IdentidadService>();
builder.Services.AddScoped<SesionesService>();
builder.Services.AddScoped<BitacoraService>();
builder.Services.AddScoped<GeneradorTokensJWTService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();
app.UseSession();   

app.Run();
