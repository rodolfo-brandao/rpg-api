using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Rpg.Application.Util;
using Rpg.Core.Enums;
using Rpg.Data.Context;
using System.Reflection;
using System.Text;

namespace Rpg.Api.Extensions
{
    internal static class ServicesConfiguration
    {
        #region Private Fields
        private const string ApiVersioningFormat = "'v'VVV";
        private const string AuthorizationSchemeName = "Bearer";
        private const string BearerFormat = "JWT";
        private const int MajorApiVersion = 1;
        private const int MinorApiVersion = 0;
        #endregion

        #region Public Methods
        public static IServiceCollection ConfigureApiVersioning(this IServiceCollection services)
        {
            return services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(MajorApiVersion, MinorApiVersion);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            }).AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = ApiVersioningFormat;
                options.SubstituteApiVersionInUrl = true;
            });
        }

        public static IServiceCollection ConfigureAuthorizationPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSecret = configuration.GetSection("Jwt:Secret").Value;
            var jwtSecretBytes = Encoding.ASCII.GetBytes(jwtSecret);
            var adminRoleString = Role.Admin.ToString();
            var userRoleString = Role.User.ToString();

            _ = services.AddAuthorization(options =>
            {
                options.AddPolicy(adminRoleString, policy => policy.RequireClaim("Player", adminRoleString));
                options.AddPolicy(userRoleString, policy => policy.RequireClaim("Player", userRoleString));
            }).AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtSecretBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }

        public static IApplicationBuilder ConfigureCors(this IApplicationBuilder app)
        {
            return app.UseCors(options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });
        }

        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            return services.AddDbContext<RpgContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
        }

        public static IMvcBuilder ConfigureMvc(this IServiceCollection services)
        {
            return services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        public static IServiceCollection ConfigureRouting(this IServiceCollection services)
        {
            return services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSwaggerGen(options =>
            {
                var openApiInfo = GetOpenApiInfo(configuration);

                options.SwaggerDoc(ApiVersions.V1, openApiInfo);
                options.AddSecurityDefinition(AuthorizationSchemeName, new OpenApiSecurityScheme
                {
                    Description = $"{BearerFormat} authorization header using the {AuthorizationSchemeName} scheme. \r\n\r\n To authenticate, simply enter '{AuthorizationSchemeName} <your_access_token>'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = AuthorizationSchemeName,
                    BearerFormat = BearerFormat,
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = AuthorizationSchemeName,
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        public static IApplicationBuilder ConfigureSwaggerUse(this IApplicationBuilder app)
        {
            return app
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.DefaultModelsExpandDepth(-1);
                });
        }
        #endregion

        #region Private Methods
        private static OpenApiInfo GetOpenApiInfo(IConfiguration configuration)
        {
            var title = configuration.GetSection("ApiInfo:Name").Value;
            var contactName = configuration.GetSection("ApiInfo:Contact:Name").Value;
            var contactUrl = new Uri(configuration.GetSection("ApiInfo:Contact:Url").Value);
            var licenceName = configuration.GetSection("ApiInfo:Licence:Name").Value;
            var licenceUrl = new Uri(configuration.GetSection("ApiInfo:Licence:Url").Value);

            return new OpenApiInfo
            {
                Version = ApiVersions.V1,
                Title = title,
                Contact = new OpenApiContact
                {
                    Name = contactName,
                    Url = contactUrl
                },
                License = new OpenApiLicense
                {
                    Name = licenceName,
                    Url = licenceUrl
                }
            };
        }
        #endregion
    }
}
