using Dialogues;
using MainCamera;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Trigger
{
    public class BoysDialogsTrigger : MonoBehaviour
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
            var dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<Image>();
            var dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<TextMeshProUGUI>();
            var dialogueNames = GameObject.FindGameObjectWithTag("DialogueNames").GetComponent<TextMeshProUGUI>();
            var textScript = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<DialogueSystem>();
            var eButton = GameObject.FindGameObjectWithTag("E Button").GetComponent<SpriteRenderer>();
            var movementScript = player.GetComponent<Movement>();
            if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && Flag)
            {
                eButton.enabled = false;
                if(!movementScript._looksRight)
                    movementScript.Flip();
                player.GetComponent<Animator>().Play("PlayerIdle");
                movementScript.enabled = false;
                dialogueWindow.enabled = true;
                dialogueText.enabled = true;
                dialogueNames.enabled = true;
                textScript.enabled = true;
                textScript.Invoke("Start", 0);
                textScript.lines = new [] {
                    "Серёга", "Ну здарова ёпт",
                    "Саша", "Даров",
                    "Кирюха", "Как жизнь?",
                    "Саша", "Да нормально",
                    "Серёга", "Смотрю, ты ровный тип, не хочешь с нами потусить?",
                    "Саша", "Почему бы и нет",
                    "Серёга", "Слыхали, у тебя родаки машину купили.",
                    "Саша", "Ну да",
                    "Кирюха", "Не прокатишь?",
                    "Саша", "Не знаю, мне отец ключи не дал.",
                    "Серёга", "Дак ты возьми, для друзей не жалко",
                    "Саша", "Да ну не, ребят, нельзя",
                    "Кирюха", "Понятно всё с тобой, очередной лошок",
                    "Саша", "Ладно, пошлите, не лох я"
                };
                Flag = false;
            }
            if (other.CompareTag("Player") && !textScript.isTalking)
            {
                friendOne.GetComponent<SpriteRenderer>().flipX = false;
                friendTwo.GetComponent<SpriteRenderer>().flipX = false;
                friendOne.GetComponent<Mover>().enabled = true;
                friendTwo.GetComponent<Mover>().enabled = true;     
                girl.GetComponent<Mover>().enabled = true;
                girl.GetComponent<SpriteRenderer>().flipX = false;
                girl.GetComponent<Mover>().Invoke("Start", 0);
                girl.GetComponent<Mover>().startPosition = new Vector2(5.152f, -0.08638373f);
                girl.GetComponent<Mover>().endPosition = new Vector2(8.2f, -0.08638373f);
                girl.GetComponent<Mover>().step = 0.0015f;
                movementScript.enabled = true;
                textScript.isTalking = true;
                dialogueWindow.enabled = false;
                dialogueNames.enabled = false;
                dialogueText.enabled = false;
                textScript.enabled = false;
                player.gameObject.transform.position = new Vector3(5.59f, -0.057f, 0f);
                chase.leftLimit = 5.31f + 0.806f;
                chase.rightLimit = 10.421f - 0.81f;
                chase.upperLimit = 0.3f;
            }
        }
    }
}
