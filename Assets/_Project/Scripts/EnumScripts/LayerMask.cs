using System;
using UnityEngine;

namespace Demonics.Enum
{
	[Serializable]
	public enum LayerMaskEnum { Ground, Default, UI, Player, Hurtbox, Hitbox, Groundbox };

	public class LayerMask : MonoBehaviour
	{
		[SerializeField] private LayerMaskEnum _layerMaskEnum = default;

		public LayerMaskEnum LayerMaskEnum { get { return _layerMaskEnum; } private set { } }
	}
}