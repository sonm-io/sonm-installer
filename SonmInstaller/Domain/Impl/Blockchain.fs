module SonmInstaller.Domain.Blockchain

open System.Numerics
open SonmInstaller.Tools
open Nethereum.Hex.HexConvertors.Extensions
open Nethereum.KeyStore
open Nethereum.Signer
open Nethereum.Util
open Nethereum.Web3.Accounts
open SonmEther.CQS
open SonmEther.Service
open Nethereum.Web3
open Nethereum.RPC.Accounts
open Nethereum.Hex.HexTypes
open Newtonsoft.Json

let genAccount () = 
    let ecKey = EthECKey.GenerateKey()
    let privateKey = ecKey.GetPrivateKeyAsBytes().ToHex()
    new Account(privateKey)

let getUtcFileName address =
    let service = new KeyStoreService()
    service.GenerateUTCFileName(address)    

let generateKeyStore (account: Account) password =
    let service = new KeyStoreService()
    service.EncryptAndGenerateDefaultKeyStoreAsJson(password, account.PrivateKey.HexToByteArray(), account.Address)

type RegisterAdminConfig = {
    blockChainUrl: string
    smartContractAddress: string
    account: IAccount
    adminAddr: string
}

let registerAdmin (cfg: RegisterAdminConfig) = async {
    let web3 = new Web3 (cfg.account, cfg.blockChainUrl)
    let srv = new SonmService (web3, cfg.smartContractAddress)
    let fn = new RegisterAdminFunction (Admin = cfg.adminAddr)
    let! t = srv.RegisterAdminRequestAndWaitForReceiptAsync fn |> Async.AwaitTask
    if t.Status.Value = BigInteger 0 then
        let ex = new exn("Invoking smart contract failed. Method: registerAdmin.")
        [
            "blockChainUrl", cfg.blockChainUrl
            "smartContractAddress", cfg.smartContractAddress
            "adminAddr", cfg.adminAddr
            "Transaction", JsonConvert.SerializeObject t
        ] |> List.iter (fun (key, value) -> ex.Data.[key] <- value)
    
}