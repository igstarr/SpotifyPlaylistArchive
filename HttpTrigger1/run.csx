
#r "Newtonsoft.Json"

using Newtonsoft.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

public static async Task<IActionResult> Run(HttpRequestMessage req,
    ICollector<string> outputQueueItem, ILogger log)
{
        dynamic body = await req.Content.ReadAsStringAsync();
        var e = JsonConvert.DeserializeObject<List<string>>(body as string);
     foreach(var item in e) {
        outputQueueItem.Add(item);
     }
     return new OkObjectResult($"Added {e.Count()}");
}
