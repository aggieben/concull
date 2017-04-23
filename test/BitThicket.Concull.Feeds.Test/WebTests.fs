module WebTests

open System
open System.Collections.Generic
open System.IO
open System.Net
open System.Net.Http
open System.Threading.Tasks
open Xunit
open BitThicket.Concull.Feeds
open AngleSharp
open AngleSharp.Network
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

[<Fact>]
let ``get parsed document from http response`` () =

    let requester = { new IRequester with
                        member this.SupportsProtocol(protocol) = true
                        member this.RequestAsync(request, cancel) =
                            Task.FromResult(
                                { new IResponse with
                                    member this.Address = Url("html/a-working-example-of-permissions-authorization.html")
                                    member this.StatusCode = HttpStatusCode.OK 
                                    member this.Headers = upcast new Dictionary<string,string>()
                                    member this.Content = upcast File.OpenRead("html/a-working-example-of-permissions-authorization.html") 
                                    member this.Dispose() = ()})
                        }
    // would need to write my own IRequester that could use the mocked client
    let config = Configuration.Default.WithDefaultLoader(setup = null, requesters = [ requester ])
    let context = BrowsingContext.New(config)

    async {
        let! result = Web.fetchDocumentWithContext context "html/a-working-example-of-permissions-authorization.html"

        Assert.NotNull(result)
    }