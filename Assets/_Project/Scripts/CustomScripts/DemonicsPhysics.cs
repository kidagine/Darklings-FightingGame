using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonicsPhysics : MonoBehaviour
{
    [SerializeField] private bool _ignoreWalls = default;
    public DemonicsVector2 Velocity { get; set; }
    public DemonicsVector2 Position { get; set; }
    public bool OnGround { get { return Position.y <= GROUND_POINT ? true : false; } private set { } }
    public bool OnWall { get { return Position.x >= WALL_RIGHT_POINT || Position.x <= WALL_LEFT_POINT ? true : false; } private set { } }
    private DemonicsVector2 _freezePosition;
    private DemonicsFloat _gravity;
    private Camera _camera;
    private bool _freeze;
    public static DemonicsFloat GROUND_POINT = (DemonicsFloat)(-4.485);
    public static DemonicsFloat CELLING_POINT = (DemonicsFloat)(7);
    public static DemonicsFloat WALL_RIGHT_POINT;
    public static DemonicsFloat WALL_LEFT_POINT;
    private int _skipWallFrame = 1;
    private readonly DemonicsFloat _wallPointOffset = (DemonicsFloat)0.6;
    public DemonicsPhysics OtherPhysics { get; set; }
    public bool IgnoreWalls { get { return _ignoreWalls; } set { _ignoreWalls = value; } }


    void Awake()
    {
        _camera = Camera.main;
    }

    public void OnCollision(DemonicsPhysics otherPhysics)
    {
        this.OtherPhysics = otherPhysics;
    }

    public void SetFreeze(bool state)
    {
        _freeze = state;
        if (_freeze)
        {
            _freezePosition = Position;
        }
    }

    public void ResetSkipWall()
    {
        _skipWallFrame = 2;
    }

    void FixedUpdate()
    {
        // Stop physics if frozen
        if (_freeze)
        {
            Position = _freezePosition;
            return;
        }
        // Set horizontal wall points
        CameraHorizontalBounds();
        // Sets physics
        Velocity = new DemonicsVector2(Velocity.x, Velocity.y - _gravity);
        // Check collision


    }

    private void CameraHorizontalBounds()
    {
        if (_skipWallFrame > 0)
        {
            _skipWallFrame--;
            WALL_LEFT_POINT = (DemonicsFloat)(-1000);
            WALL_RIGHT_POINT = (DemonicsFloat)(1000);
        }
        else
        {
            WALL_LEFT_POINT = (DemonicsFloat)_camera.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).x + _wallPointOffset;
            WALL_RIGHT_POINT = (DemonicsFloat)_camera.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, Camera.main.nearClipPlane)).x - _wallPointOffset;
        }
    }

    public void SetPositionWithRender(DemonicsVector2 position)
    {
        Position = position;
        //Bounds();
        transform.position = new Vector2((float)Position.x, (float)Position.y);
    }

    public void EnableGravity(bool state)
    {
        if (state)
        {
            _gravity = (DemonicsFloat)0.018;
        }
        else
        {
            _gravity = (DemonicsFloat)0;
        }
    }

    public void SetJuggleGravity(bool state)
    {
        if (state)
        {
            _gravity = (DemonicsFloat)0.013;
        }
        else
        {
            _gravity = (DemonicsFloat)0.018;
        }
    }
}
