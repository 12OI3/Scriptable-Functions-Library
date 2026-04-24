using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ScriptableFunctionsLibrary
{
    public class ScriptableFunctionsLibrarySO : ScriptableObject
    {
        public List<ScriptableFunctionPreset> ScriptableFunctionPresets;

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
        
        public void EditorResetPreset() => this.ScriptableFunctionPresets = new();
        public List<ScriptableFunctionPreset> EditorGetPreset() => this.ScriptableFunctionPresets;

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

        public void EditorSetPreset(ScriptableFunctionPreset _preset)
        {

            this.ScriptableFunctionPresets.Add(_preset);
        }
    }
}