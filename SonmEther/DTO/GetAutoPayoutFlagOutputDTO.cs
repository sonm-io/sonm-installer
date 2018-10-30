using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
namespace SonmEther.DTO
{
    [FunctionOutput]
    public class GetAutoPayoutFlagOutputDTO
    {
        [Parameter("bool", "", 1)]
        public bool ReturnValue1 {get; set;}
    }
}
