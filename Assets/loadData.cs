using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using TMPro;  // Add this at the top with other 'using' directives


public class loadData : MonoBehaviour
{

    public TextMeshProUGUI policyText;  // Public reference to the TextMeshPro UI component
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
        policyText.text = "";  // Initialize or clear existing text
        foreach (string row in rows)
        {
            // Append each row as a new line in the TextMeshPro text
            policyText.text += row + "\n";
        }
    }
}
