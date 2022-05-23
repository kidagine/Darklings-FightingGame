using Demonics.Sounds;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour, IPushboxResponder
{
    [SerializeField] protected PlayerAnimator _playerAnimator = default;
    [SerializeField] private GameObject _dustUpPrefab = default;
    [SerializeField] private GameObject _dustDownPrefab = default;
    [SerializeField] private GameObject _dashPrefab = default;
    [SerializeField] private GameObject _playerGhostPrefab = default;
    protected Player _player;
    private BrainController _playerController;
    protected Rigidbody2D _rigidbody;
    private PlayerStats _playerStats;
    private InputBuffer _inputBuffer;
    private Coroutine _ghostsCoroutine;
    private Coroutine _knockbackCoroutine;
    private Audio _audio;
    protected bool _isMovementLocked;
    protected bool _onTopOfPlayer;
    private bool _hasDashedMiddair;
    public bool _isGrounded;
    private UnityAction _knockbackAction;
    public bool HasJumped { get; set; }
    public bool HasDoubleJumped { get; set; }
    public bool HasAirDashed { get; set; }

    public float MovementSpeed { get; set; }
    public bool FullyLockMovement { get; set; }
    public Vector2 MovementInput { get; set; }
    public bool IsGrounded { get; set; } = true;
    public bool IsCrouching { get; private set; }
    public bool IsMoving { get; protected set; }
    public bool IsDashing { get; private set; }
    public bool CanDoubleJump { get; set; } = true;
    public bool IsInCorner { get; set; }


    void Awake()
    {
        _player = GetComponent<Player>();
        _playerStats = GetComponent<PlayerStats>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputBuffer = GetComponent<InputBuffer>();
        _audio = GetComponent<Audio>();
    }

    public void SetController()
    {
        _playerController = GetComponent<BrainController>();
    }


    void Start()
    {
        MovementSpeed = _playerStats.PlayerStatsSO.walkSpeed;
    }

    void FixedUpdate()
    {
        JumpControl();
    }

    public void ResetPlayerMovement()
    {
        IsGrounded = true;
        SetLockMovement(false);
        CanDoubleJump = true;
        ResetGravity();
    }

    private void JumpControl()
    {
        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.velocity += (4 - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
        }
        else if (_rigidbody.velocity.y > 0)
        {
            _rigidbody.velocity += (3 - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
        }
    }

    public void TravelDistance(Vector2 travelDistance)
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(new Vector2(travelDistance.x * 3.0f, travelDistance.y * 3.0f), ForceMode2D.Impulse);
    }

    public bool CrouchAction()
    {
        if (!IsCrouching && !_player.IsBlocking && !IsDashing && !_player.IsKnockedDown)
        {
            ResetToWalkSpeed();
            if (IsGrounded)
            {
                _rigidbody.velocity = Vector2.zero;
            }
            IsCrouching = true;
            //_playerAnimator.IsCrouching(true);
            return true;
        }
        return false;
    }

    public bool StandUpAction()
    {
        if (IsCrouching)
        {
            IsCrouching = false;
            //_playerAnimator.IsCrouching(false);
            return true;
        }
        return false;
    }

    public void SetLockMovement(bool state)
    {
        MovementInput = Vector2.zero;
        _rigidbody.velocity = Vector2.zero;
        _isMovementLocked = state;
        //_playerAnimator.Walk(false);
    }

    public void GroundedPoint(Transform other, float point)
    {
        if (_rigidbody.velocity.y < 0.0f)
        {
            float pushForceX = 8.0f;
            float pushForceY = -4.0f;
            if (IsInCorner && transform.localScale.x > 1.0f)
            {
                _onTopOfPlayer = true;
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.AddForce(new Vector2(-pushForceX, pushForceY), ForceMode2D.Impulse);
            }
            else if (IsInCorner && transform.localScale.x < 1.0f)
            {
                _onTopOfPlayer = true;
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.AddForce(new Vector2(pushForceX, pushForceY), ForceMode2D.Impulse);
            }
        }
    }

    public void GroundedPointExit()
    {
        if (_onTopOfPlayer)
        {
            _onTopOfPlayer = false;
        }
    }

    public void OnGrounded()
    {
        _isGrounded = true;
    }

    public void OnAir()
    {
        _isGrounded = false;
    }


    public void ShadowbreakKnockback()
    {
        _playerController.DeactivateInput();
        _knockbackAction += _playerController.ActivateInput;
        Knockback(new Vector2(-transform.localScale.x, 0.3f), 4.5f, 0.3f);
    }

    public void Knockback(Vector2 knockbackDirection, float knockbackForce, float knockbackDuration)
    {
        _rigidbody.MovePosition(new Vector2(transform.position.x + knockbackForce, transform.position.y));
        _knockbackCoroutine = StartCoroutine(KnockbackCoroutine(knockbackForce * knockbackDirection, knockbackDuration));
    }

    IEnumerator KnockbackCoroutine(Vector2 knockback, float knockbackDuration)
    {
        Vector2 startingPosition = transform.position;
        Vector2 finalPosition = new(transform.position.x + knockback.x, transform.position.y + knockback.y);
        float elapsedTime = 0;
        while (elapsedTime < knockbackDuration)
        {
            _rigidbody.MovePosition(Vector3.Lerp(startingPosition, finalPosition, elapsedTime / knockbackDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _rigidbody.MovePosition(finalPosition);
        _knockbackAction?.Invoke();
        _knockbackAction = null;
    }

    public void ResetGravity()
    {
        _rigidbody.gravityScale = 2.0f;
    }

    public void StopKnockback()
    {
        if (_knockbackCoroutine != null)
        {
            StopCoroutine(_knockbackCoroutine);
        }
    }

    public void SetRigidbodyToKinematic(bool state)
    {
        if (state)
        {
            StopKnockback();
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            _rigidbody.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }
        FullyLockMovement = state;
        _rigidbody.isKinematic = state;
        _player.SetGroundPushBox(!state);
    }

    public void ZeroGravity()
    {
        _rigidbody.gravityScale = 0.0f;
    }

    public void ResetToWalkSpeed()
    {
        if (MovementSpeed == _playerStats.PlayerStatsSO.runSpeed)
        {
            _audio.Sound("Run").Stop();
            MovementSpeed = _playerStats.PlayerStatsSO.walkSpeed;
        }
    }
}
