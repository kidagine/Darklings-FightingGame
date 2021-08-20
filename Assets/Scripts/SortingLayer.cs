using System;
using UnityEngine;

[Serializable]
public enum SortingLayerEnum { Background, Middleground, Foreground, Actor };

public class SortingLayer : MonoBehaviour
{
	[SerializeField] private SortingLayerEnum _sortingLayerEnum = default;

	public SortingLayerEnum SortingLayerEnum { get { return _sortingLayerEnum; } private set { } }
}
