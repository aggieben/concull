namespace BitThicket.Concull.Feeds

module Web =
    open System.Net.Http
    open AngleSharp

    let fetchContent (client:HttpClient) (uri:string) =
        async {
            let! resp = Async.AwaitTask (client.GetAsync(uri))

            return! Async.AwaitTask (resp.Content.ReadAsStringAsync())
        }

    let fetchDocumentWithContext (context:IBrowsingContext) (uri:string) =
        async {
            return! Async.AwaitTask (context.OpenAsync(uri))
        }

    let fetchDocument (uri:string) =
        let context = BrowsingContext.New(Configuration.Default)
        fetchDocumentWithContext context uri
