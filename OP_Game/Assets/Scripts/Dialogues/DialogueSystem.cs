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
        [SerializeField] TextMeshProUGUI name;
        [SerializeField] public string[] lines;
        [SerializeField] float TextSpeed;
        public GameObject npc;

        private int index;
        private int indexName;
        public bool isTalking = true;
        
        public GameObject girl;
        public GameObject friendOne;
        public GameObject friendTwo;
        public GameObject player;
        public GameObject mother;
        public GameObject dad;
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
                    if (AnimationName(lines[indexName]) != null)
                        npc.GetComponent<Animator>().Play(AnimationName(lines[indexName]) + "Idle");
                    text.text = lines[index];
                }
            }
        }

        GameObject PersonToTalk(string name)
        {
            switch (name)
            {
                case "Мама":
                    return mother;
                case "Папа":
                    return dad;
                case "Саша":
                    return player;
                case "Лиза":
                    return girl;
                case "Серёга":
                    return friendOne;
                case "Кирюха":
                    return friendTwo;
            }

            return null;
        }

        string AnimationName(string name)
        {
            switch (name)
            {
                case "Мама":
                    return ("Mother");
                case "Папа":
                    return ("Dad");
                case "Саша":
                    return ("Player");
                case "Лиза":
                    return ("Girl");
                case "Серёга":
                    return ("Friend1");
                case "Кирюха":
                    return ("Friend2");
            }

            return null;
        }

        void StartDialog()
        {
            index = 1;
            indexName = 0;
            StartCoroutine(TypeLine());
        }

        IEnumerator TypeLine()
        {
            npc = PersonToTalk(lines[indexName]);
            if (AnimationName(lines[indexName]) != null)
                npc.GetComponent<Animator>().Play(AnimationName(lines[indexName]) + "Talk");
            Debug.Log(AnimationName(lines[indexName]));
            name.text = lines[indexName];
            foreach(char c in lines[index])
            {
                text.text += c;
                yield return new WaitForSeconds(TextSpeed);
            }
            if (AnimationName(lines[indexName]) != null)
                npc.GetComponent<Animator>().Play(AnimationName(lines[indexName]) + "Idle");
        }


        void IsNextLine()
        {
            if(index < lines.Length - 1 && index < lines.Length - 2)
            {
                index += 2;
                indexName += 2;
                name.text = string.Empty;
                text.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                isTalking = false;
            }
        }
    }
}