using Dialogues;
using MainCamera;
using UnityEngine;
using TMPro;
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

        private void OnTriggerStay2D (Collider2D other)
        {
            if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
            {
                var dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<Image>();
                var dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<TextMeshProUGUI>();
                var textScript = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<DialogueSystem>();
                dialogueWindow.enabled = true;
                dialogueText.enabled = true;
                textScript.enabled = true;
                textScript.lines = new [] {"Мам, я пошел", "Давай, сынок", "Пап.. Можно машину взять? Я тихонько.",
                    "Жирно будет", "Ну ладно", "Будь аккуратнее, не задерживайся.", "Да да да…"};
                if (!textScript.isTalking)
                {
                    dialogueWindow.enabled = false;
                    dialogueText.enabled = false;
                    textScript.enabled = false;
                    other.gameObject.transform.position = new Vector3(1.194f, -0.063f, 0f);
                    chase.leftLimit = 1.009f + 0.9575f;
                    chase.rightLimit = 5.17f - 0.958f;
                    chase.upperLimit = 0.5f;
                    chase.bottomLimit = -0.038f;
                }
            }
        }  
    }
}
