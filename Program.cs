using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

namespace cosmosdb_core_bulk_import
{
    class Program
    {

        //
        //
        private const string EndpointUrl = "https://mv-website-content-comsosdbeus.documents.azure.com:443/";
        
        private const string DatabaseName = "mv-content-db";
        private const string ContainerName = "web-content-items";
        private const int AmountToInsert = 300;

        private const string keyVaultSecretName = "cosmosdbprimarykey";

        static async Task Main(string[] args)
        {
            //var items = LoadJson("data/temp2.json");
///Users/coleparsons/development/repos/cosmosdb-core-bulk-import/data
//data
            System.IO.DirectoryInfo root = new DirectoryInfo("data"); //.di.RootDirectory;

            System.IO.FileInfo[] files = null;

            // First, process all the files directly under this folder
            try
            {
                files = root.GetFiles("*.*");
            }
            // This is thrown if even one of the files requires permissions greater
            // than the application provides.
            catch (UnauthorizedAccessException e)
            {
                // This code just writes out the message and continues to recurse.
                // You may decide to do something different here. For example, you
                // can try to elevate your privileges and access the file again.
                //log.Add(e.Message);
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            
            var authKey = await GetSecret();
            //Environment.Exit(1);
            var cosmosClient = new CosmosClient(EndpointUrl, authKey, new CosmosClientOptions() { AllowBulkExecution = true });

            Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(Program.DatabaseName);

            await database.DefineContainer(Program.ContainerName, "/id")
                    .WithIndexingPolicy()
                        .WithIndexingMode(IndexingMode.Consistent)
                        .WithIncludedPaths()
                            .Attach()
                        .WithExcludedPaths()
                            .Path("/*")
                            .Attach()
                    .Attach()
                .CreateAsync(4000);

                //Use the helper function to initialize a list of documents to work with:
                //IReadOnlyCollection<Item> items = Program.GetItemsToInsert();

            foreach(var fi in files)
            {

                var path = $"{fi.DirectoryName}/{fi.Name}";
                
               
               //List<Task> tasks = new List<Task>(AmountToInsert);

                if(path.Contains("BlockImages")){
                     await BulkLoadDataBlockImages( database,  path);    
                }
                else if(path.Contains("BlockLinks")){
                    await BulkLoadDataBlockLinks( database,  path); 
                }
                else if(path.Contains("Menus"))
                {
                     await BulkLoadDataMenus( database,  path);       
                }
                else if(path.Contains("SitePages")){
                    await BulkLoadDataSitePage( database,  path);
                }
                // Wait until all are done
                //await Task.WhenAll(tasks);
            }

        }

        private static async Task BulkLoadDataBlockImages( Database database,string path){

            Container container = database.GetContainer(ContainerName);
            BlockImage[] items = null;
            using (StreamReader r = new StreamReader(path))
            {
                var json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<BlockImage[]>(json);

                foreach (var item in items)
                {
                   // Console.WriteLine("{0} {1}", item.temp, item.vcc);
                   item.id = Guid.NewGuid().ToString();
                   item.BlockImageId = item.id;
                }
            } 

            foreach (BlockImage item in items)
                {

                         try
                            {
                                // Read the item to see if it exists
                                ItemResponse<BlockImage> blockImageRespose = await container.ReadItemAsync<BlockImage>(item.id, new PartitionKey(item.id));
                                Console.WriteLine("Item in database with id: {0} already exists\n", blockImageRespose.Resource.id);
                            }
                            catch(CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                            {
                                // Create an item in the container representing the Wakefield family. Note we provide the value of the partition key for this item, which is "Wakefield"
                                ItemResponse<BlockImage> blockImageRespose = await container.CreateItemAsync<BlockImage>(item, new PartitionKey(item.id));

                                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", blockImageRespose.Resource.id, blockImageRespose.RequestCharge);
                            }

                }
        }

        private static async Task BulkLoadDataBlockLinks( Database database,string path){
            Container container = database.GetContainer(ContainerName);
            BlockLink[] items = null;
            using (StreamReader r = new StreamReader(path))
            {
                var json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<BlockLink[]>(json);

                foreach (var item in items)
                {
                   // Console.WriteLine("{0} {1}", item.temp, item.vcc);
                   item.id = Guid.NewGuid().ToString();
                   item.BlockLinkId = item.id;
                }
            } 

            foreach (BlockLink item in items)
                {

                         try
                            {
                                // Read the item to see if it exists
                                ItemResponse<BlockLink> blockLinkRespose = await container.ReadItemAsync<BlockLink>(item.id, new PartitionKey(item.id));
                                Console.WriteLine("Item in database with id: {0} already exists\n", blockLinkRespose.Resource.id);
                            }
                            catch(CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                            {
                                // Create an item in the container representing the Wakefield family. Note we provide the value of the partition key for this item, which is "Wakefield"
                                ItemResponse<BlockLink> blockLinkRespose = await container.CreateItemAsync<BlockLink>(item, new PartitionKey(item.id));

                                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", blockLinkRespose.Resource.id, blockLinkRespose.RequestCharge);
                            }

                }
        }
        private static async Task BulkLoadDataMenus( Database database,string path){
            Container container = database.GetContainer(ContainerName);
            Menu[] items = null;
            using (StreamReader r = new StreamReader(path))
            {
                var json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<Menu[]>(json);

                foreach (var item in items)
                {
                   // Console.WriteLine("{0} {1}", item.temp, item.vcc);
                   item.id = Guid.NewGuid().ToString();
                   item.MenuId = item.id;
                }
            }

            foreach (Menu item in items)
                {

                         try
                            {
                                // Read the item to see if it exists
                                ItemResponse<Menu> menuRespose = await container.ReadItemAsync<Menu>(item.id, new PartitionKey(item.id));
                                Console.WriteLine("Item in database with id: {0} already exists\n", menuRespose.Resource.id);
                            }
                            catch(CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                            {
                                // Create an item in the container representing the Wakefield family. Note we provide the value of the partition key for this item, which is "Wakefield"
                                ItemResponse<Menu> menuRespose = await container.CreateItemAsync<Menu>(item, new PartitionKey(item.id));

                                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", menuRespose.Resource.id, menuRespose.RequestCharge);
                            }

                }
        }

        private static async Task BulkLoadDataSitePage( Database database, string path){

            Container container = database.GetContainer(ContainerName);
            SitePage[] items = null;
            using (StreamReader r = new StreamReader(path))
            {
                var json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<SitePage[]>(json);

                foreach (var item in items)
                {
                   // Console.WriteLine("{0} {1}", item.temp, item.vcc);
                   item.id = Guid.NewGuid().ToString();
                   item.PageId = item.id;
                }
            }

            foreach (SitePage item in items)
                {
                    /* tasks.Add(container.CreateItemAsync(item, new PartitionKey(item.PageId))
                        .ContinueWith(itemResponse =>
                        {
                            if (!itemResponse.IsCompletedSuccessfully)
                            {
                                AggregateException innerExceptions = itemResponse.Exception.Flatten();
                                if (innerExceptions.InnerExceptions.FirstOrDefault(innerEx => innerEx is CosmosException) is CosmosException cosmosException)
                                {
                                    Console.WriteLine($"Received {cosmosException.StatusCode} ({cosmosException.Message}).");
                                }
                                else
                                {
                                    Console.WriteLine($"Exception {innerExceptions.InnerExceptions.FirstOrDefault()}.");
                                }
                            }
                        })); */
                         try
                            {
                                // Read the item to see if it exists
                                ItemResponse<SitePage> sitePageRespose = await container.ReadItemAsync<SitePage>(item.id, new PartitionKey(item.id));
                                Console.WriteLine("Item in database with id: {0} already exists\n", sitePageRespose.Resource.id);
                            }
                            catch(CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                            {
                                // Create an item in the container representing the Wakefield family. Note we provide the value of the partition key for this item, which is "Wakefield"
                                ItemResponse<SitePage> sitePageResponse = await container.CreateItemAsync<SitePage>(item, new PartitionKey(item.id));

                                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", sitePageResponse.Resource.id, sitePageResponse.RequestCharge);
                            }

                }
        }
        private static IReadOnlyCollection<Item> GetItemsToInsert()
        {
            return new Bogus.Faker<Item>()
            .StrictMode(true)
            //Generate item
            .RuleFor(o => o.id, f => Guid.NewGuid().ToString()) //id
            .RuleFor(o => o.username, f => f.Internet.UserName())
            .RuleFor(o => o.pk, (f, o) => o.id) //partitionkey
            .Generate(AmountToInsert);
        }


        public static SitePage[] LoadJson(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                var json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<SitePage[]>(json);

                foreach (var item in items)
                {
                   // Console.WriteLine("{0} {1}", item.temp, item.vcc);
                   item.id = Guid.NewGuid().ToString();
                   item.PageId = item.id;
                }

                return items;
            }
        }


        private static async Task<string> GetSecret(){
            //https://colep-key-vault.vault.azure.net/

            // Environment variable with the Key Vault endpoint.
            string keyVaultUrl = "https://colep-key-vault.vault.azure.net/";

            var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());

            KeyVaultSecret bankSecret = await client.GetSecretAsync(keyVaultSecretName);
            Debug.WriteLine($"Secret is returned with name {bankSecret.Name} and value {bankSecret.Value}");

            return bankSecret.Value;
        }

    }

    public class Item
    {
        public string id {get;set;}
        public string pk {get;set;}

        public string username{get;set;}
    }



}
