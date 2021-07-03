// Made with <3 by Ryan Boyer http://ryanjboyer.com

#if UNITY_EDITOR
using UnityEngine;

namespace LibraryCleaner
{
    public class LibraryCleanerSettings : ScriptableObject
    {
        public CleanCondition cleanCondition = CleanCondition.Threshold;

        public float cleanThreshold = 2.5f;
        public StorageUnit cleanThresholdUnit = StorageUnit.Gigabyte;

        [Space] public bool deleteEverything = false;
        public bool deletePackageCache = false;
        public bool deleteScriptAssemblies = false;
        public bool deleteShaderCache = false;
        public bool deleteAssetDatabase = false;

        [Space]
        public string[] deleteAdditionalFolders = new string[] {
            "il2cpp_android_arm64-v8a",
            "Il2cppBuildCache",
            "BurstCache"
        };
        public string[] deleteAdditionalFiles = new string[0];
    }
}
#endif