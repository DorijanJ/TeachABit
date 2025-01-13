using Amazon.S3;
using Amazon.S3.Transfer;

namespace TeachABit.Service.Util.S3
{
    public class S3BucketService(IAmazonS3 s3Client, string bucketName) : IS3BucketService
    {
        private readonly IAmazonS3 _s3Client = s3Client;
        private readonly string _bucketName = bucketName;

        public async Task<string> UploadImageAsync(string name, MemoryStream image)
        {
            var fileTransferUtility = new TransferUtility(_s3Client);

            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = image,
                BucketName = _bucketName,
                Key = name,
            };

            uploadRequest.Headers.CacheControl = "public, max-age=31536000, immutable";

            await fileTransferUtility.UploadAsync(uploadRequest);


            return $"https://{_bucketName}.s3.amazonaws.com/{name}";
        }
    }
}
