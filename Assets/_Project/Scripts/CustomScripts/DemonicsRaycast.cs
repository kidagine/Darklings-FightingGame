using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonicsRaycast
{
    private static List<DemonicsCollider> _demonicsColliders = new List<DemonicsCollider>();


    private static bool LineIntersectsRect(Vector2 p1, Vector2 p2, DemonicsCollider r)
    {
        return LineIntersectsLine(p1, p2, new Vector2((float)r.Position.x, (float)r.Position.y), new Vector2((float)r.Position.x + (float)r.Size.x, (float)r.Position.y)) ||
               LineIntersectsLine(p1, p2, new Vector2((float)r.Position.x + (float)r.Size.x, (float)r.Position.y), new Vector2((float)r.Position.x + (float)r.Size.x, (float)r.Position.y + (float)r.Size.x)) ||
               LineIntersectsLine(p1, p2, new Vector2((float)r.Position.x + (float)r.Size.x, (float)r.Position.y + (float)r.Size.y), new Vector2((float)r.Position.x, (float)r.Position.y + (float)r.Size.y)) ||
               LineIntersectsLine(p1, p2, new Vector2((float)r.Position.x, (float)r.Position.y + (float)r.Size.y), new Vector2((float)r.Position.x, (float)r.Position.y));
    }
    private static bool LineIntersectsLine(Vector2 l1p1, Vector2 l1p2, Vector2 l2p1, Vector2 l2p2)
    {
        float q = (l1p1.y - l2p1.y) * (l2p2.x - l2p1.x) - (l1p1.x - l2p1.x) * (l2p2.y - l2p1.y);
        float d = (l1p2.x - l1p1.x) * (l2p2.y - l2p1.y) - (l1p2.y - l1p1.y) * (l2p2.x - l2p1.x);
        if (d == 0)
        {
            return false;
        }
        float r = q / d;
        q = (l1p1.y - l2p1.y) * (l1p2.x - l1p1.x) - (l1p1.x - l2p1.x) * (l1p2.y - l1p1.y);
        float s = q / d;
        if (r < 0 || r > 1 || s < 0 || s > 1)
        {
            return false;
        }
        return true;
    }

    public static void CollidersList(List<DemonicsCollider> demonicsColliders)
    {
        _demonicsColliders = demonicsColliders;
    }

    public static DemonicsRaycastHit Cast(Vector2 origin, Vector2 directon, float length)
    {
#if UNITY_EDITOR
        Debug.DrawRay(origin, directon * length, Color.magenta);
#endif
        DemonicsRaycastHit demonicsRaycastHit = new DemonicsRaycastHit();
        Vector2 directionLength = directon * length;
        for (int i = 0; i < _demonicsColliders.Count; i++)
        {
            if (LineIntersectsRect(origin, new Vector2(origin.x + directionLength.x, origin.y + directionLength.y), _demonicsColliders[i]))
            {
                demonicsRaycastHit.collider = _demonicsColliders[i].gameObject;
            }
        }
        return demonicsRaycastHit;
    }
}

public class DemonicsRaycastHit
{
    public GameObject collider;
}