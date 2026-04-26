using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ScriptableFunctionsLibrary
{
    public class ScriptableFunctionsLibrarySO : ScriptableObject
    {

        /// <summary>
        /// Presets list
        /// </summary>
        public List<ScriptableFunctionPreset> ScriptableFunctionPresets;

        /// <summary>
        /// Init function, basically will recreate all instance based on preset and return a dictionary based on class name
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, ScriptableFunction> Init()
        {
            
            Dictionary<string, ScriptableFunction> dict = new();
            if(this.ScriptableFunctionPresets.Count == 0)
                return dict;
            for(int i = 0; i < this.ScriptableFunctionPresets.Count; i++)
            {
                if(!this.ScriptableFunctionPresets[i].Enable)
                    continue;
                    
                dict.Add(this.ScriptableFunctionPresets[i].ID, Activator.CreateInstance(Type.GetType(this.ScriptableFunctionPresets[i].Assembly)) as ScriptableFunction);
            }

            Debug.Log($"Scriptable Function Library: Library parsed for runtime, load {dict.Count} functions");
            return dict;
        }
        
        /// <summary>
        /// Reset presets list
        /// </summary>
        public void EditorResetPreset() => this.ScriptableFunctionPresets = new();

        /// <summary>
        /// Get presets list
        /// </summary>
        /// <returns></returns>
        public List<ScriptableFunctionPreset> EditorGetPreset() => this.ScriptableFunctionPresets;

        /// <summary>
        /// Add preset to list (by type)
        /// </summary>
        /// <param name="_type"></param>
        public void EditorSetPreset(Type _type)
        {
            
            ScriptableFunctionPreset preset = new();
            preset.ID = _type.Name;
            preset.Enable = true;
            preset.Assembly = _type.AssemblyQualifiedName;
            preset.ToolTip = _type.GetCustomAttribute<TooltipAttribute>()?.tooltip;
            preset.ToolTip = preset.ToolTip == null ? "" : preset.ToolTip;
            this.ScriptableFunctionPresets.Add(preset);
        }

        /// <summary>
        /// Add preset to list (by preset struct)
        /// </summary>
        /// <param name="_preset"></param>
        public void EditorSetPreset(ScriptableFunctionPreset _preset)
        {

            this.ScriptableFunctionPresets.Add(_preset);
        }
    }
}