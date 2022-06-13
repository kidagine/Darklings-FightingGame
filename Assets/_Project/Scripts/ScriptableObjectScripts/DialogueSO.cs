using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Dialogue", menuName = "Scriptable Objects/Player Dialogue", order = 1)]
public class DialogueSO : ScriptableObject
{
	public DialogueData[] playerOneDialogues;
	public DialogueData[] playerTwoDialogues;
}

[Serializable]
public struct DialogueData
{
	public CharacterTypeEnum character;
	[TextArea(3, 5)]
	public string sentence;
}