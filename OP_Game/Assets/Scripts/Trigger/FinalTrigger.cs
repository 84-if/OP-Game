using Blackout;
using Car;
using Dialogues;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Trigger
{
    public class FinalTrigger : MonoBehaviour
    {
        public void OnTriggerStay2D(Collider2D other)
        {
            var dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<Image>();
            var dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<TextMeshProUGUI>();
            var textScript = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<DialogueSystem>();
            var dialogueNames = GameObject.FindGameObjectWithTag("DialogueNames").GetComponent<TextMeshProUGUI>();
            var theEndBlackout = GameObject.FindGameObjectWithTag("TheEndImage").GetComponent<BlackoutMethod>();
            var car = GameObject.FindGameObjectWithTag("Car");
            var carMover = car.GetComponent<Mover>();
            var police = GameObject.FindGameObjectWithTag("Police");
            var policeMover = police.GetComponent<Mover>();
            carMover.startPosition.x = car.transform.position.x;
            carMover.startPosition.y = car.transform.position.y;
            carMover.endPosition.x = 14.91f;
            carMover.endPosition.y = car.transform.position.y;
            carMover.step = 0.005f;
            if (other.CompareTag("Car") && !textScript.isTalking)
            {
                theEndBlackout.Darken();
                policeMover.enabled = true;
                carMover.enabled = true;
                carMover.Invoke("Start", 0);
                textScript.isTalking = true;
                dialogueWindow.enabled = false;
                dialogueText.enabled = false;
                dialogueNames.enabled = false;
                textScript.enabled = false;
            }
        }
    }
}
