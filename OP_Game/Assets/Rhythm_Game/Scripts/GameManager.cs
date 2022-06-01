using System;
using System.Collections.Generic;
using Dialogues;
using Player;
using Rhythm_Game.Scripts;
using TMPro;
using Trigger;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ParticleSystemJobs;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioClip danceMusic;
    public AudioManager theMusic;
    public AudioClip partyMusic;

    public bool startPlaying;

    public BeatScroller theBS;

    public static GameManager instance;

    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public Text scoreText;
    public Text multiText;

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public GameObject resultsScreen;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;

    public bool danceOver;

    public int Direction = 0;

    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0";
        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;
    }

    void Update()
    {
        var audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();
        if (!startPlaying)
        {
            audioManager.loop = false;
            startPlaying = true;
            theBS.hasStarted = true;
            theMusic.ChangeBGM(danceMusic);
            theMusic.BGM.Play();
        }
        else
        {
            if (!theMusic.BGM.isPlaying)
            {
                // resultsScreen.SetActive(true);
                if (Direction == 0)
                {
                    var objectsToDisable = new List<string>{"Rhythm Game Trigger", "Rhythm Game Camera", 
                    "Track", "Buttons", "ScoreText", "MultiplierText", "NoteHolder", "GameManager", "GamesPlus"};
                    foreach (var objectTag in objectsToDisable)
                    {
                        switch (objectTag)
                        {
                            case "Rhythm Game Trigger":
                                var gameTrigger = GameObject.FindGameObjectWithTag(objectTag)
                                    .GetComponent<BoxCollider2D>();
                                gameTrigger.enabled = false;
                                break;
                            case "Rhythm Game Camera":
                                var gameCamera = GameObject.FindGameObjectWithTag(objectTag).GetComponent<Camera>();
                                gameCamera.enabled = false;
                                break;
                            case "Track":
                                var track = GameObject.FindGameObjectWithTag(objectTag).GetComponent<AudioSource>();
                                track.enabled = false;
                                break;
                            case "Buttons":
                                var buttons = GameObject.FindGameObjectWithTag(objectTag).GetComponent<Transform>();
                                buttons.transform.position = new Vector3(3.39f, -0.3799999f, -50f);
                                break;
                            case "ScoreText":
                                var scoreText = GameObject.FindGameObjectWithTag(objectTag).GetComponent<Text>();
                                scoreText.enabled = false;
                                break;
                            case "NoteHolder":
                                var noteHolder = GameObject.FindGameObjectWithTag(objectTag)
                                    .GetComponent<BeatScroller>();
                                noteHolder.enabled = false;
                                break;
                            case "GameManager":
                                var gameManager = GameObject.FindGameObjectWithTag(objectTag)
                                    .GetComponent<GameManager>();
                                gameManager.enabled = false;
                                break;
                            case "GamesPlus":
                                var gamesPlus = GameObject.FindGameObjectWithTag(objectTag).GetComponent<AudioSource>();
                                gamesPlus.enabled = false;
                                break;
                        }
                    }
                    var dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<Image>();
                    var dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<TextMeshProUGUI>();
                    var textScript = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<DialogueSystem>();
                    var dialogueNames = GameObject.FindGameObjectWithTag("DialogueNames").GetComponent<TextMeshProUGUI>();
                    var mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
                    var boysDialogueTrigger = GameObject.FindGameObjectWithTag("BoysDialogueTrigger")
                        .GetComponent<BoxCollider2D>();
                    GameObject.FindGameObjectWithTag("Rhythm Game Trigger").SetActive(false);
                    boysDialogueTrigger.enabled = true;
                    mainCamera.enabled = true;
                    dialogueWindow.enabled = true;
                    dialogueText.enabled = true;
                    dialogueNames.enabled = true;
                    textScript.enabled = true;
                    textScript.Invoke("Start", 0);
                    textScript.lines = new[]
                    {
                        "Лиза", "А ты вроде ничего. Пойдём, у меня там друзья."
                    };
                    theMusic.ChangeBGM(partyMusic);
                    theMusic.BGM.Play();
                }
                else if (Direction == 1)
                {
                    var objectsToDisable = new List<string> {"Horizontal Audio", "Horizontal Buttons", 
                        "Horizontal ScoreText", "Horizontal NoteHolder", "Horizontal GameManager", 
                        "Horizontal JamesPlus"};
                    foreach (var objectTag in objectsToDisable)
                    {
                        switch (objectTag)
                        {
                            case "Horizontal Audio":
                                var track = GameObject.FindGameObjectWithTag(objectTag).GetComponent<AudioSource>();
                                track.enabled = false;
                                break;
                            case "Horizontal Buttons":
                                var buttons = GameObject.FindGameObjectWithTag(objectTag).GetComponent<Transform>();
                                buttons.transform.position = new Vector3(buttons.transform.position.x, 
                                    buttons.transform.position.y, 0f);
                                break;
                            case "Horizontal ScoreText":
                                var scoreText = GameObject.FindGameObjectWithTag(objectTag).GetComponent<Text>();
                                scoreText.enabled = false;
                                break;
                            case "Horizontal NoteHolder":
                                var noteHolder = GameObject.FindGameObjectWithTag(objectTag)
                                    .GetComponent<BeatScroller>();
                                noteHolder.enabled = false;
                                break;
                            case "Horizontal GameManager":
                                var gameManager = GameObject.FindGameObjectWithTag(objectTag).GetComponent<GameManager>();
                                gameManager.enabled = false;
                                break;
                            case "Horizontal JamesPlus":
                                var gamesPlus = GameObject.FindGameObjectWithTag(objectTag).GetComponent<AudioSource>();
                                gamesPlus.enabled = false;
                                break;
                        }
                    }
                    var dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<Image>();
                    var dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<TextMeshProUGUI>();
                    var textScript = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<DialogueSystem>();
                    var dialogueNames = GameObject.FindGameObjectWithTag("DialogueNames").GetComponent<TextMeshProUGUI>();
                    dialogueWindow.enabled = true;
                    dialogueText.enabled = true;
                    dialogueNames.enabled = true;
                    textScript.enabled = true;
                    textScript.Invoke("Start", 0);
                    textScript.lines = new[]
                    {
                        "Лиза", "Клево, что у тебя машина есть! Сможем теперь вместе кататься!",
                        "Саша", "Да-а, офигенно!",
                        "Кирюха", "Менты!",
                        "Серёга", "Гони давай!!"
                    };
                    
                    
                }
                audioManager.loop = true;

                normalsText.text = "" + normalHits;
                goodsText.text = goodHits.ToString();
                perfectsText.text = perfectHits.ToString();
                missesText.text = "" + missedHits;

                float totalHit = normalHits + goodHits + perfectHits;
                float percentHit = (totalHit / totalNotes) * 100f;

                percentHitText.text = percentHit.ToString("F1") + "%";

                string rankVal = "F";

                if (percentHit > 40)
                {
                    rankVal = "D";
                    if (percentHit > 55)
                    {
                        rankVal = "C";
                        if (percentHit > 70)
                        {
                            rankVal = "B";
                            if (percentHit > 85)
                            {
                                rankVal = "A";
                                if (percentHit > 95)
                                {
                                    rankVal = "S";
                                }
                            }
                        }
                    }
                }

                rankText.text = rankVal;

                finalScoreText.text = currentScore.ToString();
            }
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit On Time");

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        multiText.text = " ";
        
        scoreText.text = "Score: " + currentScore;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
        normalHits++;
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
        goodHits++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        perfectHits++;
    }
    
    public void NoteMissed()
    {
        Debug.Log("Missed Note");

        currentMultiplier = 1;
        multiplierTracker = 0;
        
        multiText.text = " ";

        missedHits++;
    }
}
