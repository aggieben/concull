namespace BitThicket.Concull.Feeds

module Syndication =

open AngleSharp

// TODO: add atom to this
let (|Rss|_|) (node:Dom.Xml.IXmlDocument) = 
    if node.NodeName = "rss" then Some node
    else if node.NodeName = "atom" then Some node

let getFeedItems (xml:Dom.Xml.IXmlDocument) = 
    let items = 
        match xml with
        | n1 when n1.NodeName = "rss" ->
            match n1.Children[0] with 
            | n2 when n2.NodeName = "channel" ->
            | _ -> failwith "failed to parse feed"
        | _ -> failwith "failed to parse feed"