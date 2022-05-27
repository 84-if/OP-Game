using UnityEngine;

namespace Player
{
    public class WakeUpRigidBody : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2d;

        private void Start()
        {
            _rigidbody2d = GetComponent<Rigidbody2D>();
        
        }
        private void Update()
        {
            _rigidbody2d.WakeUp();
        }
    }
}
