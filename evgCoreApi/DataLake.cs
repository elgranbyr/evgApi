using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using evgCoreApi.model;


namespace evgCoreApi
{
    public class dataLake
    {
        
        public void UploadSession(string sess,DateTime dt)
        {
            var StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=evgdatalake;AccountKey=kJ8RehP9NNifyXLFAMc9nJSt5F8TnLvjySIZlRPgnUxE+giDPFXq7kjQDGVtXzZpl0ncTff1wq4cwm1ZrnS0DA==";
                                           
                  CloudStorageAccount storageAccount = CloudStorageAccount.Parse(StorageConnectionString);

            

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("evgmugkatcher");

            // Retrieve reference to a blob named "myblob".\
            var myblob=string.Format("sessions/{0}/{1}.json", dt.ToString("yyyy/MM/dd"), Guid.NewGuid());
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(myblob);
         /*   JsonSerializer serializer = new JsonSerializer();
            var ret= JsonConvert.SerializeObject(sess);*/
            // Create or overwrite the "myblob" blob with contents from a local file.

            blockBlob.UploadText(sess);
            

        }
    }
}
