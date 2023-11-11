using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SpriteShadersUltimate
{
    [CustomEditor(typeof(WindParallaxSSU))]
    public class WindParallaxSSUEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.richText = true;

            EditorGUILayout.BeginVertical("Helpbox");
            GUI.color = new Color(1, 1, 1, 0.7f);
            EditorGUILayout.LabelField("Fixes <b>parallax</b> or <b>movement</b> issues for the <b>Wind</b> shader.", style);
            EditorGUILayout.LabelField("Attach this to <b>sprite renderers</b> and enable <b>Is Parallax</b>.", style);
            EditorGUILayout.LabelField("Set's <b>X Position</b> on  <b>Awake()</b> to a static value.", style);
            GUI.color = Color.white;
            EditorGUILayout.EndVertical();
        }
    }
}