using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Album.API.Domain.Exceptions
{
    public class AlbumsNotFoundException : Exception
    {
        public AlbumsNotFoundException() : base("No album found") { }
    }
}