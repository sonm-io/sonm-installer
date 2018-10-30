using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
namespace SonmEther.DTO
{
    [FunctionOutput]
    public class PayoutSupremumOutputDTO
    {
        [Parameter("uint256", "", 1)]
        public BigInteger ReturnValue1 {get; set;}
    }
}
