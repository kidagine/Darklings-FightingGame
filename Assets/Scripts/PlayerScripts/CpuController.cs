using System.Collections;
using UnityEngine;

public class CpuController : BaseController
{
    private Transform _otherPlayer;
    private Coroutine _movementCoroutine;
    private float _movementInputX;
    private float _distance;


    void Awake()
    {
        _player = GetComponent<Player>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

	private void Start()
	{
        _movementCoroutine = StartCoroutine(MovementCoroutine());
        StartCoroutine(AttackCoroutine());
    }

    public void SetOtherPlayer(Transform otherPlayer)
    {
        _otherPlayer = otherPlayer;
    }

    private void Update()
    {
        _distance = Mathf.Abs(_otherPlayer.transform.position.x - transform.position.x);
        _playerMovement.MovementInput = new Vector2(_movementInputX, 0.0f);
    }

    IEnumerator MovementCoroutine()
    {
        float waitTime;
        while (_isControllerEnabled)
        {
            int movementRandom;
            if (_distance <= 6.5f)
            {
                movementRandom = Random.Range(0, 3);
            }
            else
            {
                movementRandom = Random.Range(0, 6);
            }
            int jumpRandom = Random.Range(0, 8);
            int crouchRandom = Random.Range(0, 12);
            int standingRandom = Random.Range(0, 4);
            switch (movementRandom)
            {
                case 1:
                    _movementInputX = 0.0f;
                    waitTime = Random.Range(0.2f, 0.35f);
                    break;
                case 2:
                    _movementInputX = transform.localScale.x * -1.0f;
                    waitTime = Random.Range(0.5f, 1.2f);
                    break;
                default:
                    _movementInputX = transform.localScale.x * 1.0f;
                    waitTime = Random.Range(0.5f, 1.2f);
                    break;
            }
            if (jumpRandom == 2)
            {
                if (GameManager.Instance.HasGameStarted)
                {
                    _playerMovement.JumpAction();
                }
            }
            if (crouchRandom == 2)
            {
                _playerMovement.CrouchAction();
            }
            if (_playerMovement.IsCrouching)
            {
                if (standingRandom == 2)
                {
                    _playerMovement.StandUpAction();
                }
            }
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator AttackCoroutine()
    {
        while (_isControllerEnabled)
        {
            if (_distance <= 6.5f)
            {
                if (GameManager.Instance.HasGameStarted)
                {
                    float attackWaitRandom = Random.Range(0.25f, 0.8f);
                    _player.AttackAction();
                    yield return new WaitForSeconds(attackWaitRandom);
                }
            }
            yield return null;
        }
    }

    public override void ActivateInput()
    {
        base.ActivateInput();
        StartCoroutine(MovementCoroutine());
        StartCoroutine(AttackCoroutine());
    }
    public override void DeactivateInput()
    {
        base.DeactivateInput();
        StopCoroutine(_movementCoroutine);
    }
}