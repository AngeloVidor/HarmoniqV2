using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.API.Domain.Exceptions
{
    public class MusicNotFoundException : Exception
    {
        public MusicNotFoundException() : base("The requested music item was not found.") { }
    }
}