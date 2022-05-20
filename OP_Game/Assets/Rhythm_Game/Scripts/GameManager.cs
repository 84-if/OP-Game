using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;

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

    private readonly List<string> _objectsToDisable = new List<string> 
        {"Rhythm Game Camera", "Track", "Buttons", "ScoreText", "MultiplierText", "EventSystem", "NoteHolder", "GameManager", "GamesPlus"};
    
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
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;

                theMusic.Play();
            }
        }
        else
        {
            if (!theMusic.isPlaying && !resultsScreen.activeInHierarchy)
            {
                // resultsScreen.SetActive(true);
                foreach (var objectTag in _objectsToDisable)
                {
                    switch (objectTag)
                    {
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
                            scoreText.enabled = true;
                            break;
                        case "MultiplierText":
                            var multiplierText = GameObject.FindGameObjectWithTag(objectTag).GetComponent<Text>();
                            multiplierText.enabled = true;
                            break;
                        case "EventSystem":
                            var eventSystem = GameObject.FindGameObjectWithTag(objectTag).GetComponent<EventSystem>();
                            var standaloneInpModule = GameObject.FindGameObjectWithTag(objectTag)
                                .GetComponent<StandaloneInputModule>();
                            eventSystem.enabled = false;
                            standaloneInpModule.enabled = false;
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
                var player = GameObject.FindGameObjectWithTag("Player");
                var mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
                player.GetComponent<Movement>().enabled = true;
                mainCamera.enabled = true;

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

        multiText.text = "Multiplier: x" + currentMultiplier;
        
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
        
        multiText.text = "Multiplier: x" + currentMultiplier;

        missedHits++;
    }
}
