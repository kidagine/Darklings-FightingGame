using Demonics.Sounds;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player _player;
    private Audio _audio;
    private DemonicsVector2 _velocity;
    private int _knockbackFrame;

    public DemonicsPhysics Physics { get; private set; }
    public bool HasJumped { get; set; }
    public bool HasDoubleJumped { get; set; }
    public bool HasAirDashed { get; set; }
    public DemonicsFloat MovementSpeed { get; set; }
    public Vector2 MovementInput { get; set; }
    public bool IsGrounded { get; set; } = true;
    public bool IsInHitstop { get; private set; }
    public bool IsInCorner => Physics.OnWall;

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

    public void Knockback(Vector2 knockbackForce, int knockbackDuration, int direction, int arc = 0, bool instant = false)
    {
        Physics.EnableGravity(false);
        if (_knockbackDuration > 0)
        {
            StopKnockback();
            Physics.SetFreeze(true);
        }
        if (instant)
        {
            _startPosition = Physics.Position;
            _endPosition = new DemonicsVector2(Physics.Position.x + ((DemonicsFloat)knockbackForce.x * direction), Physics.Position.y + (DemonicsFloat)knockbackForce.y);
            _knockbackDuration = knockbackDuration;
            _arc = (DemonicsFloat)arc;
        }
        else
        {
            _player.hitstopEvent.AddListener(() =>
            {
                _startPosition = Physics.Position;
                _endPosition = new DemonicsVector2(Physics.Position.x + ((DemonicsFloat)knockbackForce.x * direction), Physics.Position.y + DemonicsPhysics.GROUND_POINT);
                _knockbackDuration = knockbackDuration;
                _arc = (DemonicsFloat)arc;
            });
        }
    }

    public void StopKnockback()
    {
        _knockbackDuration = 0;
        _knockbackFrame = 0;
    }

    private DemonicsFloat _arc;
    private int _knockbackDuration;
    DemonicsVector2 _startPosition;
    DemonicsVector2 _endPosition;
    private void CheckKnockback()
    {
        if (_knockbackDuration > 0)
        {
            _knockbackFrame++;
            DemonicsFloat ratio = (DemonicsFloat)_knockbackFrame / (DemonicsFloat)_knockbackDuration;
            DemonicsFloat distance = _endPosition.x - _startPosition.x;
            DemonicsFloat nextX = DemonicsFloat.Lerp(_startPosition.x, _endPosition.x, ratio);
            DemonicsFloat baseY = DemonicsFloat.Lerp(_startPosition.y, _endPosition.y, (nextX - _startPosition.x) / distance);
            DemonicsFloat arc = _arc * (nextX - _startPosition.x) * (nextX - _endPosition.x) / ((DemonicsFloat)(-0.25) * distance * distance);
            DemonicsVector2 nextPosition = new DemonicsVector2(nextX, baseY + arc);
            Physics.SetPositionWithRender(nextPosition);
            if (_knockbackFrame == _knockbackDuration)
            {
                Physics.EnableGravity(true);
                StopKnockback();
            }
        }
    }

    public bool OnWall()
    {
        // if (Physics.Position.x > (DemonicsFloat)0 && Physics.Velocity.x <= (DemonicsFloat)0)
        // {
        //     return false;
        // }
        // else if (Physics.Position.x < (DemonicsFloat)0 && Physics.Velocity.x >= (DemonicsFloat)0)
        // {
        //     return false;
        // }
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
            Physics.Velocity = new DemonicsVector2(_velocity.x, Physics.Velocity.y);
        }
    }
}
