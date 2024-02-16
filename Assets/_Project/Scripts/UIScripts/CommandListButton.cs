using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommandListButton : MonoBehaviour
{
    [SerializeField] private CommandListMenu _commandListMenu = default;
    [SerializeField] private TextMeshProUGUI _commandName = default;
    [SerializeField] private Image _knockdownImage = default;
    [SerializeField] private Image _reversalImage = default;
    [SerializeField] private Image _projectileImage = default;

    private ArcanaSO _arcanaCommand;
    private AttackSO _attackCommand;


    public void SetData(ArcanaSO command)
    {
        _arcanaCommand = command;
        _commandName.text = _arcanaCommand.moveName;
        _reversalImage.gameObject.SetActive(false);
        _knockdownImage.gameObject.SetActive(false);
        _projectileImage.gameObject.SetActive(false);
        if (_arcanaCommand.reversal)
            _reversalImage.gameObject.SetActive(true);
        if (_arcanaCommand.causesKnockdown)
            _knockdownImage.gameObject.SetActive(true);
        if (_arcanaCommand.isProjectile)
            _projectileImage.gameObject.SetActive(true);
    }

    public void SetData(AttackSO command)
    {
        _attackCommand = command;
    }

    public void UpdateShowcase()
    {
        if (_arcanaCommand)
            _commandListMenu.SetCommandListShowcase(_arcanaCommand);
    }

    public void UpdateShowcaseNormal()
    {
        _commandListMenu.SetCommandListShowcase(_attackCommand);
    }
}
