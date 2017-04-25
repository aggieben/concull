module Utils

open System
open System.Collections.Generic
open System.IO
open System.Net
open System.Threading.Tasks

open AngleSharp
open AngleSharp.Network

let makeContextForFile path contentType =
    let requester = { new IRequester with
                        member this.SupportsProtocol(protocol) = true
                        member this.RequestAsync(request, cancel) =
                            Task.FromResult(
                                { new IResponse with
                                    member this.Address = Url(String.Empty)
                                    member this.StatusCode = HttpStatusCode.OK 
                                    member this.Headers = upcast new Dictionary<string,string>(dict [("Content-Type", contentType)])
                                    member this.Content = upcast File.OpenRead(path) 
                                    member this.Dispose() = ()})
                        }

    // would need to write my own IRequester that could use the mocked client
    let config = Configuration.Default.WithDefaultLoader(setup=null, requesters=[ requester ])
    BrowsingContext.New(config)