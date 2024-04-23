using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public class loadData : MonoBehaviour
{
    private string googleSheetUrl = "https://docs.google.com/spreadsheets/d/e/2PACX-1vQeXBxxLO9ftjUVO5iWXDfzhw0KTlIOn8557uyOL98xmYmyoo-tdofdkad2Jbcaet4If7xDD_yHZH18/pub?gid=0&single=true&output=csv";

    void Start()
    {
        StartCoroutine(DownloadData());
    }

    IEnumerator DownloadData()
    {
        UnityWebRequest request = UnityWebRequest.Get(googleSheetUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching data: " + request.error);
        }
        else
        {
            // Successfully downloaded the data
            ProcessData(request.downloadHandler.text);
        }
    }

    void ProcessData(string csvData)
    {
        // Split the data into rows.
        string[] rows = csvData.Split('\n');
        foreach (string row in rows)
        {
            // Split each row into columns. Assume columns are separated by commas.
            string[] columns = row.Split(',');
            // Process each column as needed
            Debug.Log("Row data: " + string.Join(", ", columns));
        }
    }
}
