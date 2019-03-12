module SonmInstaller.Atom

open System.Threading

module Impl =
    type Atom<'T when 'T : not struct>(value : 'T) =
        let refCell = ref value
    
        let rec swap f = 
            let currentValue = !refCell
            let result = Interlocked.CompareExchange<'T>(refCell, f currentValue, currentValue)
            if obj.ReferenceEquals(result, currentValue) then result
            else Thread.SpinWait 20; swap f
        
        member self.Value with get() = !refCell
        member self.Swap (f : 'T -> 'T) = swap f
        
    
open Impl

let atom value = new Atom<_>(value)
    
let (!) (atom : Atom<_>) =  
    atom.Value
    
let swap (atom : Atom<_>) (f : _ -> _) =
    atom.Swap f