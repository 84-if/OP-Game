using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Trigger
{
    public class RhythmGameTrigger : MonoBehaviour
    {
        private readonly List<string> _objectsToEnable = new List<string> 
        {"Rhythm Game Camera", "Track", "Buttons", "ScoreText", "MultiplierText", "EventSystem", "NoteHolder", "GameManager", "GamesPlus"};

        private void OnTriggerStay2D (Collider2D other)
        {
            if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
            {
                var movementScript = other.GetComponent<Movement>();
                var mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
                var girl = GameObject.FindGameObjectWithTag("Girl");
                if (!movementScript._looksRight)
                    movementScript.Flip();
                movementScript.enabled = false;
                mainCamera.enabled = false;
                other.gameObject.transform.position = new Vector3(1.657f, -0.08638373f, 0f);
                girl.gameObject.transform.position = new Vector3(2.565f, -0.08638373f, 0f);
                girl.GetComponent<SpriteRenderer>().flipX = true;
                girl.GetComponent<Animator>().Play("GirlDance");
                other.GetComponent<Animator>().Play("PlayerDance");
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
                            var eventSystem = GameObject.FindGameObjectWithTag(objectTag).GetComponent<EventSystem>();
                            var standaloneInpModule = GameObject.FindGameObjectWithTag(objectTag)
                                .GetComponent<StandaloneInputModule>();
                            eventSystem.enabled = true;
                            standaloneInpModule.enabled = true;
                            break;
                        case "NoteHolder":
                            var noteHolder = GameObject.FindGameObjectWithTag(objectTag).GetComponent<BeatScroller>();
                            noteHolder.enabled = true;
                            break;
                        case "GameManager":
                            var gameManager = GameObject.FindGameObjectWithTag(objectTag).GetComponent<GameManager>();
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
}
