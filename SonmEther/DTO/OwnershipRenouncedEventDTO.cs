using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
namespace SonmEther.DTO
{
    [Event("OwnershipRenounced")]
    public class OwnershipRenouncedEventDTO
    {
        [Parameter("address", "previousOwner", 1, true )]
        public string PreviousOwner {get; set;}
    }
}
