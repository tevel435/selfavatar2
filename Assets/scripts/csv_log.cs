using UnityEngine;
using System.IO;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class CSVLogger : MonoBehaviour
{
    DateTime startTime;
    string outputFileName;
    WaitForSeconds loggingInterval = new WaitForSeconds(0.5f); 
    private string logEntry;
    private character_swap characterSwap;
    private audio_play_right right_answer;
    private audio_player left_answer;


    void Start()
    {
        startTime = DateTime.Now;
        outputFileName = "log_" + startTime.ToString("yyyy-MM-dd_HH-mm-ss")+ "_" + SceneManager.GetActiveScene().name + ".csv";
        File.AppendAllText(Path.Combine(Application.persistentDataPath, outputFileName), "time,x,y,z, left charachter, right character, shoko, vanil" + Environment.NewLine);
        characterSwap = FindObjectOfType<character_swap>();
        StartCoroutine(UpdateLogFile());
    }


    IEnumerator UpdateLogFile()
    {
        while (true)
        {
            string leftCharacterName = characterSwap.leftCharacterInstance != null ? characterSwap.leftCharacterInstance.name : "None";
            string rightCharacterName = characterSwap.rightCharacterInstance != null ? characterSwap.rightCharacterInstance.name : "None";
            //right_answer = FindObjectOfType<audio_play_right>(); 
            //left_answer = FindObjectOfType<audio_player>(); 
            //string counter_yes = (right_answer.counter_yes + left_answer.counter_yes).ToString();
            //string counter_no = (right_answer.counter_no + left_answer.counter_no).ToString();

            string total_shoko = characterSwap != null ? characterSwap.total_shoko.ToString() : "null";
            string total_vanil = characterSwap != null ? characterSwap.total_vanil.ToString() : "null";
            logEntry = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
            Math.Round(transform.position.x, 2).ToString("0.00"), // Limit to 2 decimal points
            Math.Round(transform.position.y, 2).ToString("0.00"), // Limit to 2 decimal points
            Math.Round(transform.position.z, 2).ToString("0.00"), // Limit to 2 decimal points
            leftCharacterName, rightCharacterName, total_shoko, total_vanil);
            Debug.Log(logEntry);
            File.AppendAllText(Path.Combine(Application.persistentDataPath, outputFileName), logEntry + Environment.NewLine);
            yield return loggingInterval;
        }
    }
}
