#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ScriptableFunctionsLibrary
{

    /// <summary>
    /// Guide editor window
    /// </summary>
    public class ScriptableFunctionsLibraryGuideEditor : EditorWindow
    {

        private Vector2 ScrollPosition;
        
        public static void ShowWindow()
        {
            GetWindow(typeof(ScriptableFunctionsLibraryGuideEditor), false, "Scriptable Function Library Guide");	
        }

        void OnGUI()
        {
            
            this.ScrollPosition = EditorGUILayout.BeginScrollView(this.ScrollPosition);
            EditorGUILayout.LabelField("Introduction", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical(EditorStyles.textArea);
            EditorGUILayout.LabelField($"Scriptable Functions Library is a tool that generates a ScriptableObject used to register and manage callable functions by string ID.", EditorStyles.wordWrappedLabel);
            EditorGUILayout.LabelField($"It supports a variety of use cases, but is particularly suited for technical designers, as some data may need to be defined directly in code.", EditorStyles.wordWrappedLabel);
            EditorGUILayout.LabelField($"This structure is not recommended for production environments, as it may not scale well for larger systems.", EditorStyles.wordWrappedLabel);
            EditorGUILayout.LabelField($"However, it is very useful during prototyping, helping you avoid large switch-case statements and making function organization more manageable.", EditorStyles.wordWrappedLabel);
            EditorGUILayout.LabelField($"\n", EditorStyles.wordWrappedLabel);
            EditorGUILayout.LabelField($"This tool automatically registers all non-abstract classes that inherit from \"ScriptableFunction\". For example, you might define a function like this:", EditorStyles.wordWrappedLabel);
            EditorGUILayout.LabelField($"public class DemoFunction : ScriptableFunction\n{{\n     public void Execute()\n     {{\n          Debug.Log(\"Hello World!\");\n     }}\n}}", EditorStyles.wordWrappedLabel);
            EditorGUILayout.LabelField($"At runtime, you can access and execute the function like this:", EditorStyles.wordWrappedLabel);
            EditorGUILayout.LabelField($"void Start()\n{{\n     (ScriptableFunctionsLibraryManager.Library[DemoFunction] as DemoFunction).Execute();\n}}", EditorStyles.wordWrappedLabel);
            EditorGUILayout.LabelField($"It is recommended to create an abstract class that inherits from \"ScriptableFunction\", and then have your concrete function classes inherit from that abstract base. This allows you to define shared structure and logic in one place, while derived classes handle specific behavior and parameters. The structure also supports async/await, allowing you to implement asynchronous functions within your classes.", EditorStyles.wordWrappedLabel);
            EditorGUILayout.LabelField($"In the editor window, you can view basic information and all registered functions, as well as enable or disable them as needed. You can also add ToolTip for your class, which will be shown on the library tool, too More features will be added in future updates.", EditorStyles.wordWrappedLabel);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }
    }
}
#endif