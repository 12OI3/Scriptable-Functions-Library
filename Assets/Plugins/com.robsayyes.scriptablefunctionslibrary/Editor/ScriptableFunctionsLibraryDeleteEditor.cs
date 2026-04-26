using UnityEditor;
using UnityEngine;

namespace ScriptableFunctionsLibrary
{

    /// <summary>
    /// Delete double editor window
    /// </summary>
    public class ScriptableFunctionsLibraryDeleteEditor : EditorWindow
    {

        public static void ShowWindow()
        {
            EditorWindow window = GetWindowWithRect(typeof(ScriptableFunctionsLibraryDeleteEditor), new Rect(0f, 0f, 200f, 50f), false, "Delete Library");	
        }
        
        void OnGUI()
        {
            
            // Draw questions, and two answer btns
            EditorGUILayout.Space(4f);
            EditorGUILayout.LabelField("Are you sure?", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("YES"))
            {
                ScriptableFunctionsLibraryEditor.DeleteLibrary();
                this.Close();
            }
            else if(GUILayout.Button("NO"))
            {
                this.Close();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(4f);
        }
    }
}