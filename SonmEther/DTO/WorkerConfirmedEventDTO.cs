using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
namespace SonmEther.DTO
{
    [Event("WorkerConfirmed")]
    public class WorkerConfirmedEventDTO
    {
        [Parameter("address", "worker", 1, true )]
        public string Worker {get; set;}
        [Parameter("address", "master", 2, true )]
        public string Master {get; set;}
    }
}
