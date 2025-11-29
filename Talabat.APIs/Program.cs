using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.Apis.Extensions;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middlewares;
using Talabat.Core.Repositories.Contract;
using Talabat.Infrastructure.GenericRepository;
using Talabat.Repository.Data;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services - Register Services At DI Container 

            builder.Services.AddControllers();

            builder.Services.AddSwaggerServices();

            builder.Services.AddDbContext<StoreContext>(storeContextOptions =>
            {
                storeContextOptions.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
                
            builder.Services.AddApplicationServices();

            #endregion



            var app = builder.Build();

            #region Update Database  & Data Seeding 

            // 1. Create Scope & Dispose It 
            using var scope = app.Services.CreateScope();

            // 2. Get the Service Provider from the Scope
            var services = scope.ServiceProvider;

            // 3. Get the DbContext from the Service Provider
            var _dbContext = services.GetRequiredService<StoreContext>();

            // 4. Get Looger 

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            // 4. Apply Migrations and Update the Database
            try
            {
                await _dbContext.Database.MigrateAsync(); // Apply Migrations  - Update Database
                await StoreContextSeed.SeedAsync(_dbContext);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred during apply the migration");
            }

            #endregion

            #region Configure kesteral - Http Pipline - middlewares

            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleware();

                //app.UseDeveloperExceptionPage(); // by default called automaticlyy .Net 6 + 
            }

            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            #endregion

            app.Run(); 
        }
    }
}
