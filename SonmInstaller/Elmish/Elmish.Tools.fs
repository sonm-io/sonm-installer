module Elmish.Tools

let isChanged (prev: 'model option) (next: 'model) (prop: 'model -> 'p) = 
    Option.isNone prev || prop prev.Value <> prop next

let doIfChanged (prev: 'model option) (next: 'model) (prop: 'model -> 'p) fn = 
    if Option.isNone prev then
        fn None (prop next)
    elif prop prev.Value <> prop next then
        fn (Some (prop prev.Value)) (prop next) 
        
let doNextIfChanged (prev: 'model option) (next: 'model) (prop: 'model -> 'p) fn = 
    if isChanged prev next prop then
        fn next

let doPropIfChanged (prev: 'model option) (next: 'model) (prop: 'model -> 'p) fn = 
    if isChanged prev next prop then
        fn (prop next)