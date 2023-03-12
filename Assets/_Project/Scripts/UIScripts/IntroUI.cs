using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class IntroUI : MonoBehaviour
{
    [SerializeField] private PlayerDialogue _playerOneDialogue = default;
    [SerializeField] private TextMeshProUGUI _playerOneName = default;
    [SerializeField] private TextMeshProUGUI _playerTwoName = default;
    private Animator _animator;
    private Audio _audio;
    private int _midDialogueFrame;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _audio = GetComponent<Audio>();

    }

    void FixedUpdate()
    {
        if (DemonicsWorld.WaitFramesOnce(ref _midDialogueFrame))
        {
            GameplayManager.Instance.IsDialogueRunning = true;
            _playerOneDialogue.PlayDialogue();
        }
    }

    public void StartIntro()
    {
        GameplayManager.Instance.IsDialogueRunning = true;
        _animator.Rebind();
        _animator.SetBool("IsIntroRunning", true);
        _midDialogueFrame = 200;
    }

    public void SkipIntro()
    {
        _animator.SetBool("IsIntroRunning", false);
    }

    public void SetPlayerNames(string playerOne, string playerTwo)
    {
        _playerOneName.text = Regex.Replace(playerOne, "([a-z])([A-Z])", "$1 $2"); ;
        _playerTwoName.text = Regex.Replace(playerTwo, "([a-z])([A-Z])", "$1 $2"); ;
    }

    public void PlayTextAppearSoundAnimationEvent()
    {
        _audio.Sound("TextAppear").Play();
    }
}
