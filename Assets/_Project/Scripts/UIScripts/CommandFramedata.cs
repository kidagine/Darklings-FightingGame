using UnityEngine;
using TMPro;

public class CommandFramedata : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moveType = default;
    [SerializeField] private TextMeshProUGUI _damage = default;
    [SerializeField] private TextMeshProUGUI _hitStun = default;
    [SerializeField] private TextMeshProUGUI _startUp = default;
    [SerializeField] private TextMeshProUGUI _active = default;
    [SerializeField] private TextMeshProUGUI _recovery = default;


    public void SetFramedata(ArcanaSO command)
    {
        _moveType.text = command.attackTypeEnum.ToString();
        _damage.text = command.damage.ToString();
        _hitStun.text = command.hitStun.ToString();
        _startUp.text = command.startUpFrames.ToString();
        _active.text = command.recoveryFrames.ToString();
        _recovery.text = command.recoveryFrames.ToString();
    }
}
