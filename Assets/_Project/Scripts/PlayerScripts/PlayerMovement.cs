using Demonics.Sounds;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player _player;
    private Audio _audio;
    private DemonicsVector2 _velocity;
    private Coroutine _knockbackCoroutine;
    private int _knockbackFrame;
    private int _knockbackDuration;

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

    public void TravelDistance(DemonicsVector2 travelDistance)
    {
        Physics.Velocity = new DemonicsVector2((DemonicsFloat)travelDistance.x, (DemonicsFloat)travelDistance.y);
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

    public bool OnWall()
    {
        if (Physics.Position.x > (DemonicsFloat)0 && Physics.Velocity.x <= (DemonicsFloat)0)
        {
            return false;
        }
        else if (Physics.Position.x < (DemonicsFloat)0 && Physics.Velocity.x >= (DemonicsFloat)0)
        {
            return false;
        }
        return Physics.OnWall;
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
}
