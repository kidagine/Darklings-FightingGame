using UnityEngine;

public class InputHistory : MonoBehaviour
{
	public void AddInput()
	{
		Instantiate(new GameObject(), transform);
	}
}
