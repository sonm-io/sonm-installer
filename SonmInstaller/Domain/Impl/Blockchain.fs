module SonmInstaller.Domain.Blockchain

open Nethereum.Hex.HexConvertors.Extensions
open Nethereum.KeyStore
open Nethereum.Signer
open Nethereum.Util
open Nethereum.Web3.Accounts

module Impl = 
    let genAccount () = 
        let ecKey = EthECKey.GenerateKey()
        let privateKey = ecKey.GetPrivateKeyAsBytes().ToHex()
        new Account(privateKey)

open Impl

let generateKeyStore password =
    let account = genAccount ()
    let service = new KeyStoreService()
    let fileName = service.GenerateUTCFileName (account.Address)
    let json = service.EncryptAndGenerateDefaultKeyStoreAsJson(password, account.PrivateKey.HexToByteArray(), account.Address)
    fileName, json