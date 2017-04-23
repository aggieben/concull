module WebTests

open System
open System.IO
open System.Net.Http
open System.Threading.Tasks
open Xunit
open BitThicket.Concull.Feeds
open AngleSharp
open AngleSharp.Network.Default

[<Fact>]
let ``extract content from http response`` () =
    let handler = { new HttpMessageHandler() with
                        override x.SendAsync(request, cancellationToken) =
                            Task.FromResult(new HttpResponseMessage(
                                                Content = new StreamContent(File.OpenRead("html/a-working-example-of-permissions-authorization.html"))
                                            )
                            )
                    }
    let client = new HttpClient(handler);

    async {
        let! result = Web.fetchContent client "http://benjamincollins.com/blog/a-working-example-of-permissions-authorization/"

        Assert.NotNull(result)
    }

let ``get parsed document from http response`` () =
    let handler = { new HttpMessageHandler() with
                        override x.SendAsync(request, cancellationToken) =
                            Task.FromResult(new HttpResponseMessage(
                                                Content = new StreamContent(File.OpenRead("html/a-working-example-of-permissions-authorization.html"))
                                            )
                            )
                    }
    let client = new HttpClient(handler)

    // would need to write my own IRequester that could use the mocked client
    // let config = [| new HttpClientRequester(client); new DataRequester() |] |> new LoaderService |> Configuration.Default.With

    Assert.True(false)

