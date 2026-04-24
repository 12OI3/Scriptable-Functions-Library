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
            string tip = preset.FindPropertyRelative("ToolTip").stringValue;

            EditorGUILayout.BeginHorizontal();

            Color originalColor = GUI.color;
            if(!enable)
                GUI.color = Color.gray; 
            if(GUILayout.Button(enable ? "Active" : "Inactive", GUILayout.Width(100)))
            {
                preset.FindPropertyRelative("Enable").boolValue = !enable;
            }
            GUI.color = originalColor;

            EditorGUILayout.LabelField(name, GUILayout.ExpandWidth(true));
            var style = EditorStyles.textArea;
            style.wordWrap = false;
            EditorGUILayout.LabelField(new GUIContent(tip, tip == "" ? "" : tip + " "), style, GUILayout.Width(80));
            // Rect rect = EditorGUILayout.GetControlRect(GUILayout.Width(80));
            // EditorGUI.TextArea(rect, tip);
            // Event e = Event.current;
            // bool isHovering = rect.Contains(e.mousePosition);
            // if (isHovering)
            // {
            //     GUILayout.Label(tip);
            // }
            // EditorGUILayout.TextArea(tip, GUILayout.Width(80));

            EditorGUILayout.EndHorizontal();
        }
    }
}