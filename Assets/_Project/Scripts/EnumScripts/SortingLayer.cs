using System;
using UnityEngine;

namespace Demonics.Enum
{
	[Serializable]
	public enum SortingLayerEnum { Background, Middleground, Foreground, Actor };

	public class SortingLayer : MonoBehaviour
	{
		[SerializeField] private SortingLayerEnum _sortingLayerEnum = default;

		public SortingLayerEnum SortingLayerEnum { get { return _sortingLayerEnum; } private set { } }
	}
}