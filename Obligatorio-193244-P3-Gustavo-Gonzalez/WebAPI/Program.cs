
using CasosDeUso.InterfacesCU.CUTipoGasto;
using CasosDeUso.InterfacesCU.ICUAuditoria;
using CasosDeUso.InterfacesCU.ICUEquipo;
using CasosDeUso.InterfacesCU.ICUListadoMetodoPago;
using CasosDeUso.InterfacesCU.ICUMetodoPago;
using CasosDeUso.InterfacesCU.ICUPago;
using CasosDeUso.InterfacesCU.ICURol;
using CasosDeUso.InterfacesCU.ICUTipoGasto;
using CasosDeUso.InterfacesCU.ICUUsuario;
using LogicaAccesoDatos;
using LogicaAccesoDatos.Repositorios;
using LogicaAplicacion.CasosUso.CUAuditoria;
using LogicaAplicacion.CasosUso.CUEquipo;
using LogicaAplicacion.CasosUso.CUMetodoPago;
using LogicaAplicacion.CasosUso.CUPago;
using LogicaAplicacion.CasosUso.CURol;
using LogicaAplicacion.CasosUso.CUTipoGasto;
using LogicaAplicacion.CasosUso.CUUsuario;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            


            var builder = WebApplication.CreateBuilder(args);

            //INVERSION DE CONTROL DE TIPO GASTO
            builder.Services.AddScoped<IRepositorioTipoGastos, RepositorioTipoGastoEF>();
            builder.Services.AddScoped<ICUAltaTipoGasto, CUAltaTipoGasto>();
            builder.Services.AddScoped<ICUListadoTipoGasto, CUListadoTipoGasto>();
            builder.Services.AddScoped<ICUBuscarTipoGasto, CUBuscarTipoGasto>();
            builder.Services.AddScoped<ICUEliminarTipoGasto, CUEliminarTipoGasto>();
            builder.Services.AddScoped<ICUEditarTipoGasto, CUEditarTipoGasto>();
            builder.Services.AddScoped<ICUUsuarioLogin, CUUsuarioLogin>();



            //INVERSION DE CONTROL DE USUARIO
            builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
            builder.Services.AddScoped<ICUAltaUsuario, CUAltaUsuario>();
            builder.Services.AddScoped<ICUListadoUsuarios, CUListadoUsuario>();
            builder.Services.AddScoped<ICUBuscarUsuarioPorId, CUBuscarUsuarioPorId>();
            builder.Services.AddScoped<ICUUsuarioLogin, CUUsuarioLogin>();
            builder.Services.AddScoped<ICUCambiarContrasena, CUCambiarContrasena>();


            //INVERSION DE CONTROL DE ROL
            builder.Services.AddScoped<IRepositorioRol, RepositorioRol>();
            builder.Services.AddScoped<ICUAltaRol, CUAltaRol>();
            builder.Services.AddScoped<ICUListadoRolParaLogin, CUListadoRolParaLogin>();
            builder.Services.AddScoped<ICUListarRol, CUListarRolParaAltaUsuario>();

            //INVERSION DE CONTROL DE PAGO
            builder.Services.AddScoped<IRepositorioPago, RepositorioPago>();
            builder.Services.AddScoped<ICUAltaPagoUnico, CUAltaPagoUnico>();
            builder.Services.AddScoped<ICUAltaPagoRecurrente, CUAltaPagoRecurrente>();
            builder.Services.AddScoped<ICUListadoPago, CUListadoPago>();
            builder.Services.AddScoped<ICUFiltrarPagosXFecha, CUFiltarPagosPorFecha>();
            builder.Services.AddScoped<ICUBuscarPagoPorValorMayorA, CUBuscarPagoPorValorMayorA>();
            builder.Services.AddScoped<ICUBuscarPagoPorId, CUBuscarPagoPorId>();
            builder.Services.AddScoped<ICUListarPagosPorUsuario, CUListarPagosPorUsuario>();
            builder.Services.AddScoped<ICUListaEquiposConPagosUnicosMayorA, CUEquiposConPagosUnicosMayorA>();


            //INVERSION DE CONTROL DE METODOPAGO
            builder.Services.AddScoped<IRepositorioMetodoPago, RepositorioMetodoPago>();
            builder.Services.AddScoped<ICUAltaMetodoPago, CUAltaMetodoPago>();
            builder.Services.AddScoped<ICUListadoMetodoPago, CUListadoMetodoPago>();
            builder.Services.AddScoped<ICUBucarMetodoPago, CUBucarMetodoPago>();


            //INVERSION DE CONTROL DE: AUDITORIA
            builder.Services.AddScoped<IRepositorioAuditoria, RepositorioAuditoria>();
            builder.Services.AddScoped<ICUAltaAuditoria, CUAltaAuditoria>();
            builder.Services.AddScoped<ICUBuscarAuditoriaPorIdTipoGasto, CUBuscarAuditoriaPorIdTipoGasto>();


            //INVERSION DE CONTROL DE: EQUIPO
            builder.Services.AddScoped<IRepositorioEquipo, RepositorioEquipo>();
            builder.Services.AddScoped<ICUAltaEquipo, CUAltaEquipo>();
            builder.Services.AddScoped<ICUListarEquipo, CUListarEquipo>();





            //CADENA DE CONEXION
            string cadenaConexion = builder.Configuration.GetConnectionString("CadenaConexion");
            builder.Services.AddDbContext<EmpresaContexto>(option => option.UseSqlServer(cadenaConexion));


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt=>opt.IncludeXmlComments("WebAPI.xml"));


            ////Comienza JWT////
            var claveSecreta = "ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=";

            builder.Services.AddAuthentication(aut =>
            {
                aut.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                aut.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(aut =>
            {
                aut.RequireHttpsMetadata = false;
                aut.SaveToken = true;
                aut.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(claveSecreta)),
                    ValidateIssuer = false,
                    ValidateAudience = false
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
