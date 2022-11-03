using System.Collections;
using System.Collections.Generic;
using FixMath.NET;
using UnityEngine;

public class DemonicsCollider : MonoBehaviour
{
    [SerializeField] private bool _showGizmo = default;
    [SerializeField] private bool _ignoreCollision = default;
    [SerializeField] private Vector2 _offset = default;
    [SerializeField] private Vector2 _size = default;
    private List<DemonicsCollider> _demonicsColliders = new List<DemonicsCollider>();
    private DemonicsPhysics _demonicsPhysics;


    public Color GizmoColor { get; set; } = Color.green;
    public bool IgnoreCollision  { get {return _ignoreCollision;} set {_ignoreCollision = value;} }
    public Fix64 SizeX { get {return (Fix64)_size.x;} set {_size.x = (float)value;} }
    public Fix64 SizeY { get {return (Fix64)_size.y;} set {_size.y = (float)value;} }
    public Fix64 OffsetX { get {return (Fix64)_offset.x;} set {_offset.x = (float)value;} }
    public Fix64 OffsetY { get {return (Fix64)_offset.y;} set {_offset.y = (float)value;} }
    public Fix64 PositionX { get {
        if (_demonicsPhysics != null)
        {
            return (Fix64)_demonicsPhysics.PositionX + OffsetX;
        }
        else
        {
            return OffsetX;
        }
    } private set {} }
    public Fix64 PositionY { get {
        if (_demonicsPhysics != null)
        {
            return (Fix64)_demonicsPhysics.PositionY + OffsetY;
        }
        else
        {
            return OffsetY;
        }
    } private set {} }

    bool valueInRange(Fix64 value, Fix64 min, Fix64 max)
    { return (value >= min) && (value <= max); }

    public bool Colliding(DemonicsCollider a, DemonicsCollider b)
    {
        bool xOverlap = valueInRange(a.PositionX, b.PositionX, b.PositionX + b.SizeX) ||
                    valueInRange(b.PositionX, a.PositionX, a.PositionX + a.SizeX);

        bool yOverlap = valueInRange(a.PositionY, b.PositionY, b.PositionY + b.SizeY) ||
                    valueInRange(b.PositionY, a.PositionY, a.PositionY + a.SizeY);
        return xOverlap && yOverlap;
    }

    void Start()
    {
        _demonicsPhysics = transform.root.GetComponent<DemonicsPhysics>();
        DemonicsCollider[] demonicsCollidersArray =  FindObjectsOfType<DemonicsCollider>();
        for (int i = 0; i <  demonicsCollidersArray.Length; i++)
        {
            _demonicsColliders.Add(demonicsCollidersArray[i]);
        }
        _demonicsColliders.Remove(this);
    }

    void FixedUpdate()
    {
        for (int i = 0; i < _demonicsColliders.Count; i++)
        {
            if (Colliding(this, _demonicsColliders[i]))
            {           
                if (_demonicsColliders[i].transform.root.TryGetComponent(out DemonicsPhysics demonicsPhysics))
                {
                    _demonicsPhysics.OnCollision(demonicsPhysics);
                }
            }
        }
    }

    protected virtual void OnDrawGizmos()
	{
        if (_showGizmo)
        {
            Color color = GizmoColor;
            color.a = 0.6f;
            Gizmos.color = color;
            Vector2 pushboxPosition = new Vector2((float)PositionX, (float)PositionY);
            Gizmos.matrix = Matrix4x4.TRS(pushboxPosition, transform.rotation, transform.localScale);

            Vector2 gizmoPosition = new Vector2((float)SizeX, (float)SizeY);
            Gizmos.DrawWireCube(Vector3.zero, gizmoPosition);
            Gizmos.DrawWireCube(Vector3.zero, gizmoPosition);
        }
	}
}
