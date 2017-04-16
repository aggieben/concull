namespace BitThicket.Concull.Feeds

module Model =

    open System

    type Post = {
        Uri:Uri
        Title:string
        Summary:string
        Author:string
        Category:string
        Tags:string[]
        Date:DateTime
        Source:Uri
        Content:string // string for now, until netstandard2.0 allows FSharp.Data to be usable again
     }