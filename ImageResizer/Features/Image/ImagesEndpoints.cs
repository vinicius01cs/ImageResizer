using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using ImageResizer.Features.Image;

namespace ImageResizer.Features.Image
{
    public static class ImagesEndpoints
    {
        public static void MapImages(this WebApplication app)
        {

            app.MapGet("/images", () =>
            {
                return "1+1 = 3";
            });
            app.MapPost("/images", async Task<IResult> (IFormFile file, IImageServices service) =>
            {
                var result = service.ArquivoValido(file);
                if(result is not Ok)
                {
                    return result;
                }

                return await service.SalvarArquivoLocal(file, app.Environment);
            }).DisableAntiforgery();
        }
    }
}
