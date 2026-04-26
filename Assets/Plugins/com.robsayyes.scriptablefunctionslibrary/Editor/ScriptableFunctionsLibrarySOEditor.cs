
using UnityEditor;
using UnityEngine;

namespace ScriptableFunctionsLibrary
{
    
    /// <summary>
    /// Scriptable object editor window, mainly just override the datas and pointing out the tool window
    /// </summary>
    [CustomEditor(typeof(ScriptableFunctionsLibrarySO))]
    public class ScriptableFunctionsLibrarySOEditor : Editor
    {
        public override void OnInspectorGUI()
        {

            // Button for open tool window
            if (GUILayout.Button("Open Tool Window"))
                EditorWindow.GetWindow(typeof(ScriptableFunctionsLibraryEditor), false, "Scriptable Function Library");	

            // Text for explaination what this file is
            GUILayout.Label("This is the scriptable object for saving your library's data. Don't change path and name of this file.", EditorStyles.wordWrappedLabel);

            // base.OnInspectorGUI();
        }
    }
}