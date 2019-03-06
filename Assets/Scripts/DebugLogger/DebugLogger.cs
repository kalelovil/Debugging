using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public class DebugLogger : MonoBehaviour
{
    public static DebugLogger Instance;

    [Header("Settings")]
    [SerializeField]
    public bool IsLogging = true;
    [SerializeField]
    bool logToUnity = false;
    [SerializeField]
    int lineMaximum = 15;
    [Header ("Components")]
    [SerializeField]
    TMPro.TextMeshProUGUI _text;

    private void Awake()
    {
        if (!IsLogging) Destroy(gameObject);
        else Instance = this;
    }


    public static void LogMessage(string message)
    {
        if (Instance == null)
        {
            Debug.Log(message);
            return;
        }
        AddLine(message);
        if (Instance.logToUnity) Debug.Log(message);
    }

    static void AddLine(string message)
    {               
        Instance._text.text += message + "\n";
        RemoveLines();
    }

    static void RemoveLines()
    {
        int numberOfExcessLines = GetNumberOfLines() - Instance.lineMaximum;
        if (numberOfExcessLines > 0)
        {
            List<string> lines = new List<string>();
            lines.AddRange(Regex.Split(Instance._text.text, "\n"));
            for (int i = 0; i < numberOfExcessLines; i++)
                lines.RemoveAt(i);
            Instance._text.text = "";
            foreach (string line in lines)
            {
                if(line != "")
                    Instance._text.text += line + "\n";
            }
        }        
    }

    static int GetNumberOfLines()
    {
        string text = Instance._text.text;
        
        return Regex.Split(text, "\n").Length;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            Destroy(gameObject);

        if (!IsLogging)
        {
            Destroy(gameObject);
        }
    }
}
