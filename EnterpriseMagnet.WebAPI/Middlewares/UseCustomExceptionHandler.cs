using EnterpriseMagnet.Dto.Concrete;
using EnterpriseMagnet.Service.Concrete.Exceptions;
using EnterpriseMagnet.Service.Concrete.Mapping;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace EnterpriseMagnet.WebAPI.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exceptionFeature.Error switch
                    {
                    ClientSideException => 400,
                    NotFoundException => 404,
                    _ => 500
                    };

                    context.Response.StatusCode = statusCode;

                    var response = CustomResponseDto<NoContentDto>.Fail(context.Response.StatusCode, exceptionFeature.Error.Message);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));

                });

            });


        }
    }
}
