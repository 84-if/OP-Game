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
            if(other.CompareTag("Player"))
                Debug.Log("стоит в триггере");
            if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
            {
                if(!movementScript._looksRight)
                    movementScript.Flip();
                player.GetComponent<Animator>().Play("PlayerIdle");
                movementScript.enabled = false;
                dialogueWindow.enabled = true;
                dialogueText.enabled = true;
                textScript.enabled = true;
                textScript.Invoke("Start", 0);
                textScript.lines = new [] {
                    "Ну здарова ёпт",
                    "Даров",
                    "Как жизнь?",
                    "Да нормально",
                    "Смотрю, ты ровный тип, не хочешь с нами потусить?",
                    "Почему бы и нет",
                    "Слыхали, у тебя родаки машину купили.",
                    "Ну да",
                    "Не прокатишь?",
                    "Не знаю, мне отец ключи не дал.",
                    "Дак ты возьми, для друзей не жалко",
                    "Да ну не, ребят, нельзя",
                    "Понятно всё с тобой, очередной лошок",
                    "Ладно, пошлите, не лох я"
                };
                
            }
            if (other.CompareTag("Player") && !textScript.isTalking)
            {
                Debug.Log("Диалог окончен стоит в триггере");
                friendOne.GetComponent<SpriteRenderer>().flipX = false;
                friendTwo.GetComponent<SpriteRenderer>().flipX = false;
                friendOne.GetComponent<Mover>().enabled = true;
                friendTwo.GetComponent<Mover>().enabled = true;
                
                girl.GetComponent<SpriteRenderer>().flipX = false;
                girl.GetComponent<Mover>().Invoke("Start", 0);
                girl.GetComponent<Mover>().startPosition = new Vector2(5.152f, -0.08638373f);
                girl.GetComponent<Mover>().endPosition = new Vector2(8.643f, -0.08638373f);
                girl.GetComponent<Animator>().Play("GirlWalk");
                friendOne.GetComponent<Animator>().Play("Friend1Walk");
                friendTwo.GetComponent<Animator>().Play("Friend2Walk");
                movementScript.enabled = true;
                textScript.isTalking = true;
                dialogueWindow.enabled = false;
                dialogueText.enabled = false;
                textScript.enabled = false;
                player.gameObject.transform.position = new Vector3(5.59f, -0.057f, 0f);
                chase.leftLimit = 5.31f + 0.9575f;
                chase.rightLimit = 10.421f - 0.958f;
            }
        }
    }
}
