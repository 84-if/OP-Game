using Dialogues;
using MainCamera;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Trigger
{
    public class YardDialogueTrigger : MonoBehaviour
    {
        private Camera _ourCamera;
        public Chase chase;

        private void Start()
        {
            _ourCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            chase = _ourCamera.GetComponent<Chase>();
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            var girl = GameObject.FindGameObjectWithTag("Girl");
            var friendOne = GameObject.FindGameObjectWithTag("Friend1");
            var friendTwo = GameObject.FindGameObjectWithTag("Friend2");
            var player = GameObject.FindGameObjectWithTag("Player");
            var dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<Image>();
            var dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<TextMeshProUGUI>();
            var textScript = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<DialogueSystem>();
            var movementScript = player.GetComponent<Movement>();
            if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
            {
                if (!movementScript._looksRight)
                    movementScript.Flip();
                player.GetComponent<Animator>().Play("PlayerIdle");
                movementScript.enabled = false;
                dialogueWindow.enabled = true;
                dialogueText.enabled = true;
                textScript.enabled = true;
                textScript.Invoke("Start", 0);
                textScript.lines = new[]
                {
                    "Ребят, я чето и хочу и очкую ключи тащить. Батя дома, мама расстроится если поймает и узнает",
                    "Да чо ты",
                    "Да мы немножко, ничо не будет тебе. Чего тут бояться то",
                    "Ну Саша, ты же крутой. Если покатаешь, можем потом и в кино сходить.",
                    "Держи. Для храбрости",
                    "Ладно. 5 минут и поехали."
                };
                
            }

            if (other.CompareTag("Player") && !textScript.isTalking)
            {

                movementScript.enabled = true;
                textScript.isTalking = true;
                dialogueWindow.enabled = false;
                dialogueText.enabled = false;
                textScript.enabled = false;
            }
        }
    }
}
