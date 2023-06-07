using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Collections.Generic;
using System.Text.Json;
using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Newtonsoft.Json;
using System.Linq;
using System;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage;

namespace GetNewSong
{
    public static class Initialize
    {
        [FunctionName("UpdateSpotifyArchiveList")]
        public static async Task Run(
            [TimerTrigger("0 10 10 * * *")]TimerInfo myTimer, ExecutionContext context, ILogger log)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
                .AddEnvironmentVariables()
                .Build();
            string clientId = config["ClientId"];
            string clientSecret = config["ClientSecret"];
            var spotifyHelpers = new SpotifyHelpers();
            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await spotifyHelpers.GetAccessToken(client, clientId, clientSecret));
            var play = new PlayList();
            var tawCurrentList = await play.Get("1zZRVYNcJfjWAgsSbvox0O", client);
            var listofIds = new List<string>();
            foreach (var item in tawCurrentList.items.Where(x => x.added_at.Day == DateTime.Now.AddDays(-1).Day))
            {
                listofIds.Add(item.track.id);
            }
            //var responseMessage = System.Text.Json.JsonSerializer.Serialize(listofIds);
            //var content = new StringContent(responseMessage.ToString(), Encoding.UTF8, "application/json");
            //client.DefaultRequestHeaders.Accept.Clear();
            //var result = await client.PostAsync("https://writemessagetoqueue232321.azurewebsites.net/api/HttpTrigger1?code=4Q6OEYconlW0I_CzrhVSxFmT2uLcpYyIUWMahulMsWx7AzFuFNkQaA==", content); //or

            // Retrieve the connection string for the Azure Storage account
            string storageConnectionString = config["AzureQueueStorage"];

            // Create a CloudStorageAccount object by parsing the connection string
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);

            // Create a CloudQueueClient object from the storage account
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Get a reference to the queue
            CloudQueue queue = queueClient.GetQueueReference("myqueue");

            // Create a message and add it to the queue
            string messageContent = "Hello from Azure Function!";
            CloudQueueMessage message = new CloudQueueMessage(messageContent);
            foreach (var item in listofIds)
            {
                await queue.AddMessageAsync(item);
            }
        }
    }
}
