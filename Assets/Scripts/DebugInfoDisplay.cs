using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugInfoDisplay : MonoBehaviour
{
    [SerializeField]
    TMP_Text logField;
    [SerializeField]
    TMP_Text fpsField;
    [SerializeField, Range(1, 100)]
    int logQueueSize = 10;
    [SerializeField, Range(0.05f, 1f)]
    float fpsUpdateInterval = 0.1f;

    float deltaT = 0f;
    int frameCount = 0;

    Queue<string> logs = new Queue<string>();

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string message, string stackTrace, LogType type)
    {
        logs.Enqueue(message);
        if (logs.Count > logQueueSize) logs.Dequeue();
    }

    private void Update()
    {
        logField.text = "";
        foreach (var log in logs)
        {
            logField.text += log + "\n";
        }

        deltaT += Time.deltaTime;
        frameCount++;

        if (deltaT >= fpsUpdateInterval)
        {
            fpsField.text = ((float)frameCount / (deltaT + Mathf.Epsilon)).ToString();
            frameCount = 0;
            deltaT = 0f;
        }
    }
}
