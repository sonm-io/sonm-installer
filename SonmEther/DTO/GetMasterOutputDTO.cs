using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
namespace SonmEther.DTO
{
    [FunctionOutput]
    public class GetMasterOutputDTO
    {
        [Parameter("address", "master", 1)]
        public string Master {get; set;}
    }
}
