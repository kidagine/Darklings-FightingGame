using UnityEngine;

[CreateAssetMenu(fileName = "Assist Stats", menuName = "Scriptable Objects/Assist Stat", order = 1)]
public class AssistStatsSO : ScriptableObject
{
    [Header("Main")]
    public float assistRecharge = 1;
    public Vector2 assistRotation;
    public float assistSpeed = 0;
    public int assistPriority = 0;
    public Vector2 assistPosition = Vector2.zero;
    public AttackSO attackSO = default;
    public GameObject assistPrefab;
    public ObjectPool assistProjectile;
}
