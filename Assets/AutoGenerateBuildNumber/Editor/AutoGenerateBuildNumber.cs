
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.Callbacks;
using UnityEngine;
using System;
using System.IO;

#if BUILD_NUMBER_GENERATE_FROM_DATE
class AutoGenerateBuildNumberPreProcessor : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }

    public static string PrevBuildNrString;
    
    public void OnPreprocessBuild(BuildReport report)
    {
        //Highest allowed version code: 21 4748 3647
        // Our format yy ddd HHmm:      00  001 0000
        
        // we always have to pad day of year, so it becomes 3 digits
        PrevBuildNrString = PlayerSettings.Android.bundleVersionCode.ToString();
        
        string buildNrString=DateTime.Now.ToString("yy")+DateTime.Now.DayOfYear.ToString("D3")+DateTime.Now.ToString("HHmm");

        Debug.Log("AutoGenerateBuildNumber from Date: "+buildNrString);
        
        if (BuildInfoObject.Instance==null) CreateObject();
        
        PlayerSettings.Android.bundleVersionCode=int.Parse(buildNrString);
        PlayerSettings.iOS.buildNumber = buildNrString;
        BuildInfoObject.Instance.buildNumber = int.Parse(PrevBuildNrString);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

    } 
    
    [ContextMenu("Create Build Info Object")]
    void CreateObject()
    {
        var infoObject = ScriptableObject.CreateInstance<BuildInfoObject>();

        AssetDatabase.CreateAsset(infoObject,"Assets"+Path.DirectorySeparatorChar+"Resources"+Path.DirectorySeparatorChar+"BuildInfoObject.asset");
        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}

class AutoGenerateBuildNumberFromDatePostProcessor
{
    public int callbackOrder { get { return 0; } }
    
    [PostProcessBuildAttribute(1)]
    public static void OnPostprocessBuild(BuildTarget target, string path)
    {
        Debug.Log("AutoGenerateBuildNumber restoring to: "+AutoGenerateBuildNumberPreProcessor.PrevBuildNrString);
        
        PlayerSettings.Android.bundleVersionCode=int.Parse(AutoGenerateBuildNumberPreProcessor.PrevBuildNrString);
        PlayerSettings.iOS.buildNumber = AutoGenerateBuildNumberPreProcessor.PrevBuildNrString;
    }
}
#endif




