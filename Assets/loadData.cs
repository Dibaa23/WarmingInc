using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using System.Collections;

public class loadData : MonoBehaviour
{
    public TextMeshProUGUI yearText;  // Reference to TextMeshPro for the year
    public TextMeshProUGUI policyText;  // Reference to TextMeshPro for the policy name
    public TextMeshProUGUI descriptionText;  // Reference to TextMeshPro for the description
    public TextMeshProUGUI effectsText;  // Reference to TextMeshPro for the effects

    public Stats statsScript;  // Reference to the Stats script component
    private string[] rows; // Array to store rows of data
    private int currentRow = 1; // Index of the current row being displayed
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
            ProcessData(request.downloadHandler.text);
        }
    }

    void ProcessData(string csvData)
    {
        rows = csvData.Split('\n');
        DisplayCurrentRow();
    }

    void Update()
    {
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            ApprovePolicy();
        }
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            NextRow();
        }
    }

    void ApprovePolicy()
    {
        if (currentRow < rows.Length)
        {
            string[] columns = rows[currentRow].Split(',');
            float peopleEffect = float.Parse(columns[3]);
            float economyEffect = float.Parse(columns[4]);
            float ecosystemEffect = float.Parse(columns[5]);
            statsScript.ApplyPolicyEffects(peopleEffect, economyEffect, ecosystemEffect);
            NextRow();  // Move to the next row after applying the policy effects
        }
    }

    void NextRow()
    {
        if (currentRow < rows.Length - 1)
        {
            currentRow++;
        } else {
            currentRow = 1; // Loop back to the first row if at the end
        }
        DisplayCurrentRow();
    }

    void DisplayCurrentRow()
    {
        if (currentRow < rows.Length)
        {
            string[] columns = rows[currentRow].Split(',');
            yearText.text = $"Year: {columns[0]}";
            policyText.text = $"Policy: {columns[1]}";
            descriptionText.text = $"Description: {columns[2]}";
            effectsText.text = $"Effects:\n" +
                           $"People: {columns[3].Trim()}%\n" +
                           $"Economy: {columns[4].Trim()}%\n" +
                           $"Ecosystem: {columns[5].Trim()}%";
        }
    }
}
