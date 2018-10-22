module SonmInstaller.Domain.Blockchain

open Nethereum.Hex.HexConvertors.Extensions
open Nethereum.KeyStore
open Nethereum.Signer
open Nethereum.Util
open Nethereum.Web3.Accounts

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

