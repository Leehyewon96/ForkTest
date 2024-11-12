using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

class BuildTest
{
    static string[] SCENES = FindEnabledEditorScenes();

    [MenuItem("Build/Test")]
    static void PerformBuild()
    {
        BuildPipeline.BuildPlayer(FindEnabledEditorScenes(), "Builds/MyGame.exe", BuildTarget.StandaloneWindows, BuildOptions.None);
    }
    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }
        return EditorScenes.ToArray();
    }


    [UnityEditor.MenuItem("Assets/Build All Asset Bundles")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDir = "Assets/AssetBundles";

        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(assetBundleDir);
        }
        BuildPipeline.BuildAssetBundles(assetBundleDir, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
    }

    public void BuildNeedAssetBundle(string bundleName)
    {
        if (!Directory.Exists(Application.dataPath + "/AssetBundles")) 
        {
            Directory.CreateDirectory(Application.dataPath + "/AssetBundles"); 
        }
        AssetBundleBuild[] buildBundles = new AssetBundleBuild[1];
        buildBundles[0].assetBundleName = bundleName; 
        buildBundles[0].assetNames = AssetDatabase.GetAssetPathsFromAssetBundle(bundleName);
        BuildPipeline.BuildAssetBundles(Application.dataPath + "/AssetBundles", buildBundles, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}