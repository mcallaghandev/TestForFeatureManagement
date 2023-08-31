
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using TestForFeatureManagement.TargetingFilters;

namespace TestForFeatureManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddAzureAppConfiguration(options =>
                {
                    options.Connect("Endpoint=https://uks-mws-app-feature-flag-config.azconfig.io;Id=xG8y;Secret=alELqkR+1brE/VLAOWw2Z2jkiC2MmfK1NebZFyBDdrk=")
                           .UseFeatureFlags(featureFlagOptions => {
                               featureFlagOptions.CacheExpirationInterval = TimeSpan.FromSeconds(5);
                           });
                }).Build();

            builder.Services.AddSingleton<IConfiguration>(configuration)
                .AddFeatureManagement();

            /*builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddSingleton<ITargetingContextAccessor, TestTargetingContextAccessor>();

            builder.Services.AddSingleton<IConfiguration>(configuration)
                .AddFeatureManagement()
                .AddFeatureFilter<TargetingFilter>();*/

            builder.Services.AddAzureAppConfiguration();

            // Add services to the container.
            builder.Services.AddRazorComponents();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAzureAppConfiguration();

            app.MapRazorComponents<App>();

            app.Run();
        }
    }
}
