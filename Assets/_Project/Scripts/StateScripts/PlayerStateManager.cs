using Demonics.Sounds;
using UnityEngine;

public class PlayerStateManager : StateMachine
{
	[SerializeField] private State _versusInitialState;
	[SerializeField] private State _trainingInitialState;
	[Header("Components")]
	[SerializeField] private Player _player = default;
	[SerializeField] private PlayerMovement _playerMovement = default;
	[SerializeField] private PlayerAnimator _playerAnimator = default;
	[SerializeField] private PlayerStats _playerStats = default;
	[SerializeField] private BrainController _brainController = default;
	[SerializeField] private PlayerComboSystem _playerComboSystem = default;
	[SerializeField] private InputBuffer _inputBuffer = default;
	[SerializeField] private Audio _audio = default;
	[SerializeField] private Rigidbody2D _rigidbody = default;
	private TrainingMenu _trainingMenu;
	private PlayerUI _playerUI;

	public AirborneHurtState AirborneHurtState { get; private set; }


	public void Initialize(PlayerUI playerUI, TrainingMenu trainingMenu)
	{
		_playerUI = playerUI;
		_trainingMenu = trainingMenu;
		foreach (State state in GetComponents<State>())
		{
			state.Initialize(
			this, _rigidbody, _playerAnimator, _player, _playerMovement, _playerUI, _playerStats, _playerComboSystem, _inputBuffer, _audio
			);
			state.SetController(_brainController);
		}
		AirborneHurtState = GetComponent<AirborneHurtState>();
	}

	public void UpdateStateController()
	{
		foreach (State state in GetComponents<State>())
		{
			state.SetController(_brainController);
		}
	}

	public void ResetToInitialState()
	{
		if (CurrentState != GetInitialState())
		{
			ChangeState(GetInitialState());
		}
	}

	public bool TryToAttackState(InputEnum inputEnum, InputDirectionEnum inputDirectionEnum)
	{
		return CurrentState.ToAttackState(inputEnum, inputDirectionEnum);
	}

	public bool TryToArcanaState()
	{
		return CurrentState.ToArcanaState();
	}

	public bool TryToGrabState()
	{
		return CurrentState.ToGrabState();
	}

	public bool TryToThrowState()
	{
		return CurrentState.ToThrowState();
	}

	public bool TryToKnockdownState()
	{
		return CurrentState.ToKnockdownState();
	}

	public bool TryToKnockbackState()
	{
		return CurrentState.ToKnockbackState();
	}

	public bool TryToTauntState()
	{
		return CurrentState.ToTauntState();
	}

	public bool TryToAssistCall()
	{
		return CurrentState.AssistCall();
	}

	public bool TryToHurtState(AttackSO attack)
	{
		if (attack.causesKnockdown)
		{
			AirborneHurtState.Initialize(attack);
			ChangeState(AirborneHurtState);
		}
		else
		{
			CurrentState.ToHurtState(attack);
		}
		return true;
	}

	public bool TryToGrabbedState()
	{
		return CurrentState.ToGrabbedState();
	}

	public bool TryToBlockState(AttackSO attack)
	{
		return CurrentState.ToBlockState(attack);
	}

	protected override State GetInitialState()
	{
		if (GameManager.Instance.IsTrainingMode)
		{
			return _trainingInitialState;
		}
		else
		{
			return _versusInitialState;
		}
	}

	protected override void OnStateChange()
	{
		_trainingMenu.SetState(_player.IsPlayerOne, CurrentState.stateName);
	}
}
