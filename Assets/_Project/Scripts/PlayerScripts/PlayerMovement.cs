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
    public bool IsGrounded { get; set; } = true;
    public bool IsInHitstop { get; private set; }
    public bool IsInCorner => Physics.OnWall;
    private InputBuffer inputBuffer;
    void Awake()
    {
        _player = GetComponent<Player>();
        Physics = GetComponent<DemonicsPhysics>();
        _audio = GetComponent<Audio>();
        inputBuffer = GetComponent<InputBuffer>();
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
            _player.HasJuggleForce = true;
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }

    public void SetPosition(DemonicsVector2 position)
    {
        Vector2Int fixedPosition = new Vector2Int((int)(position.x * 1) / 1, (int)(position.y * 1) / 1);
        Physics.SetPositionWithRender(new DemonicsVector2((DemonicsFloat)fixedPosition.x, (DemonicsFloat)fixedPosition.y));
    }

    public void TravelDistance(DemonicsVector2 travelDistance)
    {
        Physics.Velocity = new DemonicsVector2((DemonicsFloat)travelDistance.x, (DemonicsFloat)travelDistance.y);
    }

    public void Knockback(Vector2 knockbackForce, int knockbackDuration, int direction, int arc = 0, bool instant = false, bool ground = false, bool ignoreX = false)
    {
        if (knockbackDuration > 0)
        {
            Physics.EnableGravity(false);
            if (_knockbackDuration > 0)
            {
                StopKnockback();
                Physics.SetFreeze(true);
            }
            DemonicsFloat endPositionY = (DemonicsFloat)0;
            if (arc > 0)
            {
                endPositionY = DemonicsPhysics.GROUND_POINT;
            }
            if (instant)
            {
                _startPosition = Physics.Position;
                _endPosition = new DemonicsVector2(Physics.Position.x + ((DemonicsFloat)knockbackForce.x * direction), Physics.Position.y + endPositionY);
                _knockbackDuration = knockbackDuration;
                _arc = (DemonicsFloat)arc;
            }
            else
            {
                _player.hitstopEvent.AddListener(() =>
                {
                    _ignoreX = ignoreX;
                    _startPosition = Physics.Position;
                    if (!ground)
                    {
                        _endPosition = new DemonicsVector2(Physics.Position.x + ((DemonicsFloat)knockbackForce.x * direction), Physics.Position.y + endPositionY);
                    }
                    else
                    {
                        _endPosition = new DemonicsVector2(Physics.Position.x + ((DemonicsFloat)knockbackForce.x * direction), DemonicsPhysics.GROUND_POINT - 0.5);
                    }
                    _knockbackDuration = knockbackDuration;
                    _arc = (DemonicsFloat)arc;
                });
            }
        }
    }

    public void StopKnockback()
    {
        _knockbackDuration = 0;
        _knockbackFrame = 0;
    }
    private bool _ignoreX;
    private DemonicsFloat _arc;
    private int _knockbackDuration;
    DemonicsVector2 _startPosition;
    DemonicsVector2 _endPosition;
    private void CheckKnockback()
    {
        if (_knockbackDuration > 0 && !IsInHitstop)
        {
            DemonicsFloat ratio = (DemonicsFloat)_knockbackFrame / (DemonicsFloat)_knockbackDuration;
            DemonicsFloat distance = _endPosition.x - _startPosition.x;
            DemonicsFloat nextX = DemonicsFloat.Lerp(_startPosition.x, _endPosition.x, ratio);
            DemonicsFloat baseY = DemonicsFloat.Lerp(_startPosition.y, _endPosition.y, (nextX - _startPosition.x) / distance);
            DemonicsFloat arc = _arc * (nextX - _startPosition.x) * (nextX - _endPosition.x) / ((DemonicsFloat)(-0.25) * distance * distance);
            DemonicsVector2 nextPosition;
            if (arc == (DemonicsFloat)0)
            {
                nextPosition = new DemonicsVector2(nextX, Physics.Position.y);
            }
            else
            {
                nextPosition = new DemonicsVector2(nextX, baseY + arc);
            }
            if (_ignoreX)
            {
                nextPosition = new DemonicsVector2(Physics.Position.x, nextPosition.y);
            }
            Physics.SetPositionWithRender(nextPosition);
            _knockbackFrame++;
            if (_knockbackFrame == _knockbackDuration)
            {
                Physics.EnableGravity(true);
                Physics.Velocity = DemonicsVector2.Zero;
                StopKnockback();
            }
        }
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
            _velocity = Physics.Velocity;
            IsInHitstop = true;
            Physics.SetFreeze(true);
            if (_player.CanSkipAttack)
            {
                _player.hitstopEvent.AddListener(inputBuffer.CheckInputBufferAttacks);
            }
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
