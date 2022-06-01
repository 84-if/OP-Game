using UnityEngine;

namespace Player
{
    public class EnableButton : MonoBehaviour
    {
        public SpriteRenderer _button;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag.Contains("Trigger") && !other.CompareTag("JopaTrigger"))
            {
                _button.enabled = true;
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag.Contains("Trigger") && !other.CompareTag("JopaTrigger"))
            {
                _button.enabled = false;
            }
        }
    }
}
