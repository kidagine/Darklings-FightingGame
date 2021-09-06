using System.Collections;
using UnityEngine;

public class TutorialManager : GameManager
{
    private Vector2 _lastPlayerOnePosition;
    private Vector2 _lastPlayerTwoPosition;
    private bool _reverseReset;


    public override void StartRound()
    {
        _playerOneController = _playerOne.GetComponent<PlayerController>();
        _playerTwoController = _playerTwo.GetComponent<PlayerController>();
        _playerOne.ResetPlayer();
        _playerTwo.ResetPlayer();
        _playerOne.ResetLives();
        _playerTwo.ResetLives();
        _leftStopper.SetActive(false);
        _rightStopper.SetActive(false);
        _playerOne.transform.position = new Vector2(-3.5f, -4.75f);
        _playerTwo.transform.position = new Vector2(3.5f, -4.75f);
    }

    public override void ResetRound(Vector2 movementInput)
    {
        _playerOneController = _playerOne.GetComponent<PlayerController>();
        _playerTwoController = _playerTwo.GetComponent<PlayerController>();
        _playerOne.ResetPlayer();
        _playerTwo.ResetPlayer();
        _playerOne.ResetLives();
        _playerTwo.ResetLives();
        _leftStopper.SetActive(false);
        _rightStopper.SetActive(false);
        if (movementInput.y > 0.0f)
        {
            _reverseReset = !_reverseReset;
        }
        if (movementInput.x > 0.0f)
        {
            if (_reverseReset)
            {
                _playerOne.transform.position = new Vector2(8.5f, -4.75f);
                _playerTwo.transform.position = new Vector2(5.5f, -4.75f);
            }
            else
            {
                _playerOne.transform.position = new Vector2(5.5f, -4.75f);
                _playerTwo.transform.position = new Vector2(8.5f, -4.75f);
            }
        }
        else if (movementInput.x < 0.0f)
        {
            if (_reverseReset)
            {
                _playerOne.transform.position = new Vector2(-5.5f, -4.75f);
                _playerTwo.transform.position = new Vector2(-8.5f, -4.75f);
            }
            else
            {
                _playerOne.transform.position = new Vector2(-8.5f, -4.75f);
                _playerTwo.transform.position = new Vector2(-5.5f, -4.75f);
            }
        }
        else if (movementInput.y < 0.0f)
        {
            if (_reverseReset)
            {
                _playerOne.transform.position = new Vector2(3.5f, -4.75f);
                _playerTwo.transform.position = new Vector2(-3.5f, -4.75f);
            }
            else
            {
                _playerOne.transform.position = new Vector2(-3.5f, -4.75f);
                _playerTwo.transform.position = new Vector2(3.5f, -4.75f);
            }
        }
        else
        {
            if (_reverseReset)
            {
                _playerOne.transform.position = new Vector2(3.5f, -4.75f);
                _playerTwo.transform.position = new Vector2(-3.5f, -4.75f);
            }
            else
            {
                _playerOne.transform.position = new Vector2(-3.5f, -4.75f);
                _playerTwo.transform.position = new Vector2(3.5f, -4.75f);
            }
        }
        _lastPlayerOnePosition = _playerOne.transform.position;
        _lastPlayerTwoPosition = _playerTwo.transform.position;
    }

    public override void RoundOver()
    {
        StartCoroutine(RoundOverCoroutine());
    }

    IEnumerator RoundOverCoroutine()
    {
        HasGameStarted = false;
        Time.timeScale = 0.25f;
        yield return new WaitForSeconds(1.5f);
        StartRound();
    }
}
