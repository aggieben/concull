module WebTests

open System
open System.IO
open System.Net.Http
open System.Threading.Tasks
open Xunit
open BitThicket.Concull.Feeds

[<Fact>]
let ``extract content from http response`` () =
    Console.WriteLine(Directory.GetCurrentDirectory())

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


