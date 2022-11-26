using UnityEngine;

public class Pushbox : DemonicsCollider
{
    public void SetIsTrigger(bool state)
    {
        this.IgnoreCollision = state;
    }

    void Awake()
    {
        GizmoColor = Color.blue;
    }

    protected override void InitializeCollisionList()
    {
        DemonicsCollider[] demonicsCollidersArray = FindObjectsOfType<DemonicsCollider>();
        for (int i = 0; i < demonicsCollidersArray.Length; i++)
        {
            if (!demonicsCollidersArray[i].transform.IsChildOf(transform.root))
            {
                if (demonicsCollidersArray[i].TryGetComponent(out Pushbox pushbox))
                {
                    _demonicsColliders.Add(demonicsCollidersArray[i]);
                }
            }
        }
        _demonicsColliders.Remove(this);
    }
}
