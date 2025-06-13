using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Album.API.Models
{
    public class AwsSettings
    {
        public string AWS_ACCESS_KEY { get; set; }
        public string AWS_SECRET_KEY { get; set; }
        public string AWS_REGION { get; set; }
        public string AWS_BUCKET_NAME { get; set; }
    }
}