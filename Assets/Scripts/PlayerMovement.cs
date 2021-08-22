using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerAnimator _playerAnimator = default;
    [SerializeField] private PlayerStatsSO _playerStatsSO = default;
    private Rigidbody2D _rigidbody;
    private Audio _audio;
    private bool _isCrouching;


    public Vector2 MovementInput { private get; set; }
    public bool IsGrounded { get; private set; } = true;


	void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _audio = GetComponent<Audio>();
    }

	void Update()
	{
        CheckGrounded();
	}

	void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (!_isCrouching)
        {
            _rigidbody.velocity = new Vector2(MovementInput.x * _playerStatsSO.walkSpeed, _rigidbody.velocity.y);
            if (_rigidbody.velocity.x != 0.0f)
            {
                _playerAnimator.SetMove(true);
            }
            else
            {
                _playerAnimator.SetMove(false);
            }
        }
    }

    public void CrouchAction()
    {
        _rigidbody.velocity = Vector2.zero;
        _isCrouching = true;
        _playerAnimator.IsCrouching(true);
    }

    public void StandUpAction()
    {
        _isCrouching = false;
        _playerAnimator.IsCrouching(false);
    }

    public void JumpAction()
	{
        if (IsGrounded)
        {
            _audio.Sound("Jump").Play();
            IsGrounded = false;
            _playerAnimator.IsJumping(true);
            _rigidbody.AddForce(new Vector2(0.0f, _playerStatsSO.jumpForce), ForceMode2D.Impulse);
        }
    }

    private void CheckGrounded()
    {
        if (_rigidbody.velocity.y < 0.0f)
        {
            RaycastHit2D raycastHit2D = Raycast.Cast(transform.position, Vector2.down, 0.1f, LayerMaskEnum.Ground, Color.red);
            if (raycastHit2D.collider != null)
            {
                _audio.Sound("Landed").Play();
                _playerAnimator.IsJumping(false);
                IsGrounded = true;
            }
        }
    }

    public void SetLockMovement(bool state)
    {
        _rigidbody.velocity = Vector2.zero;
        if (state)
        {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            _rigidbody.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
