using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core;
using Talabat.Core.Repositories.Contract;
using Talabat.Domain.Repositories.Contract;
using Talabat.Infrastructure.BasketRepository;
using Talabat.Infrastructure.GenericRepository;
using Talabat.Repository;
using Talabat.Repository.GenericRepository;
namespace Talabat.Apis.Extensions
{
    public static class ApplicationServicesExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddAutoMapper(typeof(MappingProfiles));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                // Customize the response for invalid model state
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0)
                                    .SelectMany(P => P.Value!.Errors)
                                    .Select(E => E.ErrorMessage)
                                    .ToArray();

                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            services.AddScoped<IBasketRepository, BasketRepository>();

            return services;
        }
    }
}
