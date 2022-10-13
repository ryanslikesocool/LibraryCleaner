// Made with love by Ryan Boyer http://ryanjboyer.com <3

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace LibraryCleaner {
    public class LibraryCleanerWindow : EditorWindow {
        private LibraryCleanerSettings settings = null;
        private SerializedObject scriptableObject = null;

        private SerializedProperty cleanConditionProp = null;
        private SerializedProperty cleanThresholdProp = null;
        private SerializedProperty cleanThresholdUnitProp = null;

        private SerializedProperty deleteEverythingProp = null;
        private SerializedProperty deletePackageCacheProp = null;
        private SerializedProperty deleteScriptAssembliesProp = null;
        private SerializedProperty deleteShaderCacheProp = null;
        private SerializedProperty deleteAssetDatabaseProp = null;

        private SerializedProperty deleteAdditionalFoldersProp = null;
        private SerializedProperty deleteAdditionalFilesProp = null;

        [MenuItem("Tools/Developed With Love/LibraryCleaner Manager")]
        private static void Init() {
            LibraryCleanerWindow window = (LibraryCleanerWindow)EditorWindow.GetWindow(typeof(LibraryCleanerWindow));
            window.Show();
            window.titleContent = new GUIContent("Library Cleaner");
        }

        private void OnGUI() {
            if (settings == null) {
                settings = (LibraryCleanerSettings)EditorGUILayout.ObjectField(settings, typeof(LibraryCleanerSettings), false);
            } else {
                ValidateProperties();

                EditorGUILayout.HelpBox("The contents of the Library folder will only be checked and potentially deleted when quitting the editor.", MessageType.None, true);

                scriptableObject.Update();

                EditorGUILayout.PropertyField(cleanConditionProp, true);
                if (settings.cleanCondition == CleanCondition.Threshold) {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PropertyField(cleanThresholdProp, true);
                    EditorGUILayout.PropertyField(cleanThresholdUnitProp, GUIContent.none);
                    EditorGUILayout.EndHorizontal();
                }

                GUILayout.Space(16);

                if (settings.deleteEverything) {
                    EditorGUILayout.HelpBox("WARNING: Deleting everything will entirely empty out the Library folder.  This includes the current layout.\nThis process will take a while.", MessageType.Error, true);
                }
                EditorGUILayout.PropertyField(deleteEverythingProp, true);

                GUILayout.Space(4);

                EditorGUI.BeginDisabledGroup(settings.deleteEverything);

                if (settings.deletePackageCache) {
                    EditorGUILayout.HelpBox("An internet connection will be required to redownload packages the next time this project is opened.\nThis process may take a while.", MessageType.Warning, true);
                }
                EditorGUILayout.PropertyField(deletePackageCacheProp, true);

                if (settings.deleteScriptAssemblies) {
                    EditorGUILayout.HelpBox("C# code will need to recompile the next time this project is opened.\nThis process may take a while.", MessageType.Warning, true);
                }
                EditorGUILayout.PropertyField(deleteScriptAssembliesProp, true);

                if (settings.deleteShaderCache) {
                    EditorGUILayout.HelpBox("Shaders will need to recompile the next time this project is opened.", MessageType.Info, true);
                }
                EditorGUILayout.PropertyField(deleteShaderCacheProp, true);

                if (settings.deleteAssetDatabase) {
                    EditorGUILayout.HelpBox("Assets will need to be reimported the next time this project is opened.\nThis process may take a while.", MessageType.Warning, true);
                }
                EditorGUILayout.PropertyField(deleteAssetDatabaseProp, true);

                if (settings.deleteAdditionalFolders.Length > 0) {
                    EditorGUILayout.HelpBox("Add folder names relative to the Library folder.", MessageType.None, true);
                }
                EditorGUILayout.PropertyField(deleteAdditionalFoldersProp, true);

                if (settings.deleteAdditionalFiles.Length > 0) {
                    EditorGUILayout.HelpBox("Add file names relative to the Library folder.", MessageType.None, true);
                }
                EditorGUILayout.PropertyField(deleteAdditionalFilesProp, true);

                EditorGUI.EndDisabledGroup();

                scriptableObject.ApplyModifiedProperties();
            }
        }

        private void ValidateProperties() {
            if (scriptableObject == null) {
                scriptableObject = new SerializedObject(settings);
            }
            if (cleanConditionProp == null) {
                cleanConditionProp = scriptableObject.FindProperty("cleanCondition");
            }
            if (cleanThresholdProp == null) {
                cleanThresholdProp = scriptableObject.FindProperty("cleanThreshold");
            }
            if (cleanThresholdUnitProp == null) {
                cleanThresholdUnitProp = scriptableObject.FindProperty("cleanThresholdUnit");
            }
            if (deleteEverythingProp == null) {
                deleteEverythingProp = scriptableObject.FindProperty("deleteEverything");
            }
            if (deletePackageCacheProp == null) {
                deletePackageCacheProp = scriptableObject.FindProperty("deletePackageCache");
            }
            if (deleteScriptAssembliesProp == null) {
                deleteScriptAssembliesProp = scriptableObject.FindProperty("deleteScriptAssemblies");
            }
            if (deleteShaderCacheProp == null) {
                deleteShaderCacheProp = scriptableObject.FindProperty("deleteShaderCache");
            }
            if (deleteAssetDatabaseProp == null) {
                deleteAssetDatabaseProp = scriptableObject.FindProperty("deleteAssetDatabase");
            }
            if (deleteAdditionalFoldersProp == null) {
                deleteAdditionalFoldersProp = scriptableObject.FindProperty("deleteAdditionalFolders");
            }
            if (deleteAdditionalFilesProp == null) {
                deleteAdditionalFilesProp = scriptableObject.FindProperty("deleteAdditionalFiles");
            }
        }
    }
}
#endif