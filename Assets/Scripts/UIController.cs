using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.IO;
using System.Linq;

public class UIController : MonoBehaviour
{
    private int death = 0;
    private float timer;
    public TextMeshProUGUI deathText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI deathCountText;
     public TextMeshProUGUI timeRecordText;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        deathText.text = "Death " + death.ToString();
        timer += Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(timer);
        timerText.text = time.Hours.ToString() + ":" + time.Minutes.ToString() +":" + time.Seconds.ToString();
    }

    public void PauseGame(){
        Time.timeScale = 0;
    }

    public void ResumeGame(){
        Time.timeScale = 1;
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void Death(){
        death++;
    }

    public void Summary(){
        deathCountText.text = "Death Count: " + death.ToString();
        timeRecordText.text = "Time: " + timerText.text;
        recordStat();
    }

    private void recordStat(){
        string filepath = "Assets/Prefabs/records.txt";
        List<string> records = File.ReadAllLines(filepath).ToList();
        string temp = deathCountText.text + "   " + timeRecordText.text;

        for (int i = 0; i < records.Count; i++){
            //if there is empty record, added new record
            if(records[i] == "Empty"){
                records[i] = temp;
                temp = "";
                break;
            }
        }

        //else, compare current record to the last record and see if it lower
        if(temp.CompareTo(records[9]) < 0 && temp != ""){
            records[9] = temp;    
        }

        //sort all record and fill it in
        records.Sort();
        File.WriteAllLines(filepath, records);
    }
}
