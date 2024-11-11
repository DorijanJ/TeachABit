namespace TeachABit.Service.Util.S3
{
    public interface IS3BucketService
    {
        Task<string> UploadImageAsync(string name, MemoryStream image);
    }
}
