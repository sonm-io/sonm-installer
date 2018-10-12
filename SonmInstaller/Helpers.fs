namespace Helpers

module List = 

    let replaceAt (index: int) (item: 'a) (l: 'a list) = 
        let before = l |> List.take index
        let after = l |> List.skip (index+1)
        before@item::after

    let removeAt (index: int) (l: 'a list) = 
        let before = l |> List.take index
        let after = l |> List.skip (index+1)
        before@after