using Car;
using Dialogues;
using MainCamera;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Trigger
{
    public class BeforeRideDialogueTrigger : MonoBehaviour
    {
        private Camera _mainCamera;
        private Camera _carCamera;

        private void Start()
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            _carCamera = GameObject.FindGameObjectWithTag("Car Camera").GetComponent<Camera>();
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            var carMovement = GameObject.FindGameObjectWithTag("Car").GetComponent<CarMovement>();
            var friendOne = GameObject.FindGameObjectWithTag("Friend1");
            var friendTwo = GameObject.FindGameObjectWithTag("Friend2");
            var player = GameObject.FindGameObjectWithTag("Player");
            var dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<Image>();
            var dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<TextMeshProUGUI>();
            var dialogueNames = GameObject.FindGameObjectWithTag("DialogueNames").GetComponent<TextMeshProUGUI>();
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
                dialogueNames.enabled = true;
                textScript.enabled = true;
                textScript.Invoke("Start", 0);
                textScript.lines = new[]
                {
                    "Саша",
                    "Ключи у меня.",
                };
            }
            if (other.CompareTag("Player") && !textScript.isTalking)
            {
                _mainCamera.enabled = false;
                _carCamera.enabled = true;
                var car = GameObject.FindGameObjectWithTag("Car");
                car.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                car.GetComponent<Mover>().enabled = true;
                carMovement.enabled = true;
                textScript.isTalking = true;
                dialogueWindow.enabled = false;
                dialogueText.enabled = false;
                dialogueNames.enabled = false;
                textScript.enabled = false;
            }
        }
    }
}
