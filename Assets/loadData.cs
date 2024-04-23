using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using System.Linq;
using TMPro;  // Add this at the top with other 'using' directives


public class loadData : MonoBehaviour
{

    public TextMeshProUGUI policyText;  // Public reference to the TextMeshPro UI component
    private string[] rows; // Array to store rows of data
    private int currentRow = 0; // Index of the current row being displayed
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
        // Split the data into rows and store them
        rows = csvData.Split('\n');
        currentRow = 1; // Start from the second row, skipping headers
        DisplayCurrentRow();
    }

    void Update()
    {
        // Check if the space bar is pressed
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            NextRow();
        }
    }

    void NextRow()
    {
        // Increment the currentRow index and display the next row
        if (currentRow < rows.Length - 1)
        {
            currentRow++;
            DisplayCurrentRow();
        }
    }

    void DisplayCurrentRow()
    {
        // Display the current row in the TextMeshPro text component
        if (currentRow < rows.Length)
        {
            policyText.text = rows[currentRow];
        }
    }
}
