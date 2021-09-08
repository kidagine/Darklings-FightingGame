using UnityEngine;

public class Raycast : MonoBehaviour
{
	public static RaycastHit2D Cast(Vector2 start, Vector2 dir, float length, LayerMaskEnum layerMask, Color color = default)
	{
#if UNITY_EDITOR
		if (color == default)
		{
			color = Color.magenta;
		}
		Debug.DrawRay(start, dir * length, color);
#endif
		return Physics2D.Raycast(start, dir, length, LayerProvider.GetLayerMask(layerMask), 0);
	}
}