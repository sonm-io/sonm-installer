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
    [Function("RegisterWorker", "bool")]
    public class RegisterWorkerFunction: FunctionMessage
    {
        [Parameter("address", "_master", 1)]
        public string Master {get; set;}
    }
}
