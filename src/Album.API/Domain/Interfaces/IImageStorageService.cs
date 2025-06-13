using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Album.API.Domain.Interfaces
{
    public interface IImageStorageService
    {
        Task<string> UploadImageAsync(IFormFile image);
    }
}