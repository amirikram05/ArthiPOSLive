using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json.Linq;
namespace ArthiPOS.Controls.test
{

    public class FirestoreUploader
    {
        

    public async Task SaveJsonWithProgress(string json)
    {
        var client = new FirebaseClient(
            "https://arthiapp-5d72b-default-rtdb.firebaseio.com"
        );

        // Parse JSON array
        JArray records = JArray.Parse(json);

        int total = records.Count;
        int current = 0;

        // Dictionary to store all records (will be stored under root)
        var allRecords = new JArray();

        foreach (JObject record in records)
        {
            allRecords.Add(record); // add to array

            current++;
            // Update progress
        }

        // Upload entire array under root node
        await client
            .Child("customer_augrai")
            .PutAsync(allRecords);
    }



}

}
