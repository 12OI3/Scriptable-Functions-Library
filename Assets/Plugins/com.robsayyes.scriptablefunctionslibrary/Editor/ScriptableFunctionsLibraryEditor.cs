
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ScriptableFunctionsLibrary
{
    public class ScriptableFunctionsLibraryEditor : EditorWindow
    {
        private ScriptableFunctionsLibrarySO LibrarySO;
        
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
        
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void DidReloadScripts()
        {
            UpdateLibrary();
        }

        #region OnGUI Functions

        private Vector2 ScrollPosition;

        void OnGUI()
		{
			if(Application.isPlaying)
			{
                GUILayout.Label("Tool cannot be used during play mode.", EditorStyles.label);
				return;
			}
            
			this.ScrollPosition = EditorGUILayout.BeginScrollView(this.ScrollPosition);

            this.OnCreateLibraryGUI();
            // this.OnUpdateLibraryGUI();
            this.OnIntroductionGUI();
            this.OnLibraryGUI();
            
			EditorGUILayout.EndScrollView();
		}

        private void OnCreateLibraryGUI()
        {
            
            string assets = ScriptableFunctionsLibraryManager.ASSETS_PATH;
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
                    if(GUILayout.Button("Create Library"))
                    {

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
                }
            }
        }
        
        private void OnUpdateLibraryGUI()
        {
            
            if(this.LibrarySO == null)
                return;

            if(GUILayout.Button("Update Library"))
                UpdateLibrary();
        }

        private void OnIntroductionGUI()
        {
            EditorGUILayout.Space(2f);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Info", EditorStyles.boldLabel);
            this.OnGuideBtnGUI();
            this.OnSettingsBtnGUI();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.textArea);
            EditorGUILayout.LabelField($"Version: {ScriptableFunctionsLibraryManager.VERSION}");
            EditorGUILayout.LabelField($"Currently Register Functions: {(this.LibrarySO == null ? "N/A" : (this.LibrarySO.EditorGetPreset() == null ? "N/A" : this.LibrarySO.EditorGetPreset().Count))}");
            EditorGUILayout.LabelField($"Last Register Time: {(this.LibrarySO == null ? "N/A" : EditorPrefs.GetString(ScriptableFunctionsLibraryManager.LAST_REGISTER_TIME_KEY))}");
            EditorGUILayout.LabelField($"Created by: ROBsayYes");
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(2f);
        }

        private void OnGuideBtnGUI()
        {
            if (GUILayout.Button("Guide"))
            {
                ScriptableFunctionsLibraryGuideEditor.ShowWindow();
            }
        }

        private void OnSettingsBtnGUI()
        {
            if (GUILayout.Button("Settings"))
            {
                ScriptableFunctionsLibrarySettingsEditor.ShowWindow();
            }
        }

        private void OnLibraryGUI()
        {
            
            if(this.LibrarySO == null)
                return;

            EditorGUILayout.Space(2f);
            EditorGUILayout.LabelField("Registered Functions", EditorStyles.boldLabel);

            SerializedObject obj = new SerializedObject(this.LibrarySO);
            obj.Update();

            SerializedProperty presets = obj.FindProperty("ScriptableFunctionPresets");

            for (int i = 0; i < presets.arraySize; i++)
            {
                SerializedProperty element = presets.GetArrayElementAtIndex(i);
                ScriptableFunctionPresetDrawer.Draw(element);
            }

            if (obj.ApplyModifiedProperties())
            {
                EditorUtility.SetDirty(this.LibrarySO);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
            }
            
            EditorGUILayout.Space(2f);
        }

        #endregion

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
        
        private static void UpdateLibrary()
        {
            
            ScriptableFunctionsLibrarySO so = TryGetScriptableFunctionsLibrarySO();
            if(so == null)
                return;

            Dictionary<string, ScriptableFunctionPreset> previous = new();
            if(so.EditorGetPreset() != null)
            {
                foreach(ScriptableFunctionPreset preset in so.EditorGetPreset())
                    previous.Add(preset.ID, preset);
            }

            so.EditorResetPreset();
        
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(ScriptableFunction)) && !type.IsAbstract);

            int count = 0;
            foreach (var type in types)
            {
                count++;
                if (previous.ContainsKey(type.Name))
                    so.EditorSetPreset(previous[type.Name]);
                else
                {
                    so.EditorSetPreset(type);
                }
            }
            
            EditorUtility.SetDirty(so);
            AssetDatabase.SaveAssetIfDirty(so);
            AssetDatabase.Refresh();
            
            Debug.Log($"Scriptable Function Library: Library update, register {count} functions");
            EditorPrefs.SetString(ScriptableFunctionsLibraryManager.LAST_REGISTER_TIME_KEY, DateTime.Now.ToString());
        }

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