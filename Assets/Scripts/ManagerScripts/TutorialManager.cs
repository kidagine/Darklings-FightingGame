using System.Collections;
using UnityEngine;

public class TutorialManager : GameManager
{
    private PlayerController _playerOneController;
    private PlayerController _playerTwoController;
    private bool _hasGameStarted;
    private float _countdown = 99.0f;


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

    public override void RoundOver()
    {
        StartCoroutine(RoundOverCoroutine());
    }

    IEnumerator RoundOverCoroutine()
    {
        _hasGameStarted = false;
        Time.timeScale = 0.25f;
        yield return new WaitForSeconds(1.5f);
        StartRound();
    }
}
