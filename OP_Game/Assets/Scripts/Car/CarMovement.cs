using UnityEngine;

namespace Car
{
    public class CarMovement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2d;
        private Vector2 _movement;
        public float moveSpeed;

        private void Start()
        {
            _rigidbody2d = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");
        }

        private void FixedUpdate()
        {
            _rigidbody2d.MovePosition(_rigidbody2d.position + _movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
