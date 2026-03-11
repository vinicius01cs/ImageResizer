using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ImageResizer.Features.Image
{
    public class ImagesServices : IImageServices
    {
        public IResult ArquivoValido(IFormFile file)
        {
            if (file is null || file.Length == 0)
            {
                return Results.BadRequest("File not selected or is empty.");
            }

            var allowedTypes = new[] { "image/jpeg", "image/png" };

            if (!allowedTypes.Contains(file.ContentType))
            {
                return Results.BadRequest("Invalid file type. Only JPEG or PNG are allowed.");
            }

            const long maxSize = 5 * 1024 * 1024; // 5MB
            if(file.Length > maxSize)
            {
                return Results.BadRequest("File size exceeds the 5MB limit.");
            }

            return Results.Ok();
        }

        public async Task<IResult> SalvarArquivoLocal(IFormFile file, [FromServices] IWebHostEnvironment env)
        {
            try
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var uploadPath = Path.Combine("wwwroot", "images", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(uploadPath));

                await using var stream = File.Create(uploadPath);

                await file.CopyToAsync(stream);
                return Results.Ok(new {FileName = fileName, Size = file.Length});
            }
            catch (Exception ex)
            {
                return Results.Problem("An error occurred while saving the file.");
            }
        }
    }
}
