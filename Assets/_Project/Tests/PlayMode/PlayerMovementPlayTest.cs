using System.Collections;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerMovementPlayTest
{
	private readonly string _testScene = "Assets/_Project/Scenes/TestScenes/TestScene.unity";


	[UnitySetUp]
	public IEnumerator Setup()
	{
		EditorSceneManager.LoadSceneInPlayMode(_testScene, new LoadSceneParameters(LoadSceneMode.Single, LocalPhysicsMode.None));
		yield return null;
	}

	[TearDown]
	public void Teardown()
	{
		GameManager.Instance.StartRound();
	}

	// PlayerMovement - Jump
	[UnityTest]
	public IEnumerator JumpAction_GroundJump_MakesIsGroundedFalse()
	{
		// Arrange
		PlayerMovement playerMovement = Object.FindObjectOfType<PlayerMovement>();

		// Act
		playerMovement.JumpAction();

		// Assert
		yield return new WaitForSeconds(0.5f);
		Assert.IsFalse(playerMovement.IsGrounded);
	}

	[UnityTest]
	public IEnumerator JumpAction_DoubleJump_MakesCanDoubleJumpFalse()
	{
		// Arrange
		PlayerMovement playerMovement = Object.FindObjectOfType<PlayerMovement>();
		playerMovement.JumpAction();
		yield return new WaitForSeconds(0.5f);

		// Act
		playerMovement.JumpAction();

		// Assert
		yield return null;
		Assert.IsFalse(playerMovement.CanDoubleJump);
	}

	[UnityTest]
	public IEnumerator JumpAction_NoJump_NoChange()
	{
		// Arrange
		PlayerMovement playerMovement = Object.FindObjectOfType<PlayerMovement>();
		playerMovement.JumpAction();
		yield return new WaitForSeconds(0.5f);
		playerMovement.JumpAction();
		yield return new WaitForSeconds(0.5f);

		// Act
		playerMovement.JumpAction();

		// Assert
		yield return null;
		Assert.IsFalse(playerMovement.IsGrounded);
		Assert.IsFalse(playerMovement.CanDoubleJump);
	}

	// PlayerMovement - Crouch
	[UnityTest]
	public IEnumerator CrouchAction_Crouches_MakesIsCrouchingTrue()
	{
		// Arrange
		PlayerMovement playerMovement = Object.FindObjectOfType<PlayerMovement>();
		PlayerController playerController = Object.FindObjectOfType<PlayerController>();

		// Act
		playerController.enabled = false;
		playerMovement.CrouchAction();

		// Assert
		yield return new WaitForSeconds(0.5f);
		Assert.IsTrue(playerMovement.IsCrouching);
	}

	// PlayerMovement - Stand
	[UnityTest]
	public IEnumerator StandAction_StandsUp_MakesIsCrouchingFalse()
	{
		// Arrange
		PlayerMovement playerMovement = Object.FindObjectOfType<PlayerMovement>();
		PlayerController playerController = Object.FindObjectOfType<PlayerController>();
		playerMovement.CrouchAction();

		// Act
		playerController.enabled = false;
		playerMovement.StandUpAction();

		// Assert
		yield return new WaitForSeconds(0.5f);
		Assert.IsFalse(playerMovement.IsCrouching);
	}

	// PlayerMovement - Dash
	[UnityTest]
	public IEnumerator DashAction_DashesForward_MakesIsDashingTrue()
	{
		// Arrange
		PlayerMovement playerMovement = Object.FindObjectOfType<PlayerMovement>();
		PlayerController playerController = Object.FindObjectOfType<PlayerController>();

		// Act
		playerController.enabled = false;
		playerMovement.DashAction(1.0f);

		// Assert
		yield return new WaitForSeconds(0.1f);
		Assert.IsTrue(playerMovement.IsDashing);
	}
}
