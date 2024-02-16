using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player _player;
    private Audio _audio;
    private DemonVector2 _velocity;
    private int _knockbackFrame;

    public DemonicsPhysics Physics { get; private set; }
    public bool HasJumped { get; set; }
    public bool HasDoubleJumped { get; set; }
    public bool HasAirDashed { get; set; }
    public DemonFloat MovementSpeed { get; set; }
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
    }

    public void SetPosition(DemonVector2 position)
    {
        Vector2Int fixedPosition = new Vector2Int((int)(position.x * 1) / 1, (int)(position.y * 1) / 1);
        Physics.SetPositionWithRender(new DemonVector2((DemonFloat)position.x, (DemonFloat)position.y));
    }

    public void TravelDistance(DemonVector2 travelDistance)
    {
        Physics.Velocity = new DemonVector2((DemonFloat)travelDistance.x, (DemonFloat)travelDistance.y);
    }

    public void Knockback(Vector2 knockbackForce, int knockbackDuration, int direction, int arc = 0, bool instant = false, bool ground = false, bool ignoreX = false)
    {
        if (knockbackDuration > 0)
        {
            if (_knockbackDuration > 0)
            {
                StopKnockback();
                Physics.SetFreeze(true);
            }
            DemonFloat endPositionY = (DemonFloat)0;
            if (arc > 0)
            {
                endPositionY = DemonicsPhysics.GROUND_POINT;
            }
            if (instant)
            {
                _startPosition = Physics.Position;
                _endPosition = new DemonVector2(Physics.Position.x + ((DemonFloat)knockbackForce.x * direction), Physics.Position.y + endPositionY);
                _knockbackDuration = knockbackDuration;
                _arc = (DemonFloat)arc;
            }
            else
            {
                _player.hitstopEvent.AddListener(() =>
                {
                    _ignoreX = ignoreX;
                    _startPosition = Physics.Position;
                    if (!ground)
                    {
                        _endPosition = new DemonVector2(Physics.Position.x + ((DemonFloat)knockbackForce.x * direction), Physics.Position.y + endPositionY);
                    }
                    else
                    {
                        _endPosition = new DemonVector2(Physics.Position.x + ((DemonFloat)knockbackForce.x * direction), DemonicsPhysics.GROUND_POINT - 0.5);
                    }
                    _knockbackDuration = knockbackDuration;
                    _arc = (DemonFloat)arc;
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
    private DemonFloat _arc;
    private int _knockbackDuration;
    DemonVector2 _startPosition;
    DemonVector2 _endPosition;
    private void CheckKnockback()
    {
        if (_knockbackDuration > 0 && !IsInHitstop)
        {
            DemonFloat ratio = (DemonFloat)_knockbackFrame / (DemonFloat)_knockbackDuration;
            DemonFloat distance = _endPosition.x - _startPosition.x;
            DemonFloat nextX = DemonFloat.Lerp(_startPosition.x, _endPosition.x, ratio);
            DemonFloat baseY = DemonFloat.Lerp(_startPosition.y, _endPosition.y, (nextX - _startPosition.x) / distance);
            DemonFloat arc = _arc * (nextX - _startPosition.x) * (nextX - _endPosition.x) / ((DemonFloat)(-0.25) * distance * distance);
            DemonVector2 nextPosition;
            if (arc == (DemonFloat)0)
            {
                nextPosition = new DemonVector2(nextX, Physics.Position.y);
            }
            else
            {
                nextPosition = new DemonVector2(nextX, baseY + arc);
            }
            if (_ignoreX)
            {
                nextPosition = new DemonVector2(Physics.Position.x, nextPosition.y);
            }
            Physics.SetPositionWithRender(nextPosition);
            _knockbackFrame++;
            if (_knockbackFrame == _knockbackDuration)
            {
                Physics.Velocity = DemonVector2.Zero;
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
        }
    }

    public void ExitHitstop()
    {
        if (IsInHitstop)
        {
            IsInHitstop = false;
            Physics.SetFreeze(false);
            Physics.Velocity = new DemonVector2(_velocity.x, Physics.Velocity.y);
        }
    }
}
