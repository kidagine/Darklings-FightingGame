using UnityEngine;
using TMPro;

public class CommandFramedata : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moveType = default;
    [SerializeField] private TextMeshProUGUI _damage = default;
    [SerializeField] private TextMeshProUGUI _chipDamage = default;
    [SerializeField] private TextMeshProUGUI _startUp = default;
    [SerializeField] private TextMeshProUGUI _active = default;
    [SerializeField] private TextMeshProUGUI _recovery = default;
    [SerializeField] private TextMeshProUGUI _hitAdv = default;
    [SerializeField] private TextMeshProUGUI _blockAdv = default;


    public void SetFramedata(AttackSO command)
    {
        _moveType.text = command.attackTypeEnum.ToString();
        _damage.text = command.damage.ToString();
        _chipDamage.text = command.isArcana ? "250" : "0";
    }
}
