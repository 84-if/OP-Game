using System;
using UnityEngine;

namespace Characters
{
    public class LayersScript : MonoBehaviour
    {
        void Update()
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)(transform.position.y*(-100));
        }
    }
}
