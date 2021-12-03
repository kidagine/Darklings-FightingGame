using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cinemachine;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;

public class GameplayScenesProvider : IEnumerable<string>
{
	IEnumerator<string> IEnumerable<string>.GetEnumerator()
	{
		foreach (var scene in EditorBuildSettings.scenes)
		{
			if (!scene.enabled || scene.path == null || !Path.GetFileName(scene.path).StartsWith("Game"))
				continue;

			yield return scene.path;
		}
	}

	public IEnumerator GetEnumerator() => ((IEnumerable<string>)this).GetEnumerator();
}

[TestFixture]
[TestFixtureSource(typeof(GameplayScenesProvider))]
public class GameSceneValidationEditTest
{
	private readonly string _scenePath;
	private Scene _scene;


	public GameSceneValidationEditTest(string scenePath)
	{
		_scenePath = scenePath;
	}


	[OneTimeSetUp]
	public void Setup()
	{
		_scene = SceneManager.GetSceneAt(0);
		if (SceneManager.sceneCount > 1 || _scene.path != _scenePath)
		{
			_scene = EditorSceneManager.OpenScene(_scenePath, OpenSceneMode.Single);
		}
	}

	[Test]
	public void CheckAllNeededObjectsExist()
	{
		// Arrange
		bool doAllNeededObjectsExist = true;
		List<GameObject> neededGameObjects = new List<GameObject>();
		neededGameObjects.Add(GameObject.FindObjectOfType<MouseSetup>().gameObject);
		neededGameObjects.Add(GameObject.FindObjectOfType<GameManager>().gameObject);
		neededGameObjects.Add(GameObject.FindObjectOfType<Camera>().gameObject);
		neededGameObjects.Add(GameObject.FindObjectOfType<CinemachineVirtualCamera>().gameObject);
		neededGameObjects.Add(GameObject.FindObjectOfType<CinemachineTargetGroup>().gameObject);
		neededGameObjects.Add(GameObject.Find("---SETUP---"));
		neededGameObjects.Add(GameObject.Find("---IN GAME---"));
		neededGameObjects.Add(GameObject.Find("Start_canvas"));
		neededGameObjects.Add(GameObject.Find("Timer_canvas"));
		neededGameObjects.Add(GameObject.Find("FirstPlayer_canvas"));
		neededGameObjects.Add(GameObject.Find("SecondPlayer_canvas"));
		neededGameObjects.Add(GameObject.Find("Pause_canvas"));
		neededGameObjects.Add(GameObject.Find("MatchOver_canvas"));
		neededGameObjects.Add(GameObject.Find("Training_group"));
		neededGameObjects.Add(GameObject.Find("Main_eventSystem"));
		neededGameObjects.Add(GameObject.Find("CameraConfiner_polygonCollider"));
		neededGameObjects.Add(GameObject.Find("Floor_boxCollider"));
		neededGameObjects.Add(GameObject.Find("LeftWall_boxCollider"));
		neededGameObjects.Add(GameObject.Find("RightWall_boxCollider"));

		// Act
		for (int i = 0; i < neededGameObjects.Count; i++)
		{
			if (neededGameObjects[i] == null)
			{
				doAllNeededObjectsExist = false;
			}
			if (neededGameObjects[i].TryGetComponent(out BoxCollider2D boxCollider))
			{
				if (boxCollider.enabled == false)
				{
					doAllNeededObjectsExist = false;
				}
			}
		}

		// Assert
		Assert.True(doAllNeededObjectsExist);
	}

	[Test]
	public void CheckCameraSettingsCorrect()
	{
		// Arrange
		bool areCameraSettingsCorrect = true;
		Camera camera = GameObject.FindObjectOfType<Camera>();

		// Act
		if (camera.orthographic)
		{
			if (camera.TryGetComponent(out AudioListener audioListener))
			{
				if (camera.TryGetComponent(out PixelPerfectCamera pixelPerfectCamera))
				{
					if (pixelPerfectCamera.assetsPPU == 16 && pixelPerfectCamera.refResolutionX == 320 && pixelPerfectCamera.refResolutionY == 180)
					{
						if (!camera.TryGetComponent(out CinemachineBrain cinemachineBrain))
						{
							areCameraSettingsCorrect = false;
						}
					}
					else
					{
						areCameraSettingsCorrect = false;
					}
				}
				else
				{
					areCameraSettingsCorrect = false;
				}
			}
			else
			{
				areCameraSettingsCorrect = false;
			}
		}
		else
		{
			areCameraSettingsCorrect = false;
		}

		// Assert
		Assert.True(areCameraSettingsCorrect);
	}

	[Test]
	public void CheckAllCanvasesSettingsCorrect()
	{
		// Arrange
		bool areCanvasesSettingsCorrect = true;
		CanvasScaler[] canvasScalers = GameObject.FindObjectsOfType<CanvasScaler>();

		// Act
		for (int i = 0; i < canvasScalers.Length; i++)
		{
			if (canvasScalers[i].referenceResolution != new Vector2(1920.0f, 1080.0f))
			{
				areCanvasesSettingsCorrect = false;
			}
		}

		// Assert
		Assert.True(areCanvasesSettingsCorrect);
	}
}
