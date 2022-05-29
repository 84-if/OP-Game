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
        public Collider2D _player;

        public void OnTriggerStay2D(Collider2D other)
        {
            var girl = GameObject.FindGameObjectWithTag("Girl");
            var player = GameObject.FindGameObjectWithTag("Player");
            var dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<Image>();
            var dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<TextMeshProUGUI>();
            var textScript = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<DialogueSystem>();
            Debug.Log("Стоит в триггере jopa");
            if (other.CompareTag("Player") && !textScript.isTalking)
            {
                Debug.Log("О4КО");
                var movementScript = player.GetComponent<Movement>();
                movementScript.enabled = true;
                textScript.isTalking = true;
                dialogueWindow.enabled = false;
                dialogueText.enabled = false;
                textScript.enabled = false;
                var girlWalkScript = GameObject.FindGameObjectWithTag("Girl").GetComponent<Mover>();
                girlWalkScript.enabled = true;
                girl.GetComponent<SpriteRenderer>().flipX = false;
                girl.GetComponent<Animator>().Play("GirlWalk");
            }
        }
    }
}
