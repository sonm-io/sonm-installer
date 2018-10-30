using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.ContractHandlers;
using System.Threading;
using SonmEther.CQS;
using SonmEther.DTO;
namespace SonmEther.Service
{

    public class SonmService
    {
    
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Web3 web3, SonmDeployment sonmDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<SonmDeployment>().SendRequestAndWaitForReceiptAsync(sonmDeployment, cancellationTokenSource);
        }
        public static Task<string> DeployContractAsync(Web3 web3, SonmDeployment sonmDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<SonmDeployment>().SendRequestAsync(sonmDeployment);
        }
        public static async Task<SonmService> DeployContractAndGetServiceAsync(Web3 web3, SonmDeployment sonmDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, sonmDeployment, cancellationTokenSource);
            return new SonmService(web3, receipt.ContractAddress);
        }
    
        protected Web3 Web3{ get; }
        
        protected ContractHandler ContractHandler { get; }
        
        public SonmService(Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }
    
        public Task<BigInteger> PayoutSupremumQueryAsync(PayoutSupremumFunction payoutSupremumFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PayoutSupremumFunction, BigInteger>(payoutSupremumFunction, blockParameter);
        }
        public Task<string> RenounceOwnershipRequestAsync(RenounceOwnershipFunction renounceOwnershipFunction)
        {
             return ContractHandler.SendRequestAsync(renounceOwnershipFunction);
        }
        public Task<TransactionReceipt> RenounceOwnershipRequestAndWaitForReceiptAsync(RenounceOwnershipFunction renounceOwnershipFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(renounceOwnershipFunction, cancellationToken);
        }
        public Task<string> OwnerQueryAsync(OwnerFunction ownerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(ownerFunction, blockParameter);
        }
        public Task<string> TransferOwnershipRequestAsync(TransferOwnershipFunction transferOwnershipFunction)
        {
             return ContractHandler.SendRequestAsync(transferOwnershipFunction);
        }
        public Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(TransferOwnershipFunction transferOwnershipFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
        }
        public Task<string> RegisterWorkerRequestAsync(RegisterWorkerFunction registerWorkerFunction)
        {
             return ContractHandler.SendRequestAsync(registerWorkerFunction);
        }
        public Task<TransactionReceipt> RegisterWorkerRequestAndWaitForReceiptAsync(RegisterWorkerFunction registerWorkerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerWorkerFunction, cancellationToken);
        }
        public Task<string> ConfirmWorkerRequestAsync(ConfirmWorkerFunction confirmWorkerFunction)
        {
             return ContractHandler.SendRequestAsync(confirmWorkerFunction);
        }
        public Task<TransactionReceipt> ConfirmWorkerRequestAndWaitForReceiptAsync(ConfirmWorkerFunction confirmWorkerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(confirmWorkerFunction, cancellationToken);
        }
        public Task<string> RemoveWorkerRequestAsync(RemoveWorkerFunction removeWorkerFunction)
        {
             return ContractHandler.SendRequestAsync(removeWorkerFunction);
        }
        public Task<TransactionReceipt> RemoveWorkerRequestAndWaitForReceiptAsync(RemoveWorkerFunction removeWorkerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(removeWorkerFunction, cancellationToken);
        }
        public Task<string> RegisterAdminRequestAsync(RegisterAdminFunction registerAdminFunction)
        {
             return ContractHandler.SendRequestAsync(registerAdminFunction);
        }
        public Task<TransactionReceipt> RegisterAdminRequestAndWaitForReceiptAsync(RegisterAdminFunction registerAdminFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerAdminFunction, cancellationToken);
        }
        public Task<string> EnableAutoPayoutRequestAsync(EnableAutoPayoutFunction enableAutoPayoutFunction)
        {
             return ContractHandler.SendRequestAsync(enableAutoPayoutFunction);
        }
        public Task<TransactionReceipt> EnableAutoPayoutRequestAndWaitForReceiptAsync(EnableAutoPayoutFunction enableAutoPayoutFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(enableAutoPayoutFunction, cancellationToken);
        }
        public Task<string> DisableAutoPayoutRequestAsync(DisableAutoPayoutFunction disableAutoPayoutFunction)
        {
             return ContractHandler.SendRequestAsync(disableAutoPayoutFunction);
        }
        public Task<TransactionReceipt> DisableAutoPayoutRequestAndWaitForReceiptAsync(DisableAutoPayoutFunction disableAutoPayoutFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(disableAutoPayoutFunction, cancellationToken);
        }
        public Task<string> GetMaterOfAdminQueryAsync(GetMaterOfAdminFunction getMaterOfAdminFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetMaterOfAdminFunction, string>(getMaterOfAdminFunction, blockParameter);
        }
        public Task<string> GetMasterQueryAsync(GetMasterFunction getMasterFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetMasterFunction, string>(getMasterFunction, blockParameter);
        }
        public Task<bool> GetAutoPayoutFlagQueryAsync(GetAutoPayoutFlagFunction getAutoPayoutFlagFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetAutoPayoutFlagFunction, bool>(getAutoPayoutFlagFunction, blockParameter);
        }
    }
}
