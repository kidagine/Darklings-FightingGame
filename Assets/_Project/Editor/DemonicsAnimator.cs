using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DemonicsAnimator : EditorWindow
{
    private Vector2 _scrollPosition;
    private AnimationSO _animation;
    private AnimationCelsGroup _animationCelsGroup;
    [MenuItem("Demonics/Animator")]
    private static void ShowWindow()
    {
        GetWindow(typeof(DemonicsAnimator));
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal("box");

        GUILayout.BeginVertical("box", GUILayout.Width(250));
        _animation = EditorGUILayout.ObjectField("Animation", _animation, typeof(AnimationSO), true) as AnimationSO;
        ScriptableObject target = _animation;
        SerializedObject scriptableObject = new SerializedObject(target);
        SerializedProperty stringsProperty = scriptableObject.FindProperty("animationCelsGroup");
        //_animationCelsGroup = stringsProperty as AnimationCelsGroup;
        Debug.Log(stringsProperty.FindPropertyRelative(""));
        EditorGUILayout.PropertyField(stringsProperty, true);
        GUILayout.EndVertical();

        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, true, false);
        GUILayout.BeginHorizontal("box", GUILayout.Width(1000));
        GUILayout.Button("Frame", GUILayout.Height(200), GUILayout.Width(150));
        GUILayout.Button("Frame", GUILayout.Height(200), GUILayout.Width(150));
        GUILayout.Button("Frame", GUILayout.Height(200), GUILayout.Width(150));
        GUILayout.Button("Frame", GUILayout.Height(200), GUILayout.Width(150));
        GUILayout.Button("Frame", GUILayout.Height(200), GUILayout.Width(150));
        GUILayout.Button("Frame", GUILayout.Height(200), GUILayout.Width(150));
        GUILayout.Button("Frame", GUILayout.Height(200), GUILayout.Width(150));
        GUILayout.Button("Frame", GUILayout.Height(200), GUILayout.Width(150));
        GUILayout.Button("Frame", GUILayout.Height(200), GUILayout.Width(150));
        GUILayout.Button("Frame", GUILayout.Height(200), GUILayout.Width(150));
        GUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();

        GUILayout.EndHorizontal();
    }
}
