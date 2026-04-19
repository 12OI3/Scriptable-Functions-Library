using UnityEditor;
using UnityEngine;

namespace ScriptableFunctionsLibrary
{
    public class ScriptableFunctionPresetDrawer : Editor
    {
        public static void Draw(SerializedProperty preset)
        {

            string name = preset.FindPropertyRelative("ID").stringValue;
            bool enable = preset.FindPropertyRelative("Enable").boolValue;

            EditorGUILayout.BeginHorizontal();

            Color originalColor = GUI.color;
            if(!enable)
                GUI.color = Color.gray; 
            if(GUILayout.Button(enable ? "Active" : "Inactive", GUILayout.Width(100)))
            {
                preset.FindPropertyRelative("Enable").boolValue = !enable;
            }
            GUI.color = originalColor;

            EditorGUILayout.LabelField(name);

            EditorGUILayout.EndHorizontal();
        }
    }
}