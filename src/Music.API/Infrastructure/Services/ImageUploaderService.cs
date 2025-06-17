using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Transfer;
using Music.API.Domain.Interfaces;
using Music.API.Models;

namespace Music.API.Infrastructure.Services
{
    public class ImageUploaderService : IImageUploaderService
    {

        private readonly IAmazonS3 _s3Client;
        private readonly AwsSettings _awsSettings;

        public ImageUploaderService(AwsSettings awsSettings)
        {
            _awsSettings = awsSettings;
            _s3Client = new AmazonS3Client(awsSettings.AWS_ACCESS_KEY, awsSettings.AWS_SECRET_KEY, Amazon.RegionEndpoint.GetBySystemName(awsSettings.AWS_REGION));
        }

        public async Task<string> UploadAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Image file is empty");
            }

            var fileTransferUtility = new TransferUtility(_s3Client);

            using (var stream = imageFile.OpenReadStream())
            {
                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = stream,
                    Key = $"{Guid.NewGuid()}_{imageFile.FileName}",
                    BucketName = _awsSettings.AWS_BUCKET_NAME,
                    CannedACL = S3CannedACL.NoACL
                };
                await fileTransferUtility.UploadAsync(uploadRequest);
                return $"https://{_awsSettings.AWS_BUCKET_NAME}.s3.us-east-1.amazonaws.com/{uploadRequest.Key}";
            }
        }
    }
}