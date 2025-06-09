using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Transfer;
using Producer.API.Domain.Interfaces;
using Producer.API.Models;

namespace Producer.API.Infrastructure.Services
{
    public class ImageStorageService : IImageStorageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly AwsSettings _awsSettings;

        public ImageStorageService(AwsSettings awsSettings)
        {
            _awsSettings = awsSettings;
            _s3Client = new AmazonS3Client(awsSettings.AWS_ACCESS_KEY, awsSettings.AWS_SECRET_KEY, Amazon.RegionEndpoint.GetBySystemName(awsSettings.AWS_REGION));
        }

        public async Task<string> UploadImageAsync(IFormFile imageFile)
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