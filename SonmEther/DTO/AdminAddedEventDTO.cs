using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
namespace SonmEther.DTO
{
    [Event("adminAdded")]
    public class AdminAddedEventDTO
    {
        [Parameter("address", "admin", 1, true )]
        public string Admin {get; set;}
        [Parameter("address", "master", 2, true )]
        public string Master {get; set;}
    }
}
