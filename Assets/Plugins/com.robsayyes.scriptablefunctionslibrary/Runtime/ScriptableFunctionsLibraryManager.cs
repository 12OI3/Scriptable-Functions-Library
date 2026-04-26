using System.Collections.Generic;
using UnityEngine;

namespace ScriptableFunctionsLibrary
{
    public abstract class ScriptableFunctionsLibraryManager
    {
        
        public const string VERSION = "0.1.5";
        public const string LAST_REGISTER_TIME_KEY = "ScriptableFunctionsLibrary: Last Register Time";
        public const string ASSETS_PATH = "Assets";
        public const string RESOURCES_PATH = "Resources";
        public const string FOLDER_PATH = "ScriptableFunctionsLibrary";
        public const string OBJECT_NAME = "ScriptableFunctionsLibrary";

        private static ScriptableFunctionsLibrarySO LibrarySO;

        /// <summary>
        /// The scriptable functions library
        /// </summary>
        public static Dictionary<string, ScriptableFunction> Library;

        /// <summary>
        /// Init function, will automatically execute on load
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        { 
            LibrarySO = Resources.Load<ScriptableFunctionsLibrarySO>($"{FOLDER_PATH}/{OBJECT_NAME}");
            if(LibrarySO == null)
            {
                Debug.LogWarning("Scriptable Function Library: Library not exist");
                return;
            }
            
            ResetLibrary();
        }

        /// <summary>
        /// Reset all instances in library
        /// </summary>    
        public static void ResetLibrary()
        {
            Library = LibrarySO.Init();
        }
    }
}