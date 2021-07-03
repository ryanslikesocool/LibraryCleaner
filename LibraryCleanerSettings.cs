// Made with <3 by Ryan Boyer http://ryanjboyer.com

#if UNITY_EDITOR
using UnityEngine;

namespace LibraryCleaner
{
    public class LibraryCleanerSettings : ScriptableObject
    {
        public CleanCondition cleanCondition = CleanCondition.Threshold;

        public float cleanThreshold = 1.5f;
        public StorageUnit cleanThresholdUnit = StorageUnit.Gigabyte;

        [Space] public bool deleteEverything = false;
        public bool deletePackageCache = false;
        public bool deleteScriptAssemblies = false;
        public bool deleteShaderCache = false;
        public bool deleteAssetDatabase = false;
    }
}
#endif