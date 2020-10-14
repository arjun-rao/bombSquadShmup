using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public int defusePercent = 0;

    public int timeRemainingSecs = 180;
    public Text time, defuse;
    public static GameManagerScript S;

    private void Awake()
    {
        S = this;
    }

    // Start is called before the first frame update

    void Start()
    {
        InvokeRepeating(nameof(DecreaseTime), 1, 1); // function string, start after float, repeat rate float
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Defuse()
    {
        defusePercent += 5;
        if (defusePercent == 100)
        {
            SceneManager.LoadScene("WonScreen");
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
   
 
    private void DecreaseTime () {
        timeRemainingSecs--;
        time.text = $"{timeRemainingSecs / 60} : {(timeRemainingSecs % 60):00}";
        defuse.text = $"{defusePercent} %";
        if (timeRemainingSecs == 0)
        {
            GameOver();
        }
    }
}
