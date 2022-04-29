using MainCamera;
using UnityEngine;

namespace Trigger
{
    public class Trigger : MonoBehaviour
    {
        private Camera _ourCamera;
        public Chase chase;

        private void Start()
        {
            _ourCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            chase = _ourCamera.GetComponent<Chase>();
        }

        private void OnTriggerStay2D (Collider2D other)
        {
            if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
            {
                other.gameObject.transform.position = new Vector3(1.194f, -0.063f, 0f);
                chase.leftLimit = 1.009f + 0.9575f;
                chase.rightLimit = 5.17f - 0.958f;
                chase.upperLimit = 0.8f - 0.47f;
                chase.bottomLimit = -0.48f + 0.47f;
            }
        }  
    }
}
