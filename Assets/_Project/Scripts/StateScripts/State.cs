using Demonics.Sounds;
using UnityEngine;

public class State : MonoBehaviour
{
    public string stateName;
    protected StateMachine _stateMachine;
    protected Rigidbody2D _rigidbody;
    protected DemonicsPhysics _physics;
    protected PlayerAnimator _playerAnimator;
    protected Player _player;
    protected PlayerMovement _playerMovement;
    protected PlayerUI _playerUI;
    protected BaseController _baseController;
    protected BrainController _brainController;
    protected PlayerComboSystem _playerComboSystem;
    protected InputBuffer _inputBuffer;
    protected Audio _audio;


    public void Initialize(StateMachine stateMachine, Rigidbody2D rigidbody, DemonicsPhysics physics, PlayerAnimator playerAnimator, Player player, PlayerMovement playerMovement,
        PlayerUI playerUI, PlayerComboSystem playerComboSystem, InputBuffer inputBuffer, Audio audio)
    {
        _stateMachine = stateMachine;
        _rigidbody = rigidbody;
        _physics = physics;
        _playerAnimator = playerAnimator;
        _player = player;
        _playerMovement = playerMovement;
        _playerUI = playerUI;
        _playerComboSystem = playerComboSystem;
        _inputBuffer = inputBuffer;
        _audio = audio;
    }

    public void SetController(BrainController brainController)
    {
        _brainController = brainController;
        _baseController = brainController.ActiveController;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void Exit() { }
    public virtual bool ToAttackState(InputEnum inputEnum, InputDirectionEnum inputDirectionEnum) { return false; }
    public virtual bool ToArcanaState(InputDirectionEnum inputDirectionEnum) { return false; }
    public virtual bool ToIdleState() { return false; }
    public virtual bool ToGrabState() { return false; }
    public virtual bool ToParryState() { return false; }
    public virtual bool ToRedFrenzyState() { return false; }
    public virtual bool ToDashState(int direction) { return false; }
    public virtual bool ToThrowState() { return false; }
    public virtual bool ToKnockdownState() { return false; }
    public virtual bool ToKnockbackState() { return false; }
    public virtual bool ToHurtState(AttackSO attack) { return true; }
    public virtual bool ToAirborneHurtState(AttackSO attack) { return true; }
    public virtual bool ToBlockState(AttackSO attack) { return false; }
    public virtual bool ToGrabbedState() { return false; }
    public virtual bool ToTauntState() { return false; }
    public virtual bool ToGiveUpState() { return false; }
    public virtual bool AssistCall() { return false; }
}