namespace ImageResizer.Features.Image
{
    public interface IImageServices
    {
        IResult ArquivoValido(IFormFile file);
        Task<IResult> SalvarArquivoLocal(IFormFile file, IWebHostEnvironment env);
    }
}
