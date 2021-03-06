using Dialogues;
using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    public class EnableEnterButton : MonoBehaviour
    {
        public Image dialogueWindow;
        public SpriteRenderer enterButton;
        public SpriteRenderer carEnterButton;
        
        void Start()
        {
            dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<Image>();
            enterButton = GameObject.FindGameObjectWithTag("Enter Button").GetComponent<SpriteRenderer>();
            carEnterButton = GameObject.FindGameObjectWithTag("Car Enter Button").GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (dialogueWindow.enabled)
            {
                enterButton.enabled = true;
                carEnterButton.enabled = true;
            }
            else
            {
                enterButton.enabled = false;
                carEnterButton.enabled = false;
            }
                

        }
    }
}
