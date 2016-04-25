using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace LogOutUser.Controllers
{
    public class PublicKey
    {
        public string Modules { get; set; }
        public string Exponent { get; set; }
        public string PK { get; set; }
        public int Size { get; set; }

    }
}
