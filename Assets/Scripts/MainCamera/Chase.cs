using System;
using UnityEngine;

namespace MainCamera
{
    public class Chase : MonoBehaviour
    {
        public float dumping = 0.1f;
        
        private Vector2 _offset = new Vector2(0.1f, 0.1f);
        public bool isLeft;
        private Transform _player;
        private int _lastX;

        [SerializeField]
        public float leftLimit;
        [SerializeField]
        public float rightLimit;
        [SerializeField]
        public float upperLimit;
        [SerializeField]
        public float bottomLimit;

        private void Start()
        {
            _offset = new Vector2(Mathf.Abs(_offset.x), _offset.y);
            FindPlayer(isLeft);
        }

        private void FindPlayer(bool playerIsLeft)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _lastX = Mathf.RoundToInt(_player.position.x);
            if (playerIsLeft)
            {
                var position = _player.position;
                transform.position = new Vector3(position.x - _offset.x, position.y - _offset.y, -1);
            }
            else
            {
                var position = _player.position;
                transform.position = new Vector3(position.x + _offset.x, position.y + _offset.y, -1);
            }
        }

        private void Update() 
        {
            if (_player)
            {
                var currentX = Mathf.RoundToInt(_player.position.x);
                if (currentX > _lastX)
                {
                    isLeft = false;
                }
                else if (currentX < _lastX)
                {
                    isLeft = true;
                }
                _lastX = Mathf.RoundToInt(_player.position.x);

                Vector3 target;
                if (isLeft)
                {
                    var position = _player.position;
                    target = new Vector3(position.x - _offset.x, position.y - _offset.y, -1);
                }
                else
                {
                    var position = _player.position;
                    target = new Vector3(position.x + _offset.x, position.y + _offset.y, -1);
                }
                
                Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
                transform.position = currentPosition;
            }

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
                Mathf.Clamp(transform.position.y, bottomLimit, upperLimit),
                transform.position.z);

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector2(leftLimit, upperLimit), new Vector2(rightLimit, upperLimit));
            Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(rightLimit, bottomLimit));
            Gizmos.DrawLine(new Vector2(leftLimit, upperLimit), new Vector2(leftLimit, bottomLimit));
            Gizmos.DrawLine(new Vector2(rightLimit, upperLimit), new Vector2(rightLimit, bottomLimit));

        }
    }
}
