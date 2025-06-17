using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.API.Domain.Exceptions
{
    public class AlbumNotFoundException : Exception
    {
        public AlbumNotFoundException() : base("Album not found") { }
    }
}