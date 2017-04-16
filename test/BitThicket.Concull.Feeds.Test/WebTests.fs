module WebTests

open System
open System.Net.Http
open Xunit
open BitThicket.Concull.Feeds

[<Fact>]
let ``extract content from http response`` () =
    use client = new HttpClient()
    let result = Web.fetchContent client "http://benjamincollins.com/blog/a-working-example-of-permissions-authorization/"
    
    Assert.NotNull(result)
