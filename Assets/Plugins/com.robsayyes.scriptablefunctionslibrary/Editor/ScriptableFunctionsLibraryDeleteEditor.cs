using UnityEditor;
using UnityEngine;

namespace ScriptableFunctionsLibrary
{
    public class ScriptableFunctionsLibraryDeleteEditor : EditorWindow
    {

        public static void ShowWindow()
        {
            GetWindow(typeof(ScriptableFunctionsLibraryDeleteEditor), false, "Delete Library");	
        }
        
        void OnGUI()
        {
            
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
        }
    }
}