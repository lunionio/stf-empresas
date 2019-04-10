﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WpEmpresas.Domains;
using WpEmpresas.Infraestructure;
using WpEmpresas.Services;

namespace WpEmpresas
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddTransient<EmpresaDomain>(); 
            services.AddTransient<EnderecoDomain>();
            services.AddTransient<ContatoDomain>();
            services.AddTransient<EmpresaRepository>();
            services.AddTransient<EnderecoRepository>();
            services.AddTransient<ContatoRepository>(); 
            services.AddTransient<SegurancaService>();
            services.AddTransient<TelefoneDomain>();
            services.AddTransient<TelefoneRepository>(); 
            services.AddTransient<TipoEmpresasDomain>();
            services.AddTransient<TipoEmpresaRepository>();
            services.AddTransient<ResponsavelDomain>(); 
            services.AddTransient<ResponsavelRepository>(); 
            services.AddTransient<EmpresaXEspecialidadesDomain>(); 
            services.AddTransient<EmpresaXEspecialidadeRepository>();
            services.AddTransient<EspecialidadeRepository>();
            services.AddTransient<EspecialidadesDomain>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
