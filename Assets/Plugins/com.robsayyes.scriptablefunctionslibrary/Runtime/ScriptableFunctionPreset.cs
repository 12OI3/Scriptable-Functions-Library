using System;

namespace ScriptableFunctionsLibrary
{
    
    /// <summary>
    /// Preset data structure
    /// </summary>
    [Serializable]
    public struct ScriptableFunctionPreset
    {
        /// <summary>
        /// Mainly class name
        /// </summary>
        public string ID;

        /// <summary>
        /// Class's assembly name, essential to create in runtime
        /// </summary>
        public string Assembly;

        /// <summary>
        /// Class's status in runtime
        /// </summary>
        public bool Enable;
        
        /// <summary>
        /// Class's tooltip
        /// </summary>
        public string ToolTip;
    }
}