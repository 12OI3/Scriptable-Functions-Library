
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ScriptableFunctionsLibrary
{
    public class ScriptableFunctionsLibraryEditor : EditorWindow
    {

        /// <summary>
        /// Sciprable object reference in editor script
        /// </summary>
        private ScriptableFunctionsLibrarySO LibrarySO;
        
        /// <summary>
        /// Show window function
        /// </summary>
        [MenuItem("Window/Scriptable Functions Library")]
        public static void ShowWindow()
        {
            if(Application.isPlaying)
            {
                Debug.LogWarning("Scriptable Function Library: Tool cannot be used during play mode.");
                return;
            }
            GetWindow(typeof(ScriptableFunctionsLibraryEditor), false, "Scriptable Function Library");	
        }
        
        /// <summary>
        /// Auto update library everytime when reload script (including open Unity)
        /// </summary>
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void DidReloadScripts()
        {
            UpdateLibrary();
        }

        #region OnGUI Functions

        /// <summary>
        /// Scrolling position for entire tool window
        /// </summary>
        private Vector2 ScrollPosition;

        /// <summary>
        /// Main GUI function
        /// </summary>
        void OnGUI()
		{

            // Disable tool during runtime
			if(Application.isPlaying)
			{
                GUILayout.Label("Tool cannot be used during play mode.", EditorStyles.label);
				return;
			}
            
			this.ScrollPosition = EditorGUILayout.BeginScrollView(this.ScrollPosition);

            this.OnIntroductionGUI();
            this.OnLibraryGUI();
            
			EditorGUILayout.EndScrollView();
		}

        /// <summary>
        /// Introduction GUI, includeing basic info and btns
        /// </summary>
        private void OnIntroductionGUI()
        {
            EditorGUILayout.Space(2f);

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Info", EditorStyles.boldLabel,  GUILayout.Width(60));

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            this.OnCreateOrDeleteLibraryGUI();
            this.OnGuideBtnGUI();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical(EditorStyles.textArea);
            EditorGUILayout.LabelField($"Version: {ScriptableFunctionsLibraryManager.VERSION}");
            EditorGUILayout.LabelField($"Currently Register Functions: {(this.LibrarySO == null ? "N/A" : (this.LibrarySO.EditorGetPreset() == null ? "N/A" : this.LibrarySO.EditorGetPreset().Count))}");
            EditorGUILayout.LabelField($"Last Register Time: {(this.LibrarySO == null ? "N/A" : EditorPrefs.GetString(ScriptableFunctionsLibraryManager.LAST_REGISTER_TIME_KEY))}");
            EditorGUILayout.LabelField($"Created by: ROBsayYes");
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space(2f);
        }

        /// <summary>
        /// Btns to create or delete library based on library existed or not
        /// </summary>
        private void OnCreateOrDeleteLibraryGUI()
        {

            // Check existence of library
            bool isCreateOrDelete = false;
            string assets =  ScriptableFunctionsLibraryManager.ASSETS_PATH;
            string resources = ScriptableFunctionsLibraryManager.RESOURCES_PATH;
            string folder = ScriptableFunctionsLibraryManager.FOLDER_PATH;
            string name = ScriptableFunctionsLibraryManager.OBJECT_NAME;
            string path = $"{assets}/{resources}/{folder}/{name}.asset";
            if(this.LibrarySO == null)
            {
                if(path != "" && File.Exists(path) == true)
                {
                    this.LibrarySO = TryGetScriptableFunctionsLibrarySO();
                }
                else
                {
                    isCreateOrDelete = true;
                }
            }
            
            if(isCreateOrDelete)
            {
                
                // Create library button
                if(GUILayout.Button("Create Library",  GUILayout.Width(120)))
                {
                    this.CreateLibrary();
                }
            }
            else
            {
                
                // Delete library button
                if(GUILayout.Button("Delete Library",  GUILayout.Width(120)))
                {
                    ScriptableFunctionsLibraryDeleteEditor.ShowWindow();
                }
            }
        }

        /// <summary>
        /// Guide btn to open github page
        /// </summary>
        private void OnGuideBtnGUI()
        {
            if (GUILayout.Button("Guide",  GUILayout.Width(80)))
            {
                Application.OpenURL("https://github.com/12OI3/Scriptable-Functions-Library");
            }
        }

        /// <summary>
        /// Library list, read all items in scriptable object and list them out using drawer
        /// </summary>
        private void OnLibraryGUI()
        {
            
            if(this.LibrarySO == null)
                return;

            // Count functions
            EditorGUILayout.Space(2f);
            EditorGUILayout.LabelField("Registered Functions", EditorStyles.boldLabel);

            // Access all items
            SerializedObject obj = new SerializedObject(this.LibrarySO);
            obj.Update();
            SerializedProperty presets = obj.FindProperty("ScriptableFunctionPresets");

            // Draw all items
            for (int i = 0; i < presets.arraySize; i++)
            {
                SerializedProperty element = presets.GetArrayElementAtIndex(i);
                ScriptableFunctionPresetDrawer.Draw(element);
            }

            // Modify changes
            if (obj.ApplyModifiedProperties())
            {
                EditorUtility.SetDirty(this.LibrarySO);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            EditorGUILayout.Space(2f);
        }

        #endregion

        /// <summary>
        /// Static function to get library object
        /// </summary>
        /// <returns></returns>
        private static ScriptableFunctionsLibrarySO TryGetScriptableFunctionsLibrarySO()
        {
            ScriptableFunctionsLibrarySO so = null;
            string assets = ScriptableFunctionsLibraryManager.ASSETS_PATH;
            string resources = ScriptableFunctionsLibraryManager.RESOURCES_PATH;
            string folder = ScriptableFunctionsLibraryManager.FOLDER_PATH;
            string name = ScriptableFunctionsLibraryManager.OBJECT_NAME;
            string path = $"{assets}/{resources}/{folder}/{name}.asset";
            if(path != "" && File.Exists(path) == true)
            {
                so = AssetDatabase.LoadAssetAtPath<ScriptableFunctionsLibrarySO>(path);
            }
            return so;
        }

        /// <summary>
        /// Function to create library
        /// </summary>
        private void CreateLibrary()
        {
    
            string assets = ScriptableFunctionsLibraryManager.ASSETS_PATH;
            string resources = ScriptableFunctionsLibraryManager.RESOURCES_PATH;
            string folder = ScriptableFunctionsLibraryManager.FOLDER_PATH;
            string name = ScriptableFunctionsLibraryManager.OBJECT_NAME;
            string path = $"{assets}/{resources}/{folder}/{name}.asset";
            if(AssetDatabase.IsValidFolder($"{assets}/{resources}") == false)
            {
                AssetDatabase.CreateFolder(assets, resources);
                AssetDatabase.Refresh();
            }
            
            if(AssetDatabase.IsValidFolder($"{assets}/{resources}/{folder}") == false)
            {
                AssetDatabase.CreateFolder($"{assets}/{resources}", folder);
                AssetDatabase.Refresh();
            }
            this.LibrarySO = CreateInstance<ScriptableFunctionsLibrarySO>();
            AssetDatabase.CreateAsset(this.LibrarySO, path);
            AssetDatabase.SaveAssetIfDirty(this.LibrarySO);
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = this.LibrarySO;
            AssetDatabase.Refresh();
            UpdateLibrary();
        }
        
        /// <summary>
        /// Function to update libarary
        /// </summary>
        private static void UpdateLibrary()
        {
            
            ScriptableFunctionsLibrarySO so = TryGetScriptableFunctionsLibrarySO();
            if(so == null)
                return;

            // Keep existing items' editor setting
            Dictionary<string, ScriptableFunctionPreset> previous = new();
            if(so.EditorGetPreset() != null)
            {
                foreach(ScriptableFunctionPreset preset in so.EditorGetPreset())
                {
                    previous.Add(preset.ID, preset);
                }
            }

            // Get all class inherit from ScriptableFunction
            so.EditorResetPreset();
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(ScriptableFunction)) && !type.IsAbstract);

            // Write down all esential datas and stored into list
            int count = 0;
            foreach (var type in types)
            {
                count++;
                if (previous.ContainsKey(type.Name))
                {
                    ScriptableFunctionPreset exist = previous[type.Name];
                    exist.ToolTip = type.GetCustomAttribute<TooltipAttribute>()?.tooltip;
                    exist.ToolTip = exist.ToolTip == null ? "" : exist.ToolTip;
                    so.EditorSetPreset(exist);
                }
                else
                {
                    so.EditorSetPreset(type);
                }
            }
            
            // Save changes
            EditorUtility.SetDirty(so);
            AssetDatabase.SaveAssetIfDirty(so);
            AssetDatabase.Refresh();
            Debug.Log($"Scriptable Function Library: Library update, register {count} functions");
            EditorPrefs.SetString(ScriptableFunctionsLibraryManager.LAST_REGISTER_TIME_KEY, DateTime.Now.ToString());
        }

        /// <summary>
        /// Function to delete library
        /// </summary>
        public static void DeleteLibrary()
        {
            
            ScriptableFunctionsLibrarySO so = TryGetScriptableFunctionsLibrarySO();
            if(so == null)
                return;

            string assets = ScriptableFunctionsLibraryManager.ASSETS_PATH;
            string resources = ScriptableFunctionsLibraryManager.RESOURCES_PATH;
            string folder = ScriptableFunctionsLibraryManager.FOLDER_PATH;
            string name = ScriptableFunctionsLibraryManager.OBJECT_NAME;
            string path = $"{assets}/{resources}/{folder}/{name}.asset";
            if (AssetDatabase.AssetPathExists(path))
            {
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.Refresh();
            }
        }
    }
}