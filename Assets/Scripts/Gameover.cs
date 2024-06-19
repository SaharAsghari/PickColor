using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Gameover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int FinalScore = PlayerPrefs.GetInt("FinalScore", 0);
        GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = FinalScore.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
