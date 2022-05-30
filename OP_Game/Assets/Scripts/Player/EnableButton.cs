using UnityEngine;

namespace Player
{
    public class EnableButton : MonoBehaviour
    {
        public GameObject _button;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag.Contains("Trigger"))
            {
                _button.SetActive(true);
            }
        }
    }
}
