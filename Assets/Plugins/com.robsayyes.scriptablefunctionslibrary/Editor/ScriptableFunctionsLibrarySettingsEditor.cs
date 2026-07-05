#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ScriptableFunctionsLibrary
{
    
    /// <summary>
    /// Settings editor window
    /// </summary>
    public class ScriptableFunctionsLibrarySettingsEditor : EditorWindow
    {

        private Vector2 ScrollPosition;
        
        public static void ShowWindow()
        {
            GetWindow(typeof(ScriptableFunctionsLibrarySettingsEditor), false, "Scriptable Function Library Settings");	
        }

        void OnGUI()
        {
            
            this.ScrollPosition = EditorGUILayout.BeginScrollView(this.ScrollPosition);
            EditorGUILayout.BeginVertical(EditorStyles.textArea);
            EditorGUILayout.LabelField($"WIP", EditorStyles.wordWrappedLabel);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }
    }
}
#endif