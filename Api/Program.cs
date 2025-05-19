
using CasosUso.DTOs;
using LogicaAccesoDatos.EF;
using LogicaAccesoDatos.Repositorios;
using LogicaAplicacion;
using LogicaAplicacion.CasosUso;
using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<JwtService>(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                return new JwtService(config["Jwt:Key"]);
            });

            builder.Services
                   .AddHttpContextAccessor()
                   .AddScoped<ICurrentUser, CurrentUser>();

            builder.Services.AddScoped<IAgenciaService, AgenciaService>();
            builder.Services.AddScoped<IRepositorioAgencia, RepositorioAgenciaBD>();
            builder.Services.AddScoped<IRepositorioAuditoria, RepositorioAuditoriaBD>();
            builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuarioBD>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<ILoginService,LoginService>();

            builder.Services.AddScoped<IRepositorioEnvio, RepositorioEnvioBD>();
            builder.Services.AddScoped<IEnvioService, EnvioService>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string connectionString = builder.Configuration.GetConnectionString("Agencia");
            builder.Services.AddDbContext<AgenciaContext>(
                options => options.UseSqlServer(connectionString)
                );

            var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });


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
        }
    }
}
