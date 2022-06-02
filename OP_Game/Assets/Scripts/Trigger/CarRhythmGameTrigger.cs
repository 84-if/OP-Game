using System;
using System.Collections.Generic;
using Blackout;
using Car;
using Dialogues;
using Player;
using Rhythm_Game.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Trigger
{
    public class CarRhythmGameTrigger : MonoBehaviour
    {
        private readonly List<string> _objectsToEnable = new() {"Horizontal Audio", "Horizontal Buttons", 
            "Horizontal ScoreText", "Horizontal NoteHolder", "Horizontal GameManager", "Horizontal JamesPlus"};
        
        public AudioManager theMusic;
        public AudioClip roadMusic;
        
        private bool Flag = true;

        private void OnTriggerStay2D (Collider2D other)
        {
            var dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<Image>();
            var dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<TextMeshProUGUI>();
            var dialogueNames = GameObject.FindGameObjectWithTag("DialogueNames").GetComponent<TextMeshProUGUI>();
            var blackout = GameObject.FindGameObjectWithTag("Blackout").GetComponent<BlackoutMethod>();
            var textScript = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<DialogueSystem>();
            var carMover = GameObject.FindGameObjectWithTag("Car").GetComponent<Mover>();
            carMover.enabled = false;
            var flag = GameObject.FindGameObjectWithTag("jopa").GetComponent<SpriteRenderer>();
            if(other.CompareTag("Car") && flag.enabled && Flag)
            {
                blackout.doneDarken = false;
                theMusic.ChangeBGM(roadMusic);
                dialogueWindow.enabled = true;
                dialogueText.enabled = true;
                dialogueNames.enabled = true;
                textScript.enabled = true;
                textScript.Invoke("Start", 0);
                textScript.lines = new[]
                {
                    "Кирюха", "Тачка зачёт!",
                    "Лиза", "Музыку, музыку погромче включите!",
                    "Серёга", "Поддай газку, дружище! Чо еле плетёмся?!",
                    "Кирюха", "Серёга, бутылку передай!",
                    "Саша", "Ребят, не толкайтесь только...",
                };
                flag.enabled = false;
                Flag = false;
            }
            if (other.CompareTag("Car") && !textScript.isTalking)
            {
                GetComponent<Collider2D>().enabled = false;
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().Direction = 1;
                GameObject.FindGameObjectWithTag("Horizontal GameManager").GetComponent<GameManager>().Direction = 1;
                StartCarRhythmGame();
            }
        }
        private void StartCarRhythmGame()
        {
            var dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<Image>();
            var dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<TextMeshProUGUI>();
            var dialogueNames = GameObject.FindGameObjectWithTag("DialogueNames").GetComponent<TextMeshProUGUI>();
            var textScript = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<DialogueSystem>();
            var roadScroll = GameObject.FindGameObjectWithTag("Road").GetComponent<Scroll>();
            var carMovement = GameObject.FindGameObjectWithTag("Car").GetComponent<CarMovement>();
            carMovement.enabled = true;
            roadScroll.speed = 0.7f;
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
                        break;
                    case "Horizontal Buttons":
                        var buttons = GameObject.FindGameObjectWithTag(objectTag).GetComponent<Transform>();
                        buttons.transform.position = new Vector3(buttons.transform.position.x, 
                            buttons.transform.position.y, 0f);
                        break;
                    case "Horizontal ScoreText":
                        var scoreText = GameObject.FindGameObjectWithTag(objectTag).GetComponent<Text>();
                        scoreText.enabled = true;
                        break;
                    case "Horizontal NoteHolder":
                        var noteHolder = GameObject.FindGameObjectWithTag(objectTag)
                            .GetComponent<BeatScroller>();
                        noteHolder.enabled = true;
                        break;
                    case "Horizontal GameManager":
                        var gameManager = GameObject.FindGameObjectWithTag(objectTag).GetComponent<GameManager>();
                        gameManager.enabled = true;
                        break;
                    case "Horizontal JamesPlus":
                        var gamesPlus = GameObject.FindGameObjectWithTag(objectTag).GetComponent<AudioSource>();
                        gamesPlus.enabled = true;
                        break;
                }
            }
        }
    }
}
