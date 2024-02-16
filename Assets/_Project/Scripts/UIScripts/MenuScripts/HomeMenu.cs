using TMPro;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;

public class HomeMenu : BaseMenu
{
    [SerializeField] private PlayerUIRender _playerUIRender = default;
    [SerializeField] private PlayerUIRender _playerOnlineUIRender = default;
    [SerializeField] private TextMeshProUGUI _playerName = default;
    [SerializeField] private TextMeshProUGUI _playerOnlineProfileName = default;
    [SerializeField] private TextMeshProUGUI _playerProfileName = default;
    [SerializeField] private TextMeshProUGUI _onlineInfoText = default;
    [SerializeField] private Animator _onlineInfoAnimator = default;
    [SerializeField] private PlayerStatsSO[] _playerStatsSO = default;


    async private void Awake()
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsAuthorized)
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private void Start()
    {
#if UNITY_WEBGL
        _onlineInfoText.text = "Unavailable on Web";
        _onlineInfoAnimator.Play("ButtonDisable");
#endif
#if !UNITY_WEBGL
        _onlineInfoText.text = "Early Version";
#endif
    }

    public void SetCharacter(int character, int color, string playerName)
    {
        _playerUIRender.gameObject.SetActive(true);
        _playerName.text = playerName;
        _playerProfileName.text = playerName;
        _playerOnlineProfileName.text = playerName;
        _playerUIRender.SetAnimator(_playerStatsSO[character]._animation);
        _playerUIRender.SetSpriteLibraryAsset(color);
        _playerOnlineUIRender.SetAnimator(_playerStatsSO[character]._animation);
        _playerOnlineUIRender.SetSpriteLibraryAsset(color);
        _playerProfileName.text = playerName;
    }
}
