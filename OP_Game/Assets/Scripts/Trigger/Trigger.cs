using Blackout;
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

        private void OnTriggerStay2D (Collider2D other)
        {
            var blackout = GameObject.FindGameObjectWithTag("Blackout").GetComponent<BlackoutMethod>();
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
                textScript.lines = new [] {
                    "Саша", "Мам, я пошёл.", 
                    "Мама", "Давай, сынок. Не замёрзнешь? Поздно холодно будет.", 
                    "Саша", "Мам, ну что ты опять, не на северный полюс же иду.", 
                    "Мама", "Хорошо хорошо, до поздна там не гуляй, волноваться я буду.", 
                    "", ". . .",
                    "Саша", "Па-а-ап.. Можно машину взять? Я аккуратно.", 
                    "Папа", "Жирно будет!",
                    "Саша", "Но я -", 
                    "Папа", "Нет!!",
                    "Саша", "Ну ладно...", 
                    "Мама", "Сашенька, будет тебе ещё, покатаешься еще на машине в своей жизни.", 
                    "Мама", "Будь аккуратнее, не задерживайся.", 
                    "Саша", "Да да да..."};
            }
            if(other.CompareTag("Player") && !textScript.isTalking)
            {
                blackout.Darken();
                if (blackout.doneDarken)
                {
                    movementScript.enabled = true;
                    textScript.isTalking = true;
                    dialogueWindow.enabled = false;
                    dialogueText.enabled = false;
                    dialogueNames.enabled = false;
                    textScript.enabled = false;
                    movementScript.Flip();
                    player.gameObject.transform.position = new Vector3(1.194f, -0.063f, 0f);
                    chase.leftLimit = 1.009f + 0.806f;
                    chase.rightLimit = 5.17f - 0.88f;
                    chase.upperLimit = 0.5f;
                    chase.bottomLimit = -0.028f;
                    blackout.Brighten();
                }
            }
        }  
    }
}
