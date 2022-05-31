using System.Collections.Generic;
using Dialogues;
using Player;
using TMPro;
using Trigger;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Trigger
{
    public class JopaTrigger : MonoBehaviour
    {
        public void OnTriggerStay2D(Collider2D other)
        {
            var girl = GameObject.FindGameObjectWithTag("Girl");
            var player = GameObject.FindGameObjectWithTag("Player");
            var dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<Image>();
            var dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<TextMeshProUGUI>();
            var textScript = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<DialogueSystem>();
            var dialogueNames = GameObject.FindGameObjectWithTag("DialogueNames").GetComponent<TextMeshProUGUI>();
            if (other.CompareTag("Player") && !textScript.isTalking)
            {
                var movementScript = player.GetComponent<Movement>();
                movementScript.enabled = true;
                textScript.isTalking = true;
                dialogueWindow.enabled = false;
                dialogueText.enabled = false;
                dialogueNames.enabled = false;
                textScript.enabled = false;
                var girlWalkScript = GameObject.FindGameObjectWithTag("Girl").GetComponent<Mover>();
                girlWalkScript.enabled = true;
                girl.GetComponent<SpriteRenderer>().flipX = false;
                GameObject.FindGameObjectWithTag("JopaTrigger").SetActive(false);
            }
        }
    }
}
