using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSelesai : MonoBehaviour
{
    public Text Text_Score, Text_Hightscore;

    public void Start()
    {
        if(Data.DataScore >= PlayerPrefs.GetInt("score"))
        {
            PlayerPrefs.SetInt("score", Data.DataScore);
        }

        Text_Score.text = Data.DataScore.ToString();
        Text_Hightscore.text = PlayerPrefs.GetInt("score").ToString();
    }
}
