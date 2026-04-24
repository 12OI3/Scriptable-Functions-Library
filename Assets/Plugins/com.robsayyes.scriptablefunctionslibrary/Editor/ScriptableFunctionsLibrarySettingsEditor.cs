using UnityEditor;
using UnityEngine;

namespace ScriptableFunctionsLibrary
{
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
            this.OnDeleteBtnGUI();
            EditorGUILayout.BeginVertical(EditorStyles.textArea);
            EditorGUILayout.LabelField($"WIP", EditorStyles.wordWrappedLabel);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }

        public void OnDeleteBtnGUI()
        {
            if(GUILayout.Button("Delete Library"))
            {
                ScriptableFunctionsLibraryDeleteEditor.ShowWindow();
            }
        }
    }
}