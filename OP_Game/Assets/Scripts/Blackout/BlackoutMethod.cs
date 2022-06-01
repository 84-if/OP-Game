using System;
using System.Collections;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

namespace Blackout
{
    public class BlackoutMethod : MonoBehaviour
    {
        private Image _bg;
        public bool doneDarken;
        public bool doneBrighten;

        private void Start()
        {
            _bg = GameObject.FindGameObjectWithTag("Blackout").GetComponent<Image>();
        }

        private void Update()
        {
            
        }

        public void Darken()
        {
            StartCoroutine(c_Alpha(1.0f, 2.0f,"darken"));
        }

        public void Brighten()
        {
            StartCoroutine(c_Alpha(0.0f, 3.0f, "brighten"));
        }
 
        IEnumerator c_Alpha(float value, float time, string type)
        {
            float k = 0.0f;
            Color c0 = _bg.color;
            Color c1 = c0;
            c1.a = value;
 
            while ((k += Time.deltaTime) <= time)
            {
                _bg.color = Color.Lerp(c0, c1, k / time);
 
                yield return null;
            }
            if (type == "darken")
                doneDarken = true;
            if (type == "brighten")
                doneBrighten = true;
 
            _bg.color = c1;
        }
    }
}