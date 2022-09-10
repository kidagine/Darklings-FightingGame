using UnityEngine;

public class DemonicsPhysics : MonoBehaviour
{
	public static int Frame;
	private float _tick;


	void Awake()
	{
		Application.targetFrameRate = 60;
		QualitySettings.vSyncCount = 1;
		Time.fixedDeltaTime = 0.01667f;
	}

	void Update()
	{
		_tick += Time.deltaTime;
		while (_tick >= Time.fixedDeltaTime)
		{
			_tick -= Time.fixedDeltaTime;
			Physics2D.Simulate(Time.fixedDeltaTime);
		}
	}

	void FixedUpdate()
	{
		Frame++;
	}
}
