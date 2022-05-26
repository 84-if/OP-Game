using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Dialogues
{
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] public string[] lines;
        [SerializeField] float TextSpeed;

        private int index;
        public bool isTalking = true;
        void Start()
        {
            text.text = string.Empty;
            StartDialog();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (text.text == lines[index])
                {
                    IsNextLine();
                }
                else
                {
                    StopAllCoroutines();
                    text.text = lines[index];
                }
            }
        }

        void StartDialog()
        {
            index = 0;
            StartCoroutine(TypeLine());
        }

        IEnumerator TypeLine()
        {
            foreach(char c in lines[index])
            {
                text.text += c;
                yield return new WaitForSeconds(TextSpeed);
            }
        }


        void IsNextLine()
        {
            if(index < lines.Length -1)
            {
                index++;
                text.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                isTalking = false;
                // gameObject.SetActive(false);
            }
        }
    }
}