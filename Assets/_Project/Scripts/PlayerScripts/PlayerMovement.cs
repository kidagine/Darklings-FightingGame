using Demonics.Sounds;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _wallLayerMask = default;
    [SerializeField] private LayerMask _playerLayerMask = default;
    private Player _player;
    private Audio _audio;
    private DemonicsVector2 _velocity;
    private Coroutine _knockbackCoroutine;
    private int _knockbackFrame;
    private int _knockbackDuration;

    private readonly DemonicsFloat _cornerLimit = (DemonicsFloat)10.46;
    public DemonicsPhysics Physics { get; private set; }
    public bool HasJumped { get; set; }
    public bool HasDoubleJumped { get; set; }
    public bool HasAirDashed { get; set; }
    public DemonicsFloat MovementSpeed { get; set; }
    public Vector2 MovementInput { get; set; }
    public bool IsGrounded { get; set; } = true;
    public bool IsInCorner { get; private set; }
    public bool IsInHitstop { get; private set; }

    void Awake()
    {
        _player = GetComponent<Player>();
        Physics = GetComponent<DemonicsPhysics>();
        _audio = GetComponent<Audio>();
    }

    void Start()
    {
        MovementSpeed = _player.playerStats.SpeedWalk;
    }

    void FixedUpdate()
    {
        CheckKnockback();
        CheckCorner();
        CheckGrounded();
    }

    private void CheckGrounded()
    {
        if (Physics.Position.y == DemonicsPhysics.GROUND_POINT)
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }

    public void TravelDistance(Vector2 travelDistance)
    {
        Physics.Velocity = new DemonicsVector2((DemonicsFloat)travelDistance.x, (DemonicsFloat)travelDistance.y);
    }

    public void CheckForPlayer()
    {
        float space = 0.685f;
        for (int i = 0; i < 3; i++)
        {
            Debug.DrawRay(new Vector2(transform.position.x + space, transform.position.y), Vector2.down, Color.green);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + space, transform.position.y), Vector2.down, 0.6f, _playerLayerMask);
            if (hit.collider != null)
            {
                if (hit.collider.transform.root != transform.root)
                {
                    if (hit.collider.transform.root.TryGetComponent(out Player player))
                    {
                        float pushboxSizeX = hit.collider.GetComponent<BoxCollider2D>().size.x;
                    }
                }
            }
            space -= 0.685f;
        }
    }

    public void AddForce(float moveHorizontally)
    {
        int direction = 0;
        if (moveHorizontally == 1)
        {
            direction = (int)transform.localScale.x * -1;
        }
    }

    public void KnockbackNow(Vector2 knockbackDirection, Vector2 knockbackForce, int knockbackDuration)
    {
        _startPosition = Physics.Position;
        _endPosition = new DemonicsVector2(Physics.Position.x + (DemonicsFloat)knockbackDirection.x * (DemonicsFloat)knockbackForce.x, Physics.Position.y + (DemonicsFloat)knockbackDirection.y * (DemonicsFloat)knockbackForce.y); ;
        _knockbackDuration = knockbackDuration;
    }

    public void Knockback(Vector2 knockbackDirection, Vector2 knockbackForce, int knockbackDuration)
    {
        _player.hitstopEvent.AddListener(() =>
        {
            _startPosition = Physics.Position;
            _endPosition = new DemonicsVector2(Physics.Position.x + (DemonicsFloat)knockbackDirection.x * (DemonicsFloat)knockbackForce.x, Physics.Position.y + (DemonicsFloat)knockbackDirection.y * (DemonicsFloat)knockbackForce.y); ;
            _knockbackDuration = knockbackDuration;
        });
    }

    public void StopKnockback()
    {
        if (_knockbackCoroutine != null)
        {
            StopCoroutine(_knockbackCoroutine);
        }
    }
    DemonicsVector2 _startPosition;
    DemonicsVector2 _endPosition;
    private void CheckKnockback()
    {
        if (_knockbackDuration > 0)
        {
            _knockbackFrame++;
            DemonicsFloat ratio = (DemonicsFloat)_knockbackFrame / (DemonicsFloat)_knockbackDuration;
            Physics.SetPositionWithRender(new DemonicsVector2(_startPosition.x * ((DemonicsFloat)1 - ratio) + _endPosition.x * ratio, _startPosition.y * ((DemonicsFloat)1 - ratio) + _endPosition.y * ratio));
            if (_knockbackFrame == _knockbackDuration)
            {
                Physics.Velocity = DemonicsVector2.Zero;
                Physics.Position = _endPosition;
                _knockbackDuration = 0;
                _knockbackFrame = 0;
            }
        }
    }

    private void CheckCorner()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(-transform.localScale.x, 0.0f), 1.0f, _wallLayerMask);
        if (Physics.Position.x >= DemonicsFloat.Abs(_cornerLimit))
        {
            IsInCorner = true;
        }
        else
        {
            IsInCorner = false;
        }
    }

    public Vector2 OnWall()
    {
        if (!IsInHitstop)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(-transform.localScale.x, 0.0f), 2.0f, _wallLayerMask);
            if (hit.collider != null)
            {
                return new Vector2(hit.point.x - (0.2f * -transform.localScale.x), transform.localPosition.y);
            }
            else
            {
                return Vector2.zero;
            }
        }
        return Vector2.zero;
    }

    public void ResetGravity()
    {
    }

    public void ZeroGravity()
    {
    }

    public void ResetToWalkSpeed()
    {
        if (MovementSpeed == _player.playerStats.SpeedRun)
        {
            _audio.Sound("Run").Stop();
            MovementSpeed = _player.playerStats.SpeedWalk;
        }
    }

    public void EnterHitstop()
    {
        if (!IsInHitstop)
        {
            IsInHitstop = true;
            _velocity = Physics.Velocity;
            Physics.SetFreeze(true);
        }
    }

    public void ExitHitstop()
    {
        if (IsInHitstop)
        {
            IsInHitstop = false;
            Physics.SetFreeze(false);
            Physics.Velocity = _velocity;
        }
    }

    public void SetRigidbodyKinematic(bool state)
    {

    }
}
