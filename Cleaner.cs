// Made with <3 by Ryan Boyer http://ryanjboyer.com

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

namespace LibraryCleaner
{
    [InitializeOnLoad]
    public class Cleaner
    {
        private const string PACKAGE_SETTINGS_PATH = "Packages/com.ifelse.librarycleaner/CleanerSettings.asset";
        private const string LOCAL_SETTINGS_PATH = "Assets/Plugins/LibraryCleaner/CleanerSettings.asset";

        private static readonly string[] PackageCacheDirectories = new string[] {
            "PackageCache",
            "PackageManager"
        };

        private static readonly string[] ScriptAssemblyDirectories = new string[] {
            "ScriptAssemblies",
            "ScriptMapper"
        };

        private static readonly string[] ShaderCacheDirectories = new string[] {
            "ShaderCache"
        };
        private static readonly string[] ShaderCacheFiles = new string[] {
            "ShaderCache.db"
        };

        private static readonly string[] AssetDatabaseDirectories = new string[] {
            "Artifacts"
        };
        private static readonly string[] AssetDatabaseFiles = new string[] {
            "ArtifactDB",
            "ArtifactDB-lock",
            "SourceAssetDB",
            "SourceAssetDB-lock"
        };

        static Cleaner() => EditorApplication.quitting += Clean;

        public static void Clean()
        {
            string path = Application.dataPath.Replace("Assets", "Library");

            LibraryCleanerSettings settings = null;
            if (File.Exists(LOCAL_SETTINGS_PATH))
            {
                settings = (LibraryCleanerSettings)AssetDatabase.LoadAssetAtPath(LOCAL_SETTINGS_PATH, typeof(LibraryCleanerSettings));
            }
            else if (File.Exists(PACKAGE_SETTINGS_PATH))
            {
                settings = (LibraryCleanerSettings)AssetDatabase.LoadAssetAtPath(PACKAGE_SETTINGS_PATH, typeof(LibraryCleanerSettings));
            }
            if (settings == null) { return; }

            if (settings.cleanCondition == CleanCondition.Always || ConvertUnit(DirectorySize(path), settings.cleanThresholdUnit) > settings.cleanThreshold)
            {
                Clean(path, settings);
            }
        }

        private static void Clean(string path, LibraryCleanerSettings settings)
        {
            if (settings.deleteEverything)
            {
                DirectoryInfo d = new DirectoryInfo(path);
                CleanDirectory(d);
            }
            else
            {
                if (settings.deletePackageCache)
                {
                    CleanDirectories(path, PackageCacheDirectories);
                }
                if (settings.deleteScriptAssemblies)
                {
                    CleanDirectories(path, ScriptAssemblyDirectories);
                }
                if (settings.deleteShaderCache)
                {
                    CleanDirectories(path, ShaderCacheDirectories);
                    CleanFiles(path, ShaderCacheFiles);
                }
                if (settings.deleteAssetDatabase)
                {
                    CleanDirectories(path, AssetDatabaseDirectories);
                    CleanFiles(path, AssetDatabaseFiles);
                }
                if (settings.deleteAdditionalFolders.Length > 0)
                {
                    CleanDirectories(path, settings.deleteAdditionalFolders);
                }
                if (settings.deleteAdditionalFiles.Length > 0)
                {
                    CleanDirectories(path, settings.deleteAdditionalFiles);
                }
            }
        }

        private static void CleanDirectory(DirectoryInfo di)
        {
            foreach (FileInfo file in di.EnumerateFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                dir.Delete(true);
            }
        }

        private static void CleanDirectories(string path, string[] folders)
        {
            foreach (string folder in folders)
            {
                string p = Path.Combine(path, folder);
                DirectoryInfo d = new DirectoryInfo(p);
                CleanDirectory(d);
            }
        }

        private static void CleanFiles(string path, string[] files)
        {
            foreach (string file in files)
            {
                string p = Path.Combine(path, file);
                FileInfo f = new FileInfo(p);
                f.Delete();
            }
        }

        private static long DirectorySize(string path)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            return DirectorySize(d);
        }

        private static long DirectorySize(DirectoryInfo d)
        {
            long size = 0;

            FileInfo[] fileInfos = d.GetFiles();
            foreach (FileInfo info in fileInfos)
            {
                size += info.Length;
            }

            DirectoryInfo[] directoryInfos = d.GetDirectories();
            foreach (DirectoryInfo directoryInfo in directoryInfos)
            {
                size += DirectorySize(directoryInfo);
            }
            return size;
        }

        private static float ConvertUnit(long bytes, StorageUnit unit)
        {
            switch (unit)
            {
                case StorageUnit.Megabyte: return bytes / 1000000f;
                default: return bytes / 1000000000f;
            }
        }
    }
}
#endif