
namespace RPGCalendar.Api
{
    using System;
    using System.IO;
    using AutoMapper;
    using Core;
    using Core.Models;
    using Core.Repositories;
    using Core.Services;
    using Data;
    using Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.AspNetCore.SpaServices.AngularCli;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get;}
        public RpgCalendarSettings AppSettings { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Env = env;
            Configuration = configuration;
            AppSettings = GetRpgCalendarSettings();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddTransient<IEmailService, EmailService>();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AuthorizeFilter(
                    new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build()));
            }).AddNewtonsoftJson();
            services.AddSwaggerDocument();

            if (Env.IsProduction())
            {
                services.AddDbContext<ApplicationDbContext>();
                services.AddDbContext<AuthenticationDbContext>();
            }
            else
            {
                services.AddDbContext<ApplicationDbContext, SqliteDbContext>();
                services.AddDbContext<AuthenticationDbContext, SqliteAuthenticationDbContext>();
            }

            services
                .AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<AuthenticationDbContext>();


            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;

                // Token settings.
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
            });

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(2);
            });

            services.Configure<RpgCalendarSettings>(Configuration.GetSection(nameof(RpgCalendarSettings)));

            services.AddTransient<IAuthenticationService, AuthenticationService>();

            services.AddTransient<ICalendarRepository, CalendarRepository>()
                .AddTransient<IEventRepository, EventRepository>()
                .AddTransient<IGameRepository, GameRepository>()
                .AddTransient<IItemRepository, ItemRepository>()
                .AddTransient<INoteRepository, NoteRepository>()
                .AddTransient<INotificationRepository, NotificationRepository>()
                .AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IEventService, EventService>()
                .AddTransient<IGameService, GameService>()
                .AddTransient<IItemService, ItemService>()
                .AddTransient<INoteService, NoteService>()
                .AddTransient<INotificationService, NotificationService>()
                .AddTransient<ISessionService, SessionService>()
                .AddTransient<ITimeService, TimeService>()
                .AddTransient<IUserService, UserService>();


            services.AddAutoMapper(new[] { typeof(AutomapperConfigurationProfile).Assembly });
            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.Cookie.SameSite = SameSiteMode.None;
                options.LoginPath = "/";
                options.SlidingExpiration = true;
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            services.AddSession(options =>
                {
                    options.Cookie.Name = $".{AppSettings.ApplicationName!}.Session";
                    options.IdleTimeout = TimeSpan.FromHours(1);
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                    options.Cookie.SameSite = SameSiteMode.None;
                }

            );
            services.AddHttpContextAccessor();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseOpenApi(x => x.PostProcess = (doc, _) =>
            {
                doc.Info.Title = $"{AppSettings.ApplicationName} API ({env.EnvironmentName})";
            });

            app.UseSwaggerUi3();

            app.UseCors();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

        }

        private RpgCalendarSettings GetRpgCalendarSettings()
        {
            var appSettings = new RpgCalendarSettings();
            Configuration.GetSection(nameof(RpgCalendarSettings)).Bind(appSettings);
            return appSettings;
        }

    }
}
