//----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//----------------------------------------------------------------------------------


using Coolblue.Utils;
using Simple.CredentialManager;
using Soundboard.AzureApiApp.Dto;

namespace Soundboard.AzureApiApp.DataAccess
{
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Azure.Documents.Linq;
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        //**************************************************************************************************************************************
        // This sample demonstrates using Azure DocumentDB. In particular it demonstrates the following key concepts;
        // 1. How to connect to a DocumentDB Account
        // 2. How to query for a Database by its Id, and create one if not found
        // 3. How to query for a DocumentCollection by its Id, and create one if not found
        // 4. How to create a Document
        // 5. How to query for Documents
        // 6. How to delete a Database and a DocumentCollection
        // 
        // PRE-REQUISTES:
        // **************
        // In order to run this sample for Azure DocumentDB you need to have the following pre-requistes;
        //           1) An Azure subscription
        //              If you don't have an Azure subscription you can get a free trial. To create a free trial see:
        //              http://azure.microsoft.com/en-us/pricing/free-trial/
        //
        //           2) An Azure DocumentDB Account
        //              For instructions on creating an Azure DocumentDB account see: 
        //              http://azure.microsoft.com/en-us/documentation/articles/documentdb-create-account/
        //
        // NOTE:
        // *****
        // Never store credentials in source code. In this example placeholders in App.config are used. 
        //       For information on how to store credentials, see:
        //       Azure Websites: How Application Strings and Connection Strings Work 
        //       http://azure.microsoft.com/blog/2013/07/17/windows-azure-web-sites-how-application-strings-and-connection-strings-work/
        //
        // ADDITIONAL RESOURCES:
        // *********************
        // Service Documentation - http://aka.ms/documentdbdocs
        // Additional Samples - http://aka.ms/documentdbsamples      
        //**************************************************************************************************************************************

        static DocumentClient client;

        static void Main(string[] args)
        {
            string serviceEndpoint = new WindowsCredential("soundboard_serviceendpoint").Password;
            string authKey = new WindowsCredential("soundboard_authkey").Password;
            
            if (string.IsNullOrWhiteSpace(serviceEndpoint) || string.IsNullOrWhiteSpace(authKey) ||
                String.Equals(serviceEndpoint, "TODO - [YOUR ENDPOINT]", StringComparison.OrdinalIgnoreCase) ||
                String.Equals(authKey, "TODO - [YOUR AUTHKEY]", StringComparison.OrdinalIgnoreCase))
            {

                Console.WriteLine("Please update your DocumentDB Account credentials in App.config");
                Console.ReadKey();
            }

            else
            {
                try
                {
                    // 1. It is recommended to create an instance of DocumentClient and reuse the same instance
                    //    as opposed to creating, using and destroying the instance time and time again

                    //    For this sample we are using the Defaults. There are optional parameters for things like ConnectionPolicy
                    //    that allow you to change from Gateway to Direct or from HTTPS to TCP. 
                    //    For more information on this, please consult the Service Documentation page in Additional Resources
                    Console.WriteLine("1. Create an instance of DocumentClient");
                    using (client = new DocumentClient(new Uri(serviceEndpoint), authKey))
                    {
                        // 2.
                        Console.WriteLine("2. Getting reference to Database");
                        Database database = ReadOrCreateDatabase("Sounds");

                        // 3.
                        Console.WriteLine("3. Getting reference to a DocumentCollection");
                        DocumentCollection collection = ReadOrCreateCollection(database.SelfLink, "Documents");

                        // 4. 
                        Console.WriteLine("4. Inserting Documents");
                        CreateDocuments(collection.SelfLink);

                        // 5.
                        Console.WriteLine("5. Querying for Documents");
                        QueryDocuments(collection.SelfLink);
                    }
                }
                catch (DocumentClientException docEx)
                {
                    Exception baseException = docEx.GetBaseException();
                    Console.WriteLine("{0} StatusCode error occurred with activity id: {1}, Message: {2}",
                        docEx.StatusCode, docEx.ActivityId, docEx.Message, baseException.Message);
                }
                catch (AggregateException aggEx)
                {
                    Console.WriteLine("One or more errors occurred during execution");
                    foreach (var ex in aggEx.InnerExceptions)
                    {
                        DocumentClientException dce = ex as DocumentClientException;
                        if (dce != null)
                        {
                            Console.WriteLine("A Document Client Exception with Status Code {0} occurred with activity id {1}, message: {2}",
                                dce.StatusCode, dce.ActivityId, dce.Message);
                        }
                        else
                        {
                            Console.WriteLine("An exception of type {0} occurred: {1}", ex.GetType(), ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected exception of type {0} occurred: {1}", ex.GetType(), ex.Message);
                }

                Console.WriteLine("\nSample complete. Press any key to exit.");
                Console.ReadKey();
            }
        }

        private static Database ReadOrCreateDatabase(string databaseId)
        {
            // Most times you won't need to create the Database in code, someone has likely created
            // the Database already in the Azure Management Portal, but you still need a reference to the
            // Database object so that you can work with it. Therefore this first query should return a record
            // the majority of the time
            var db = client.CreateDatabaseQuery()
                            .Where(d => d.Id == databaseId)
                            .AsEnumerable()
                            .FirstOrDefault();

            
            // In case there was no database matching, go ahead and create it. 
            if (db != null)
            {
                // delete it
                Cleanup(db.SelfLink);
            }
            
            // create it
            db = client.CreateDatabaseAsync(new Database { Id = databaseId }).Result;

            return db;
        }

        private static DocumentCollection ReadOrCreateCollection(string databaseLink, string collectionId)
        {
            var col = client.CreateDocumentCollectionQuery(databaseLink)
                              .Where(c => c.Id == collectionId)
                              .AsEnumerable()
                              .FirstOrDefault();

            if (col == null)
            {
                Console.WriteLine("3. DocumentCollection not found.");
                Console.WriteLine("3. Creating new DocumentCollection.");

                // NOTE: NB: CreateDocumentCollectionAsync has a billing implication. 
                //           A DocumentCollection is a physical entity taking up physical resources
                //           It is NOT (neccessarily) a logical way to group Documents of the same type
                //           For more information on DocumentCollections, and DocumentDB pricing please refer
                //           to the DocumentDB pricing page - http://azure.microsoft.com/en-us/pricing/details/documentdb/
                col = client.CreateDocumentCollectionAsync(databaseLink, new DocumentCollection { Id = collectionId }).Result;
            }

            Console.WriteLine("3. Creating DocumentCollection");
            return col;
        }

        private static void CreateDocuments(string collectionLink)
        {
            // DocumentDB provides many different ways of working with documents. 
            // 1. You can create an object that extends the Document base class
            // 2. You can use any POCO whether as it is without extending the Document base class
            // 3. You can use dynamic types
            // 4. You can even work with Streams directly.
            //
            // In DocumentDB every Document must have an "id" property. If you supply one, it must be unique. 
            // If you do not supply one, DocumentDB will generate a GUID for you and add it to the Document as "id".
            // You can disable the auto generaction of ids if you prefer by setting the disableAutomaticIdGeneration option on CreateDocumentAsync method

            var task0 = client.DeleteDocumentCollectionAsync(collectionLink);

            var task1 = client.CreateDocumentAsync(collectionLink, new Sound
            {
                Name = "He meisje",
                ImagePath = "http://strengthandconditioningfitness.com/wp-content/uploads/2011/04/Kangaroos-jump-high-1.jpg",
            });

            var task2 = client.CreateDocumentAsync(collectionLink, new Sound
            {
                Name = "Ik heb een mop",
                ImagePath = "http://cbsnews2.cbsistatic.com/hub/i/r/2011/07/25/df3a8530-a644-11e2-a3f0-029118418759/thumbnail/620x350/0cb41f489b87587b34042d9793a7cab7/red_kangaroo.jpg",
            });

            // Wait for the above Async operations to finish executing
            Task.WaitAll(task1, task2);

            Console.WriteLine("4. Documents successfully created");
        }

        private static void QueryDocuments(string collectionLink)
        {
            // The .NET SDK for DocumentDB supports 3 different methods of Querying for Documents
            // LINQ queries, lamba and SQL
            // Any query can be written in any of the 3 formats shown below, you can pick which you prefer.

            // 1. Get each Document in the collection, 1 record at a time
            //    This is how to retrieve records in an async operation.
            //    You can control the number of results returned by adjusting MaxItemCount of FeedOptions
            var feed = client.CreateDocumentQuery<Sound>(collectionLink, new FeedOptions() { MaxItemCount = 10 }).AsDocumentQuery();
            while (feed.HasMoreResults)
            {
                foreach (Sound sound in feed.ExecuteNextAsync().Result)
                {
                    Console.WriteLine("5. Sound Name is - {0}", sound.Name);
                }
            }
        }

        private static void Cleanup(string databaseId)
        {
            client.DeleteDatabaseAsync(databaseId).Wait();
        }
    }
}
