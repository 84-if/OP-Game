using System.Collections.Generic;
using Dialogues;
using Player;
using TMPro;
using Trigger;
using UnityEngine;
using UnityEngine.EventSystems;
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
    
    private readonly List<string> _objectsToDisable = new() {"Rhythm Game Trigger", "Rhythm Game Camera", 
        "Track", "Buttons", "ScoreText", "MultiplierText", "NoteHolder", "GameManager", "GamesPlus"};
    
    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0";
        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;
    }

    void Update()
    {
        if (!startPlaying)
        {
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
                foreach (var objectTag in _objectsToDisable)
                {
                    switch (objectTag)
                    {
                        case "Rhythm Game Trigger":
                            var gameTrigger = GameObject.FindGameObjectWithTag(objectTag).GetComponent<BoxCollider2D>();
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
                        case "MultiplierText":
                            var multiplierText = GameObject.FindGameObjectWithTag(objectTag).GetComponent<Text>();
                            multiplierText.enabled = false;
                            break;
                        case "NoteHolder":
                            var noteHolder = GameObject.FindGameObjectWithTag(objectTag).GetComponent<BeatScroller>();
                            noteHolder.enabled = false;
                            break;
                        case "GameManager":
                            var gameManager = GameObject.FindGameObjectWithTag(objectTag).GetComponent<GameManager>();
                            gameManager.enabled = false;
                            break;
                        case "GamesPlus":
                            var gamesPlus = GameObject.FindGameObjectWithTag(objectTag).GetComponent<AudioSource>();
                            gamesPlus.enabled = false;
                            break;
                    }
                }
                
                theMusic.ChangeBGM(partyMusic);
                theMusic.BGM.Play();
                var audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();
                audioManager.loop = true;
                var player = GameObject.FindGameObjectWithTag("Player");
                var mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
                var trigger = GameObject.FindGameObjectWithTag("Rhythm Game Trigger").GetComponent<BoxCollider2D>();
                trigger.enabled = false;
                // player.GetComponent<Movement>().enabled = true;
                mainCamera.enabled = true;
                var dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<Image>();
                var dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<TextMeshProUGUI>();
                var textScript = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<DialogueSystem>();
                var jopaTrigger = GameObject.FindGameObjectWithTag("JopaTrigger").GetComponent<JopaTrigger>();
                dialogueWindow.enabled = true;
                dialogueText.enabled = true;
                textScript.enabled = true;
                textScript.Invoke("Start", 0);
                textScript.lines = new[]
                {
                    "А ты вроде ничего. Пойдём, у меня там друзья."
                };
                

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
