namespace BitThicket.Concull.Feeds

module Web =
    open System.Net.Http

    let fetchContent (client:HttpClient) (uri:string) =
        async {
            let! resp = Async.AwaitTask (client.GetAsync(uri))

            return! Async.AwaitTask (resp.Content.ReadAsStringAsync())
        }
        
