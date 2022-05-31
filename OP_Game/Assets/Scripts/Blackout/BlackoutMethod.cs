using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Blackout
{
    public class BlackoutMethod : MonoBehaviour
    {
        private Image _bg;

        private void Start()
        {
            _bg = GameObject.FindGameObjectWithTag("Blackout").GetComponent<Image>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                Darken();
            if (Input.GetKeyDown(KeyCode.F))
                Brighten();
        }

        public void Darken()
        {
            StartCoroutine(c_Alpha(1.0f, 2.0f));
        }

        public void Brighten()
        {
            StartCoroutine(c_Alpha(0.0f, 3.0f));
        }
 
        IEnumerator c_Alpha(float value, float time)
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
 
            _bg.color = c1;
        }
    }
}
