using System.Collections;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerPlayTest
{
	private readonly string _testScene = "Assets/_Project/Scenes/TestScenes/TestScene.unity";


	[SetUp]
	public void Setup()
	{
		EditorSceneManager.LoadSceneInPlayMode(_testScene, new LoadSceneParameters(LoadSceneMode.Single, LocalPhysicsMode.None));
	}

	[TearDown]
	public void Teardown()
	{
		GameManager.Instance.StartRound();
	}

	[UnityTest]
	public IEnumerator TakeDamage_Dies_MakesIsDeadTrue()
	{
		// Arrange
		yield return null;
		Player player = Object.FindObjectOfType<Player>();
		PlayerController playerController = Object.FindObjectOfType<PlayerController>();
		AttackSO attackSO = Resources.Load<AttackSO>("_Project/ScriptableObjects/AttackScriptableObjects/TobiDarkAttacks/2L");

		// Act
		playerController.enabled = false;
		player.TakeDamage(attackSO);

		// Assert
		yield return new WaitForSeconds(0.1f);
		Assert.IsTrue(player.IsDead);
	}

	// Player - Attack
	[UnityTest]
	public IEnumerator AttackAction_NormalAttack_MakesIsAttackTrue()
	{
		// Arrange
		yield return null;
		Player player = Object.FindObjectOfType<Player>();
		PlayerController playerController = Object.FindObjectOfType<PlayerController>();

		// Act
		playerController.enabled = false;
		player.AttackAction();

		// Assert
		yield return new WaitForSeconds(0.1f);
		Assert.IsTrue(player.IsAttacking);
	}
}
