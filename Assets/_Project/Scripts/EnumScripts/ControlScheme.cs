using System;
using UnityEngine;

namespace Demonics
{
	[Serializable]
	public enum ControlSchemeEnum { KeyboardMouse, Xbox, Dualshock };

	public class ControlScheme : MonoBehaviour
	{
		[SerializeField] private ControlSchemeEnum _controlSchemeEnum = default;

		public ControlSchemeEnum ControlSchemesEnums { get { return _controlSchemeEnum; } private set { } }
	}
}
