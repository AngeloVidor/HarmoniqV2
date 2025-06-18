using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.API.Domain.Exceptions
{
    public class AlbumMusicsNotFoundException : Exception
    {
        public AlbumMusicsNotFoundException() : base("Album musics not found for the specified album and producer.") { }
    }
}