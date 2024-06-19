using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PickColorGame : MonoBehaviour
{
    List<string> colors = new List<string> { "black", "blue", "white", "gray", "green", "orange", "pink", "purple", "red", "yellow" };
    List<string> randomColors = new List<string>();
    int score = 0;
    Hashtable directionWithBoxes = new Hashtable();
    string rightAnswerColor, rightAnswerDirection;
    private float timer = 5f;

    int lives = 3;



    void Start()
    {
        InitializeGame();
        SetOnClickListenerForImages();
    }

   
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            CheckUserAwnser("Up");

        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            CheckUserAwnser("Down");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            CheckUserAwnser("Left");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            CheckUserAwnser("Right");

        }

    }

    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            lives--;
            InitializeGame();
        }
        switch (lives)
        {
            case 0:
                GameObject.Find("Heart1").GetComponent<Image>().enabled = false;
                PlayerPrefs.SetInt("FinalScore", score);
                SceneManager.LoadScene("GameOver");
                break;

            case 1:
                GameObject.Find("Heart2").GetComponent<Image>().enabled = false;
                break;

            case 2:
                GameObject.Find("Heart3").GetComponent<Image>().enabled = false;
                break;

        }
    }

    void InitializeGame()
    {
        timer = 5f;
        randomColors.Clear();
        directionWithBoxes.Clear();
        SetRandomBoxImages();
        SetColorText();
        IndicateRightAnswer();
    }

    void CheckUserAwnser(string userAwnser)
    {
        if (rightAnswerDirection == userAwnser)
        {
            PlayAudioSource("correct_answer");
            UpdateScore();
            InitializeGame();
        }
        else
        {
            PlayAudioSource("incorrect_answer");
            lives--;
        }

    }

    void SetOnClickListenerForImages()
    {
        GameObject.Find("Upbox").GetComponent<Button>().onClick.AddListener(() => CheckUserAwnser("Up"));
        GameObject.Find("Downbox").GetComponent<Button>().onClick.AddListener(() => CheckUserAwnser("Down"));
        GameObject.Find("Rightbox").GetComponent<Button>().onClick.AddListener(() => CheckUserAwnser("Right"));
        GameObject.Find("Leftbox").GetComponent<Button>().onClick.AddListener(() => CheckUserAwnser("Left"));

    }

    void UpdateScore()
    {
        score += 5;
        GameObject.Find("Scoretext").GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

    void SetColorText()
    {
        int randomNumber = GenerateRandomNumber(0, randomColors.Count);
        rightAnswerColor = randomColors[randomNumber];
        GameObject.Find("Colortext").GetComponent<TextMeshProUGUI>().text = rightAnswerColor.ToUpper();
    }

    void SetRandomBoxImages()
    {

        while (randomColors.Count < 4)
        {
            int randomNumber = GenerateRandomNumber(0, colors.Count);
            if (!randomColors.Contains(colors[randomNumber]))
                randomColors.Add(colors[randomNumber]);
        }
            Sprite firstbox = Resources.Load<Sprite>("Sprites/" + randomColors[0] + "_box");
            Sprite secondbox = Resources.Load<Sprite>("Sprites/" + randomColors[1] + "_box");
            Sprite thirdbox = Resources.Load<Sprite>("Sprites/" + randomColors[2] + "_box");
            Sprite fourthbox = Resources.Load<Sprite>("Sprites/" + randomColors[3] + "_box");

            GameObject.Find("Upbox").GetComponent<Image>().sprite = firstbox;
            GameObject.Find("Downbox").GetComponent<Image>().sprite = secondbox;
            GameObject.Find("Rightbox").GetComponent<Image>().sprite = thirdbox;
            GameObject.Find("Leftbox").GetComponent<Image>().sprite = fourthbox;

            directionWithBoxes.Add("Up", randomColors[0]);
            directionWithBoxes.Add("Down", randomColors[1]);
            directionWithBoxes.Add("Right", randomColors[2]);
            directionWithBoxes.Add("Left", randomColors[3]);
        
    }

    void IndicateRightAnswer()
    {
        foreach (DictionaryEntry enrty in directionWithBoxes)
        {
            if (enrty.Value.Equals(rightAnswerColor))
            {
                rightAnswerDirection = enrty.Key.ToString();
            }

        }
    }

    void PlayAudioSource(string audioClipName)
    {
        AudioSource audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("Sounds/" + audioClipName);
        audioSource.Play();

    }

    int GenerateRandomNumber(int firstNumber, int lastNumber)
    {
        System.Random random = new System.Random();
        return random.Next(firstNumber, lastNumber);
    }

    

    

    

}
