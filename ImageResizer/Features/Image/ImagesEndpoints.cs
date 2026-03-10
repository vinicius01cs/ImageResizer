using System.Runtime.CompilerServices;

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
        }
    }
}
