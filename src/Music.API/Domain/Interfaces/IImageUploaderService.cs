using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.API.Domain.Interfaces
{
    public interface IImageUploaderService
    {
        Task<string> UploadAsync(IFormFile imageFile);
    }
}