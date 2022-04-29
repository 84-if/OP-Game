using UnityEngine;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2d;
        private Vector2 _movement;
        public float moveSpeed;

        private Animator _animator;
        public bool _looksRight = true;

        private void Start()
        {
            _rigidbody2d = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");
        }

        private void FixedUpdate()
        {
            _rigidbody2d.MovePosition(_rigidbody2d.position + _movement * moveSpeed * Time.fixedDeltaTime);

            if (_movement.x > 0 && !_looksRight)
                Flip();
            else if (_movement.x < 0 && _looksRight)
                Flip();
        
            if (_movement.x != 0 || _movement.y != 0)
                _animator.Play("PlayerRun");
            else
                _animator.Play("PlayerIdle");
        }

        public void Flip()
        {
            _looksRight = !_looksRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
