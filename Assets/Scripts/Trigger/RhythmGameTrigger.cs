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
        {"Rhythm Game Camera", "Track", "Buttons", "Canvas", "EventSystem", "NoteHolder", "GameManager", "GamesPlus"};
        void Start()
        {
            
        }

        private void OnTriggerStay2D (Collider2D other)
        {
            if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
            {
                var movementScript = other.GetComponent<Movement>();
                var mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
                if (!movementScript._looksRight)
                    movementScript.Flip();
                movementScript.enabled = false;
                mainCamera.enabled = false;
                other.gameObject.transform.position = new Vector3(1.756116f, -0.08638373f, 0f);
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
                        case "Canvas":
                            var canvas = GameObject.FindGameObjectWithTag(objectTag).GetComponent<Canvas>();
                            var canvasScaler = GameObject.FindGameObjectWithTag(objectTag).GetComponent<CanvasScaler>();
                            var graphicRaycaster = GameObject.FindGameObjectWithTag(objectTag)
                                .GetComponent<GraphicRaycaster>();
                            canvas.enabled = true;
                            canvasScaler.enabled = true;
                            graphicRaycaster.enabled = true;
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
