using Core;
using Core.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.EntityFrameworkCore.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LinkGamer.Core;
using Dommel;
using Dapper.FluentMap.Dommel.Resolvers;
using LinkGamer.Configuration;
using System.Data;
using Newtonsoft.Json;
using System;

namespace LinkGamer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //ConfigurationBuilder builder = new ConfigurationBuilder();
            //builder.AddJsonFile("cnets.json", optional: false, reloadOnChange: true);
            //Configuration = builder.Build();
            DommelMapper.SetTableNameResolver(new DommelTableNameResolver());
            Configuration = configuration;
            StaticConfig = configuration;
        }

        public IConfiguration Configuration { get; }
        public static IConfiguration StaticConfig { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
       
            // Inclui os repositórios específicos 
            DependencyInjection.AddDependencies(ref services);

            //Connection DB
            var connectionString = Environment.GetEnvironmentVariable("LinkGamerDB");
            //services.AddDbContext<LinkGamerContext>(options => options.UseMySQL(connectionString));
            // Mantem case sensitive das propriedade do objeto no json de retorno
            services.AddControllers().AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                })
                // Habilita a possibilidade de passar o Accept XML no Header (transforma retorno em XML)
                .AddXmlSerializerFormatters();

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });


            //JWT 

            //AppSettings appSetup = new AppSettings();
            //var key = appSetup.Secret;
            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("Token").Value);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            // Inicia as conexões dos core's no controller chamado
            services.AddTransient<CreateCoreConnectionsActionFilter>();
            services.AddMvc(options =>
            {
                options.Filters.AddService<CreateCoreConnectionsActionFilter>();
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
