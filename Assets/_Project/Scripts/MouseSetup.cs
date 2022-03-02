using UnityEngine;

public class MouseSetup : MonoBehaviour
{
    void OnApplicationFocus(bool hasFocus)
    {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}
