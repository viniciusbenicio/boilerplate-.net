﻿using BoilerplateCleanArch.Application.DTOS.Email;
using BoilerplateCleanArch.Application.Interfaces.Email;
using BoilerplateCleanArch.Application.Interfaces.ITokenService;
using BoilerplateCleanArch.Application.Interfaces.IUserService;
using BoilerplateCleanArch.Application.Mappings;
using BoilerplateCleanArch.Application.Services.Email;
using BoilerplateCleanArch.Application.Services.TokenService;
using BoilerplateCleanArch.Application.Services.UserService;
using BoilerplateCleanArch.Domain.Interfaces.IUserRepository;
using BoilerplateCleanArch.Domain.Interfaces.IUserRepository.ITokenRepository;
using BoilerplateCleanArch.Infra.Data.Context;
using BoilerplateCleanArch.Infra.Data.Repositories.TokenRepository;
using BoilerplateCleanArch.Infra.Data.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BoilerplateCleanArch.Infra.IoC
{
    public static class DependencyInjectionAPI
    {
        public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<ITokenService, TokenService>();


            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));



            var myhandlers = AppDomain.CurrentDomain.Load("BoilerplateCleanArch.Application");
            //services.AddMediatR(myhandlers);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(myhandlers));


            //Configuração para Envio de e-mail
            var configuracaoSmtp = new ConfigurationDTO.SmtpConfiguracao();
            configuration.GetSection("Smtp").Bind(configuracaoSmtp);
            ConfigurationDTO.Smtp = configuracaoSmtp;


            return services;
        }
    }
}
