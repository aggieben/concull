module SyndicationTests

open System.Collections.Generic
open System.IO
open System.Net
open System.Threading.Tasks

open AngleSharp
open AngleSharp.Network
open Xunit
open BitThicket.Concull.Feeds

[<Fact>]
let ``parse single feed item`` () =
    let context = Utils.makeContextForFile "html/index.xml" "text/xml"

    async {
        // this url is just to document where the doc came from; the doc is not fetched live in this test
        let! result = Web.fetchDocumentWithContext context "http://benjamincollins.com/index.xml"        

        Assert.NotNull(result)
        Assert.IsAssignableFrom<Dom.Xml.IXmlDocument>(result) |> ignore

        
    }