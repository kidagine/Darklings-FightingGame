using UnityEngine;

public class CpuController : BaseController
{
    private Transform _otherPlayer;
    private int _movementInputX;
    private float _distance;
    private float _arcanaTimer;
    private float _attackTimer;
    private float _movementTimer;
    private bool _crouch;
    private bool _jump;
    private bool _reset;

    public void SetOtherPlayer(Transform otherPlayer)
    {
        _otherPlayer = otherPlayer;
    }

    void FixedUpdate()
    {
        if (GameplayManager.Instance.HasGameStarted)
        {
            if (!TrainingSettings.CpuOff || !SceneSettings.IsTrainingMode)
            {
                NetworkInput.ONE_LIGHT_INPUT = false;
                NetworkInput.ONE_MEDIUM_INPUT = false;
                NetworkInput.ONE_HEAVY_INPUT = false;
                NetworkInput.ONE_ARCANA_INPUT = false;
                NetworkInput.ONE_GRAB_INPUT = false;
                NetworkInput.ONE_SHADOW_INPUT = false;
                NetworkInput.TWO_LIGHT_INPUT = false;
                NetworkInput.TWO_MEDIUM_INPUT = false;
                NetworkInput.TWO_HEAVY_INPUT = false;
                NetworkInput.TWO_ARCANA_INPUT = false;
                NetworkInput.TWO_GRAB_INPUT = false;
                NetworkInput.TWO_SHADOW_INPUT = false;
                _reset = false;
                Movement();
                bool attacked = true;
                if (_distance <= 80)
                    Attack();
                if (!attacked)
                    Specials();

            }
            else
            {
                if (!_reset)
                {
                    if (_player.IsPlayerOne)
                    {
                        NetworkInput.ONE_UP_INPUT = false;
                        NetworkInput.ONE_DOWN_INPUT = false;
                        NetworkInput.ONE_RIGHT_INPUT = false;
                        NetworkInput.ONE_LEFT_INPUT = false;
                    }
                    else
                    {
                        NetworkInput.TWO_UP_INPUT = false;
                        NetworkInput.TWO_DOWN_INPUT = false;
                        NetworkInput.TWO_RIGHT_INPUT = false;
                        NetworkInput.TWO_LEFT_INPUT = false;
                    }

                    _reset = true;
                    InputDirection = Vector2Int.zero;
                }
            }
        }
        else
        {
            InputDirection = Vector2Int.zero;
        }
    }

    private void Movement()
    {
        _distance = Mathf.Abs(_otherPlayer.transform.position.x - transform.position.x);
        _movementTimer -= Time.deltaTime;
        _jump = false;
        if (_movementTimer < 0)
        {
            int movementRandom;
            if (_distance <= 80)
            {
                movementRandom = Random.Range(0, 6);
            }
            else
            {
                movementRandom = Random.Range(0, 9);
            }
            int jumpRandom = Random.Range(0, 12);
            int crouchRandom = Random.Range(0, 12);
            int standingRandom = Random.Range(0, 4);
            int dashRandom = Random.Range(0, 8);
            if (_player.IsPlayerOne)
            {
                NetworkInput.ONE_UP_INPUT = false;
            }
            else
            {
                NetworkInput.TWO_UP_INPUT = false;
            }
            switch (movementRandom)
            {
                case 0:
                    if (_player.IsPlayerOne)
                    {
                        NetworkInput.ONE_RIGHT_INPUT = false;
                        NetworkInput.ONE_LEFT_INPUT = false;
                    }
                    else
                    {
                        NetworkInput.TWO_RIGHT_INPUT = false;
                        NetworkInput.TWO_LEFT_INPUT = false;
                    }
                    _movementInputX = 0;
                    break;
                case > 0 and <= 4:
                    _movementInputX = (int)(transform.localScale.x * -1);
                    if (_movementInputX == 1.0f)
                    {
                        if (_player.IsPlayerOne)
                        {
                            NetworkInput.ONE_RIGHT_INPUT = false;
                            NetworkInput.ONE_LEFT_INPUT = true;
                        }
                        else
                        {
                            NetworkInput.TWO_RIGHT_INPUT = false;
                            NetworkInput.TWO_LEFT_INPUT = true;
                        }
                    }
                    else if (_movementInputX == -1.0f)
                    {
                        if (_player.IsPlayerOne)
                        {
                            NetworkInput.ONE_LEFT_INPUT = false;
                            NetworkInput.ONE_RIGHT_INPUT = true;
                        }
                        else
                        {
                            NetworkInput.TWO_LEFT_INPUT = false;
                            NetworkInput.TWO_RIGHT_INPUT = true;
                        }
                    }
                    break;
                case > 5:
                    _movementInputX = (int)(transform.localScale.x * 1);
                    if (_movementInputX == 1.0f)
                    {
                        if (_player.IsPlayerOne)
                        {
                            NetworkInput.ONE_RIGHT_INPUT = false;
                            NetworkInput.ONE_LEFT_INPUT = true;
                        }
                        else
                        {
                            NetworkInput.TWO_RIGHT_INPUT = false;
                            NetworkInput.TWO_LEFT_INPUT = true;
                        }
                    }
                    else if (_movementInputX == -1.0f)
                    {
                        if (_player.IsPlayerOne)
                        {
                            NetworkInput.ONE_LEFT_INPUT = false;
                            NetworkInput.ONE_RIGHT_INPUT = true;
                        }
                        else
                        {
                            NetworkInput.TWO_LEFT_INPUT = false;
                            NetworkInput.TWO_RIGHT_INPUT = true;
                        }
                    }
                    break;
            }
            if (jumpRandom == 2)
            {
                _jump = true;
                _movementInputX = (int)(transform.localScale.x * 1.0f);
                if (_player.IsPlayerOne)
                {
                    NetworkInput.ONE_UP_INPUT = true;
                }
                else
                {
                    NetworkInput.TWO_UP_INPUT = true;
                }
            }
            if (crouchRandom == 2)
            {
                _crouch = true;
                if (_player.IsPlayerOne)
                {
                    NetworkInput.ONE_DOWN_INPUT = true;
                }
                else
                {
                    NetworkInput.TWO_DOWN_INPUT = true;
                }
            }
            if (standingRandom == 2)
            {
                if (_player.IsPlayerOne)
                {
                    NetworkInput.ONE_DOWN_INPUT = false;
                }
                else
                {
                    NetworkInput.TWO_DOWN_INPUT = false;
                }
            }
            InputDirection = new Vector2Int(_movementInputX, 0);
            _movementTimer = Random.Range(0.2f, 0.35f);
        }
    }

    private void Attack()
    {
        if (IsControllerEnabled)
        {
            _attackTimer -= Time.deltaTime;
            if (_attackTimer < 0)
            {
                int lowRandom = Random.Range(0, 2);
                int attackRandom = Random.Range(0, 7);
                if (attackRandom <= 2)
                {
                    if (lowRandom == 0)
                    {
                        if (_player.IsPlayerOne)
                        {
                            NetworkInput.ONE_DOWN_INPUT = false;
                            NetworkInput.ONE_LIGHT_INPUT = true;
                        }
                        else
                        {
                            NetworkInput.TWO_DOWN_INPUT = false;
                            NetworkInput.TWO_LIGHT_INPUT = true;
                        }
                    }
                    else if (lowRandom == 1)
                    {
                        if (_player.IsPlayerOne)
                        {
                            NetworkInput.ONE_DOWN_INPUT = true;
                            NetworkInput.ONE_LIGHT_INPUT = true;
                        }
                        else
                        {
                            NetworkInput.TWO_DOWN_INPUT = true;
                            NetworkInput.TWO_LIGHT_INPUT = true;
                        }
                    }
                }
                else if (attackRandom <= 4 && attackRandom > 2)
                {
                    if (lowRandom == 0)
                    {
                        if (_player.IsPlayerOne)
                        {
                            NetworkInput.ONE_DOWN_INPUT = false;
                            NetworkInput.ONE_MEDIUM_INPUT = true;
                        }
                        else
                        {
                            NetworkInput.TWO_DOWN_INPUT = false;
                            NetworkInput.TWO_MEDIUM_INPUT = true;
                        }
                    }
                    else if (lowRandom == 1)
                    {
                        if (_player.IsPlayerOne)
                        {
                            NetworkInput.ONE_DOWN_INPUT = false;
                            NetworkInput.ONE_MEDIUM_INPUT = true;
                        }
                        else
                        {
                            NetworkInput.TWO_DOWN_INPUT = true;
                            NetworkInput.TWO_MEDIUM_INPUT = true;
                        }
                    }
                }
                else if (attackRandom <= 6 && attackRandom > 4)
                {
                    if (lowRandom == 0)
                    {
                        if (_player.IsPlayerOne)
                        {
                            NetworkInput.ONE_DOWN_INPUT = false;
                            NetworkInput.ONE_HEAVY_INPUT = true;
                        }
                        else
                        {
                            NetworkInput.TWO_DOWN_INPUT = false;
                            NetworkInput.TWO_HEAVY_INPUT = true;
                        }
                    }
                    else if (lowRandom == 1)
                    {
                        if (_player.IsPlayerOne)
                        {
                            NetworkInput.ONE_DOWN_INPUT = true;
                            NetworkInput.ONE_HEAVY_INPUT = true;
                        }
                        else
                        {
                            NetworkInput.TWO_DOWN_INPUT = true;
                            NetworkInput.TWO_HEAVY_INPUT = true;
                        }
                    }
                }
                else
                {
                    if (_player.IsPlayerOne)
                    {
                        NetworkInput.ONE_GRAB_INPUT = true;
                    }
                    else
                    {
                        NetworkInput.TWO_GRAB_INPUT = true;
                    }
                }
                _attackTimer = Random.Range(0.15f, 0.3f);
            }
        }
    }

    private void Specials()
    {
        if (IsControllerEnabled)
        {
            _arcanaTimer -= Time.deltaTime;
            if (_arcanaTimer < 0)
            {
                int arcanaRandom = Random.Range(0, 2);
                int lowRandom = Random.Range(0, 2);
                if (arcanaRandom == 0)
                {
                    if (_player.IsPlayerOne)
                    {
                        NetworkInput.ONE_SHADOW_INPUT = true;
                    }
                    else
                    {
                        NetworkInput.TWO_SHADOW_INPUT = true;
                    }
                }
                else if (arcanaRandom == 1)
                {
                    if (lowRandom == 0)
                    {
                        if (_player.IsPlayerOne)
                        {
                            NetworkInput.ONE_DOWN_INPUT = false;
                            NetworkInput.ONE_ARCANA_INPUT = true;
                        }
                        else
                        {
                            NetworkInput.TWO_DOWN_INPUT = false;
                            NetworkInput.TWO_ARCANA_INPUT = true;
                        }
                    }
                    else
                    {
                        if (_player.IsPlayerOne)
                        {
                            NetworkInput.ONE_DOWN_INPUT = true;
                            NetworkInput.ONE_ARCANA_INPUT = true;
                        }
                        else
                        {
                            NetworkInput.TWO_DOWN_INPUT = true;
                            NetworkInput.TWO_ARCANA_INPUT = true;
                        }

                    }
                    _attackTimer = Random.Range(0.15f, 0.35f);
                    _arcanaTimer = Random.Range(0.4f, 0.85f);
                }
            }
        }
    }

    public override bool Crouch()
    {
        return _crouch;
    }

    public override bool StandUp()
    {
        return !_crouch;
    }

    public override bool Jump()
    {
        return _jump;
    }

    public override void ActivateInput()
    {
        if (!TrainingSettings.CpuOff)
        {
            base.ActivateInput();
        }
    }

    public override void DeactivateInput()
    {
        if (!TrainingSettings.CpuOff)
        {
            base.DeactivateInput();
        }
    }
}