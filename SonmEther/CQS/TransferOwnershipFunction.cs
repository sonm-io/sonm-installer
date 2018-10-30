using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.Contracts;
using Nethereum.ABI.FunctionEncoding.Attributes;
using SonmEther.DTO;
namespace SonmEther.CQS
{
    [Function("transferOwnership")]
    public class TransferOwnershipFunction: FunctionMessage
    {
        [Parameter("address", "_newOwner", 1)]
        public string NewOwner {get; set;}
    }
}
