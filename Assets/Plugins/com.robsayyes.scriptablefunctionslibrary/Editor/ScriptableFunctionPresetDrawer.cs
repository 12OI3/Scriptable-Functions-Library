#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ScriptableFunctionsLibrary
{
    /// <summary>
    /// Drawer for each scriptable function item based on preset recieved
    /// </summary>
    public class ScriptableFunctionPresetDrawer : Editor
    {
        public static void Draw(SerializedProperty preset)
        {
            
            // Get attributes
            string name = preset.FindPropertyRelative("ID").stringValue;
            bool enable = preset.FindPropertyRelative("Enable").boolValue;
            string tip = preset.FindPropertyRelative("ToolTip").stringValue;

            EditorGUILayout.BeginHorizontal();

            // Draw active/inactive btn
            Color originalColor = GUI.color;
            if(!enable)
                GUI.color = Color.gray; 
            if(GUILayout.Button(enable ? "Active" : "Inactive", GUILayout.Width(100)))
            {
                preset.FindPropertyRelative("Enable").boolValue = !enable;
            }
            GUI.color = originalColor;

            // Draw label and tool tip
            EditorGUILayout.LabelField(name, GUILayout.ExpandWidth(true));
            var style = EditorStyles.textArea;
            style.wordWrap = false;
            EditorGUILayout.LabelField(new GUIContent(tip, tip == "" ? "" : tip + " "), style, GUILayout.Width(80));

            EditorGUILayout.EndHorizontal();
        }
    }
}
#endif