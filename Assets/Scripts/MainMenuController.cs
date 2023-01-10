using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public TextMeshProUGUI display;

    void Start() {
        display.text = "";
        string filepath = "Assets/Prefabs/records.txt";
        List<string> records = File.ReadAllLines(filepath).ToList();

        foreach(string line in records){
            display.text += line+"\n";
        }
    }

    public void Play(){
        SceneManager.LoadScene("Game");
    }

    public void Quit(){
        Application.Quit();
        Debug.Log("Game Quited");
    }
}
