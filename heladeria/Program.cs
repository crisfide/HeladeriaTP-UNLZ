using heladeria.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using heladeria.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddScoped<ProductoRepository>(
        _ => new ProductoRepository(builder.Configuration["Db:ConnectionString"]));

builder.Services.AddScoped<UsuarioRepository>(
        _ => new UsuarioRepository(builder.Configuration["Db:ConnectionString"]));

builder.Services.AddScoped<PedidoRepository>(
        _ => new PedidoRepository(builder.Configuration["Db:ConnectionString"]));

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    options.ClientId = builder.Configuration.GetSection("GoogleKeys:ClientId").Value;
    options.ClientSecret = builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;

    options.Events.OnCreatingTicket = ctx =>
    {
        var usuarioServicio = ctx.HttpContext.RequestServices.GetRequiredService<UsuarioRepository>();

        string googleNameIdentifier = ctx.Identity.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value.ToString(); ;

        var usuario = usuarioServicio.ObtenerPorGoogle(googleNameIdentifier);
        int idUsuario = 0;
        if (usuario == null)
        {
            usuario = new Usuario();
            //usuarioNuevo.Nombre = ctx.Identity.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname").Value.ToString();
            usuario.NombreUsuario = ctx.Identity.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value.ToString();
            usuario.GoogleIdentificador = googleNameIdentifier;
            usuario.MailUsuario = ctx.Identity.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value.ToString();

            usuarioServicio.Agregar(usuario);

            idUsuario = usuarioServicio.ObtenerPorGoogle(googleNameIdentifier).IdUsuario;
        }
        else
        {
            idUsuario = usuario.IdUsuario;
        }
        //ctx.Identity.
        //   usuarioServicio.GetUsuarioPorGoogleSubject(ctx.Identity.Claims)
        // Agregar reclamaciones personalizadas aquí
        ctx.Identity.AddClaim(new System.Security.Claims.Claim("usuario", idUsuario.ToString()));

        //la contraseña es grupo666
        string rolUsuario = usuario.MailUsuario == "heladeria.unlz.2024@gmail.com" ? "Administrador" : "Cliente";
        ctx.Identity.AddClaim(new System.Security.Claims.Claim("UNLZRole", rolUsuario));

        var accessToken = ctx.AccessToken;
        ctx.Identity.AddClaim(new System.Security.Claims.Claim("accessToken", accessToken));


        return Task.CompletedTask;
    };

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
