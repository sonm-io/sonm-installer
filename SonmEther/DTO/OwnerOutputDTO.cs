using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
namespace SonmEther.DTO
{
    [FunctionOutput]
    public class OwnerOutputDTO
    {
        [Parameter("address", "", 1)]
        public string ReturnValue1 {get; set;}
    }
}
