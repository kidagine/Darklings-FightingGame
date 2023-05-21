using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class OnlineMainMenu : BaseMenu
{
    [SerializeField] private GameObject _connectingGroup = default;
    [SerializeField] private GameObject _connectedGroup = default;
    [SerializeField] private OnlineSetupMenu _onlineSetupMenu = default;
    [SerializeField] private DemonNameplate _demonNameplate = default;

    async void Awake()
    {
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () =>
        {
            _connectingGroup.SetActive(false);
            _connectedGroup.SetActive(true);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private void OnEnable()
    {
        _demonNameplate.transform.parent.gameObject.SetActive(true);
        _demonNameplate.SetDemonData(_onlineSetupMenu.DemonData);
    }
}