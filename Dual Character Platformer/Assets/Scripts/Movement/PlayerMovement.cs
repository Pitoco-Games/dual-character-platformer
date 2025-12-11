using System.Collections;
using UnityEngine;

namespace Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpSpeed;
        [SerializeField] private float _jumpMaxHeight;
        [SerializeField] private float _fallingSpeed;
        [SerializeField] private float _counterJumpGravity;
        [SerializeField] private float _fallingGravity;
        
        [SerializeField] private CircleCollider2D _circleCollider;

        private float _moveX;
        private float _moveY;
           
        private float MoveAmount => _speed *  Time.deltaTime;
        private Coroutine _jumpCoroutine;
        private Coroutine _fallingCoroutine;
        private bool _isGrounded = true;
        
        //Jump
        private bool _isJumping;
        private float _jumpStartHeight;
        private float _jumpRelativeMaxHeight;
        private float JumpSpeedCorrected => _jumpSpeed * 100 * Time.deltaTime;
        private float FallingSpeedCorrected => _fallingSpeed * 100 * Time.deltaTime;
        
        //Grounded
        private float GroundedCheckDistance => _circleCollider.radius + 0.05f;

        public void Move(float direction)
        {
            _moveX = direction * MoveAmount;
        }
        
        public void StartJump()
        {
            if (!_isGrounded || _isJumping)
            {
                return;
            }
            
            _isJumping = true;
            
            if (_jumpCoroutine != null)
            {
                StopCoroutine(_jumpCoroutine);
            }
            
            _jumpCoroutine = StartCoroutine(JumpCoroutine());
        }

        public void StopJump()
        {
            if (_jumpCoroutine != null)
            {
                StopCoroutine(_jumpCoroutine);
            }
            _fallingCoroutine = StartCoroutine(FallingCoroutine());
            
            _isJumping = false;
            _moveY = 0;
        }

        private IEnumerator JumpCoroutine()
        {
            float startingHeight = transform.position.y;
            float relativeMaxHeight = startingHeight + _jumpMaxHeight;

            while (transform.position.y < relativeMaxHeight)
            {
                _moveY = JumpSpeedCorrected;
                yield return null;
            }

            float upwardsVelocity = JumpSpeedCorrected;
            while (upwardsVelocity > 0)
            {
                upwardsVelocity -= _counterJumpGravity * Time.deltaTime;
                _moveY = upwardsVelocity;
                yield return null;
            }
            
            StopJump();
        }

        private IEnumerator FallingCoroutine()
        {
            while (!_isGrounded)
            {
                _moveY = -FallingSpeedCorrected;
                
                yield return null;
            }
        }
        
        private void FixedUpdate()
        {
            UpdateIsGrounded();
            if (!_isGrounded && !_isJumping && _fallingCoroutine == null)
            {
                _fallingCoroutine = StartCoroutine(FallingCoroutine());
            }
            
            _rigidbody.linearVelocity = new Vector2(_moveX, _moveY);
            
            _moveX = 0;
            _moveY = 0;
        }

        private void UpdateIsGrounded()
        {
            const string groundLayerName = "Ground";
            var raycast = Physics2D.Raycast(transform.position, Vector2.down, GroundedCheckDistance, LayerMask.GetMask(groundLayerName));
            _isGrounded = raycast.collider != null;
            
            if (_isGrounded && _fallingCoroutine != null)
            {
                StopCoroutine(_fallingCoroutine);
                _fallingCoroutine = null;
            }
        }
    }
}
