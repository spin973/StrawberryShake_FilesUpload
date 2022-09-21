using Microsoft.AspNetCore.Components.Forms;
using StrawberryShake;
using System;

namespace GraphQL.Client.Pages
{
    public partial class Index
    {
        public string? ResponseSingle { get; set; }
        public string? ResponseMultiple { get; set; }

        private async Task UploadSingleImageAsync(InputFileChangeEventArgs args)
        {
            try
            {
                await using var stream = args.File.OpenReadStream();
                var profilePicture = new Upload(stream, args.File.Name);
                var input = new UploadSingleImageInput
                {
                    File = profilePicture,
                };

                var result = await GraphQLClient.UploadSingleImage.ExecuteAsync(input);
                if (result.IsSuccessResult())
                {
                    ResponseSingle = result.Data?.UploadSingleImage.String;
                }
            }
            catch (Exception ex)
            {
                ResponseSingle = ex.Message;
                Console.WriteLine(ex.Message);
            }
        }

        private async Task UploadMultipleImagesAsync(InputFileChangeEventArgs args)
        {
            try
            {
                List<Upload> files = new();
                foreach (var file in args.GetMultipleFiles())
                {
                    using var stream = file.OpenReadStream();
                    files.Add(new Upload(stream, file.Name));
                }
            
                var input = new UploadMultipleImagesInput
                {
                    Files = files,
                };

                var result = await GraphQLClient.UploadMultipleImages.ExecuteAsync(input);
                if (result.IsSuccessResult())
                {
                    ResponseMultiple = result.Data?.UploadMultipleImages.String;
                }
            }
            catch (Exception ex)
            {
                ResponseMultiple = ex.Message;
                Console.WriteLine(ex.Message);
            }
        }
    }
}
