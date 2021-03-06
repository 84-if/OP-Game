using System;
using System.Collections.Generic;
using Blackout;
using Dialogues;
using Player;
using Rhythm_Game.Scripts;
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
            "NoteHolder", "GameManager", "GamesPlus"};
        private bool Flag = true;

        private void OnTriggerStay2D (Collider2D other)
        {
            var blackout = GameObject.FindGameObjectWithTag("Blackout").GetComponent<BlackoutMethod>();
            var dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<Image>();
            var dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<TextMeshProUGUI>();
            var dialogueNames = GameObject.FindGameObjectWithTag("DialogueNames").GetComponent<TextMeshProUGUI>();
            var textScript = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<DialogueSystem>();
            var girl = GameObject.FindGameObjectWithTag("Girl");
            var player = GameObject.FindGameObjectWithTag("Player");
            var eButton = GameObject.FindGameObjectWithTag("E Button").GetComponent<SpriteRenderer>();
            var movementScript = other.GetComponent<Movement>();
            if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && Flag)
            {
                blackout.doneDarken = false;
                eButton.enabled = false;
                if(movementScript._looksRight && player.transform.position.x > girl.transform.position.x ||
                   !movementScript._looksRight && player.transform.position.x < girl.transform.position.x)
                    movementScript.Flip();
                player.GetComponent<Animator>().Play("PlayerIdle");
                movementScript.enabled = false;
                dialogueWindow.enabled = true;
                dialogueText.enabled = true;
                dialogueNames.enabled = true;
                textScript.enabled = true;
                textScript.Invoke("Start", 0);
                textScript.lines = new[]
                {
                    "????????", "?? ???? ????????, ?????? ???? ?????? ??????????????, ???? ?????? ????????????????. ????????????!",
                    "????????", "???? ????????????????!",
                    "????????", "?? ???? ????????????????, ???? ?????????? ???????? ?????????????????????? ?????? ????????????.",
                    "????????", "??????????????",
                    "????????", "???? ???? ????????????????, ???? ?? ???? ?????????????????? ?????? ???? ??????????????????.",
                    "????????", "????, ???? ???????????? ??????. ?????? ???????? ?????????? ???? ?????????",
                    "????????", "???????? ??.",
                    "????????", "?? ????????, ???????????? ??????, ???????? ???????????? ????.",
                };
                Flag = false;
            }
            if (other.CompareTag("Player") && textScript.isTalking == false)
            {
                if (!movementScript._looksRight)
                    movementScript.Flip();
                StartRhythmGame();
            }
        }

        private void StartRhythmGame()
        {
            var dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<Image>();
            var dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<TextMeshProUGUI>();
            var dialogueNames = GameObject.FindGameObjectWithTag("DialogueNames").GetComponent<TextMeshProUGUI>();
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
            dialogueNames.enabled = false;
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
