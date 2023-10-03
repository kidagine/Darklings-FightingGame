using TMPro;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;

public class HomeMenu : BaseMenu
{
    [SerializeField] private TopBarMenu _topBarMenu = default;
    [SerializeField] private PlayerUIRender _playerUIRender = default;
    [SerializeField] private TextMeshProUGUI _playerName = default;
    [SerializeField] private TextMeshProUGUI _playerProfileName = default;
    [SerializeField] private PlayerStatsSO[] _playerStatsSO = default;


    async private void Awake()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public void SetCharacter(int character, int color, string playerName)
    {
        _playerUIRender.gameObject.SetActive(true);
        _playerName.text = playerName;
        _playerProfileName.text = playerName;
        _playerUIRender.SetAnimator(_playerStatsSO[character]._animation);
        _playerUIRender.SetSpriteLibraryAsset(color);
    }

    private void OnEnable()
    {
        _topBarMenu.SetMenuTitle("");
    }
}
