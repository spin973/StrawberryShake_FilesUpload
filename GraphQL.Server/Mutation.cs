using Path = System.IO.Path;

namespace GraphQL.Server;

public sealed class Mutation
{
    public const string ImageDirectory = "./wwwroot/images";

    public async Task<string> UploadSingleImage(IFile file, CancellationToken cancellationToken)
    {
        try
        {
            using var stream = File.Create(Path.Combine(ImageDirectory, $"single.jpg"));
            await file.CopyToAsync(stream, cancellationToken);

            return "ok";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return "ko";
    }

    public async Task<string> UploadMultipleImages(List<IFile> files, CancellationToken cancellationToken)
    {
        try
        {
            int index = 0;
            foreach (var file in files)
            {
                using var stream = File.Create(Path.Combine(ImageDirectory, $"multiple-{index}.jpg"));
                await file.CopyToAsync(stream, cancellationToken);
                index++;
            }

            return "ok";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return "ko";
    }
}
