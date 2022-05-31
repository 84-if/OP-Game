using System.Collections.Generic;
using Dialogues;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Trigger
{
    public class CarRhythmGameTrigger : MonoBehaviour
    {
        private readonly List<string> _objectsToEnable = new() {"Horizontal Audio", "Horizontal Buttons", 
            "Horizontal ScoreText", "Horizontal NoteHolder", "Horizontal GameManager", "Horizontal JamesPlus"};
        private void OnTriggerStay2D (Collider2D other)
        {
            var dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<Image>();
            var dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<TextMeshProUGUI>();
            var dialogueNames = GameObject.FindGameObjectWithTag("DialogueNames").GetComponent<TextMeshProUGUI>();
            var textScript = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<DialogueSystem>();
            var flag = GameObject.FindGameObjectWithTag("jopa").GetComponent<SpriteRenderer>();
            if(other.CompareTag("Car"))
                Debug.Log("Vfibyf в триггере");
            if(other.CompareTag("Car") && flag.enabled)
            {
                dialogueWindow.enabled = true;
                dialogueText.enabled = true;
                dialogueNames.enabled = true;
                textScript.enabled = true;
                textScript.Invoke("Start", 0);
                textScript.lines = new[]
                {
                    "Кирюха", "Тачка зачёт!",
                    "Серёга", "Поддай газку, дружище! Чо еле плетёмся?!"
                };
                flag.enabled = false;
            }
            if (other.CompareTag("Car") && !textScript.isTalking)
            {
                StartRhythmGame();
            }
        }
        private void StartRhythmGame()
        {
            Debug.Log("fuck it");
            var audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();
            var dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<Image>();
            var dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<TextMeshProUGUI>();
            var dialogueNames = GameObject.FindGameObjectWithTag("DialogueNames").GetComponent<TextMeshProUGUI>();
            var textScript = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<DialogueSystem>();
            audioManager.loop = false;
            textScript.isTalking = true;
            dialogueWindow.enabled = false;
            dialogueText.enabled = false;
            dialogueNames.enabled = false;
            textScript.enabled = false;
            foreach (var objectTag in _objectsToEnable)
            {
                switch (objectTag)
                {
                    case "Horizontal Audio":
                        var track = GameObject.FindGameObjectWithTag(objectTag).GetComponent<AudioSource>();
                        track.enabled = true;
                        Debug.Log("Enable audio");
                        break;
                    case "Horizontal Buttons":
                        var buttons = GameObject.FindGameObjectWithTag(objectTag).GetComponent<Transform>();
                        buttons.transform.position = new Vector3(buttons.transform.position.x, 
                            buttons.transform.position.y, 0f);
                        Debug.Log("Enable buttons");
                        break;
                    case "Horizontal ScoreText":
                        var scoreText = GameObject.FindGameObjectWithTag(objectTag).GetComponent<Text>();
                        scoreText.enabled = true;
                        Debug.Log("Enable scoretext");
                        break;
                    case "Horizontal NoteHolder":
                        var noteHolder = GameObject.FindGameObjectWithTag(objectTag)
                            .GetComponent<BeatScroller>();
                        noteHolder.enabled = true;
                        Debug.Log("Enable noteholder");
                        break;
                    case "Horizontal GameManager":
                        var gameManager = GameObject.FindGameObjectWithTag(objectTag).GetComponent<GameManager>();
                        gameManager.enabled = true;
                        Debug.Log("Enable gamemanager");

                        break;
                    case "Horizontal JamesPlus":
                        var gamesPlus = GameObject.FindGameObjectWithTag(objectTag).GetComponent<AudioSource>();
                        gamesPlus.enabled = true;
                        Debug.Log("Enable jamesplus");

                        break;
                }
            }
        }
    }
}
