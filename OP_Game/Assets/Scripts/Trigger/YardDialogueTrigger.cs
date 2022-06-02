using System.Threading;
using Dialogues;
using MainCamera;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Blackout;

namespace Trigger
{
    public class YardDialogueTrigger : MonoBehaviour
    {
        private Camera _ourCamera;
        public Chase chase;
        private bool Flag = true;
        

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
            var blackout = GameObject.FindGameObjectWithTag("Blackout").GetComponent<BlackoutMethod>();
            var dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<Image>();
            var dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<TextMeshProUGUI>();
            var dialogueNames = GameObject.FindGameObjectWithTag("DialogueNames").GetComponent<TextMeshProUGUI>();
            var textScript = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<DialogueSystem>();
            var eButton = GameObject.FindGameObjectWithTag("E Button").GetComponent<SpriteRenderer>();
            var movementScript = player.GetComponent<Movement>();
            if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && Flag)
            {
                eButton.enabled = false;
                blackout.doneDarken = false;
                if (!movementScript._looksRight)
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
                    "Саша", "Ребят, я что-то и хочу, и боюсь ключи тащить. Родители дома, я отхвачу, если поймают.",
                    "Серёга", "Да ты чо?!",
                    "Кирюха", "Да мы аккуратно, ничо не будет тебе. Чего тут бояться то.",
                    "Лиза", "Ну Саша, ты же крутой. Если покатаешь, можем потом и в кино сходить.",
                    "Саша", "Ладно. 5 минут и поехали."
                };
                Flag = false;
            }

            if (other.CompareTag("Player") && !textScript.isTalking)
            {
                blackout.Darken();
                if (blackout.doneDarken)
                {
                    dialogueWindow.enabled = false;
                    dialogueText.enabled = false;
                    dialogueNames.enabled = false;
                    textScript.enabled = false;
                    girl.gameObject.transform.position = new Vector3(5.514f, 0.122f, 0f);
                    friendOne.gameObject.transform.position = new Vector3(6.196f, 0.209f, 0f);
                    friendTwo.gameObject.transform.position = new Vector3(5.851f, 0.229f, 0f);
                    player.gameObject.transform.position = new Vector3(8.4f, 0.102f, 0f);
                    friendTwo.GetComponent<SpriteRenderer>().flipX = false;
                    girl.GetComponent<SpriteRenderer>().flipX = false;
                    blackout.doneDarken = false;
                    blackout.Brighten();
                    textScript.isTalking = true;
                    movementScript.enabled = true;
                    GetComponent<BoxCollider2D>().enabled = false;
                    var beforeRideTrigger = GameObject.FindGameObjectWithTag("BeforeRideDialogueTrigger")
                        .GetComponent<BoxCollider2D>();
                    beforeRideTrigger.enabled = true;
                }
                
            }
        }
    }
}