using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encriptacion
{
    public interface IHashingService
    {
        string CreateSha256(string password);
    }
}
