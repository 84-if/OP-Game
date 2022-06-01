using Dialogues;
using MainCamera;
using UnityEngine;
using TMPro;
using Player;
using UnityEngine.UI;

namespace Trigger
{
    public class Trigger : MonoBehaviour
    {
        private Camera _ourCamera;
        public Chase chase;

        private void Start()
        {
            _ourCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            chase = _ourCamera.GetComponent<Chase>();
        }

        // void OnT
        private void OnTriggerStay2D (Collider2D other)
        {
            var textScript = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<DialogueSystem>();
            var dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<Image>();
            var dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<TextMeshProUGUI>();
            var dialogueNames = GameObject.FindGameObjectWithTag("DialogueNames").GetComponent<TextMeshProUGUI>();
            var player = GameObject.FindGameObjectWithTag("Player");
            var eButton = GameObject.FindGameObjectWithTag("E Button").GetComponent<SpriteRenderer>();
            var movementScript = player.GetComponent<Movement>();
            if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
            {
                eButton.enabled = false;
                if(movementScript._looksRight)
                    movementScript.Flip();
                player.GetComponent<Animator>().Play("PlayerIdle");
                movementScript.enabled = false;
                dialogueWindow.enabled = true;
                dialogueText.enabled = true;
                dialogueNames.enabled = true;
                textScript.enabled = true;
                textScript.lines = new [] {"Саша", "Мам, я пошел", "Мама", "Давай, сынок", "Саша", "Пап.. Можно машину взять? Я тихонько.",
                  "Папа",   "Жирно будет", "Саша", "Ну ладно", "Мама", "Будь аккуратнее, не задерживайся.", "Саша", "Да да да…"};
                
            }
            if(other.CompareTag("Player") && !textScript.isTalking)
            {
                movementScript.enabled = true;
                textScript.isTalking = true;
                dialogueWindow.enabled = false;
                dialogueText.enabled = false;
                dialogueNames.enabled = false;
                textScript.enabled = false;
                player.gameObject.transform.position = new Vector3(1.194f, -0.063f, 0f);
                chase.leftLimit = 1.009f + 0.9575f;
                chase.rightLimit = 5.17f - 0.958f;
                chase.upperLimit = 0.5f;
                chase.bottomLimit = -0.028f;
            }
        }  
    }
}
