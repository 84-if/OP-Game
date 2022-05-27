using System;
using System.Collections.Generic;
using Dialogues;
using Player;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Trigger
{
    public class RhythmGameTrigger : MonoBehaviour
    {
        private readonly List<string> _objectsToEnable = new() {"Rhythm Game Camera", "Track", "Buttons", "ScoreText", 
            "MultiplierText", "EventSystem", "NoteHolder", "GameManager", "GamesPlus"};

        private void OnTriggerStay2D (Collider2D other)
        {
            var dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<Image>();
            var dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<TextMeshProUGUI>();
            var textScript = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<DialogueSystem>();
            var mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            var girl = GameObject.FindGameObjectWithTag("Girl");
            var player = GameObject.FindGameObjectWithTag("Player");
            if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
            {
                var movementScript = other.GetComponent<Movement>();
                if (!movementScript._looksRight)
                    movementScript.Flip();
                // movementScript.enabled = false;
                dialogueWindow.enabled = true;
                dialogueText.enabled = true;
                textScript.enabled = true;
                textScript.Invoke("Start", 0);
                textScript.lines = new[]
                {
                    "Я не знаю, как ты это делаешь, но это работает. Привет",
                    "Ну приветик",
                    "Я не фотограф, но легко могу представить нас вместе",
                    "Ахахаха",
                    "Потанцуем?",
                    "Ну давай!"
                };
            }
            if (other.CompareTag("Player") && textScript.isTalking == false)
            {
                StartRhythmGame();
            }
        }

        private void StartRhythmGame()
        {
            var dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<Image>();
            var dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<TextMeshProUGUI>();
            var textScript = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<DialogueSystem>();
            var mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            var girl = GameObject.FindGameObjectWithTag("Girl");
            var player = GameObject.FindGameObjectWithTag("Player");
            var movementScript = player.GetComponent<Movement>();
            movementScript.enabled = false;
            mainCamera.enabled = false;
            textScript.isTalking = true;
            dialogueWindow.enabled = false;
            dialogueText.enabled = false;
            textScript.enabled = false;
            player.gameObject.transform.position = new Vector3(1.657f, -0.08638373f, 0f);
            girl.gameObject.transform.position = new Vector3(2.565f, -0.08638373f, 0f);
            girl.GetComponent<SpriteRenderer>().flipX = true;
            girl.GetComponent<Animator>().Play("GirlDance");
            player.GetComponent<Animator>().Play("PlayerDance");
            foreach (var objectTag in _objectsToEnable)
            {
                switch (objectTag)
                {
                    case "Rhythm Game Camera":
                        var gameCamera = GameObject.FindGameObjectWithTag(objectTag).GetComponent<Camera>();
                        gameCamera.enabled = true;
                        break;
                    case "Track":
                        var track = GameObject.FindGameObjectWithTag(objectTag).GetComponent<AudioSource>();
                        track.enabled = true;
                        break;
                    case "Buttons":
                        var buttons = GameObject.FindGameObjectWithTag(objectTag).GetComponent<Transform>();
                        buttons.transform.position = new Vector3(3.39f, -0.3799999f, 50f);
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
                        var eventSystem = GameObject.FindGameObjectWithTag(objectTag)
                            .GetComponent<EventSystem>();
                        var standaloneInpModule = GameObject.FindGameObjectWithTag(objectTag)
                            .GetComponent<StandaloneInputModule>();
                        eventSystem.enabled = true;
                        standaloneInpModule.enabled = true;
                        break;
                    case "NoteHolder":
                        var noteHolder = GameObject.FindGameObjectWithTag(objectTag)
                            .GetComponent<BeatScroller>();
                        noteHolder.enabled = true;
                        break;
                    case "GameManager":
                        var gameManager = GameObject.FindGameObjectWithTag(objectTag)
                            .GetComponent<GameManager>();
                        gameManager.enabled = true;
                        break;
                    case "GamesPlus":
                        var gamesPlus = GameObject.FindGameObjectWithTag(objectTag).GetComponent<AudioSource>();
                        gamesPlus.enabled = true;
                        break;
                }
            }
        }
    }
}
