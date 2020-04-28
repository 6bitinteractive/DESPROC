using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Build.Reporting;
using System.IO;


[CreateAssetMenu(fileName = "BuildVersion", menuName = "Database/Create Build Version")]
public class BuildVersion : ScriptableObject
{
    public BuildVersion(BuildVersion otherVersionData)
    {
        major = otherVersionData.major;
        minor = otherVersionData.minor;
        patch = otherVersionData.patch;
    }

    public int major;
    public int minor;
    public int patch;
    public bool isInitialVersion;

    public void SetVersion(BuildVersion otherVersionData)
    {
        major = otherVersionData.major;
        minor = otherVersionData.minor;
        patch = otherVersionData.patch;
    }

    public string GetVersionString()
    {
        return major.ToString() + "." + minor.ToString() + "." +patch.ToString();
    }
}

public class BuildProject 
{
    private static BuildVersion currBuildVersion;
    private static BuildVersion oldBuildVersion;
    private static string       currVersionString;
    private static string       appName;

    [MenuItem("Build/Android")]
    public static void BuildAndroid()
    {
        GetVersion();
        
        if (currBuildVersion == null)
        {
            Debug.LogError("Cannot find BuildVersion.asset in Resources");
            return;
        }

        if(!currBuildVersion.isInitialVersion)
            currBuildVersion.patch++;

        SaveVersion();

        Build();
    }

    [MenuItem("Build/Android Minor")]
    public static void BuildAndroidMinor()
    {
        GetVersion();

        if (currBuildVersion == null)
        {
            Debug.LogError("Cannot find BuildVersion.asset in Resources");
            return;
        }

        if (!currBuildVersion.isInitialVersion)
        {
            currBuildVersion.minor++;
            currBuildVersion.patch = 0;
        }

        SaveVersion();

        Build();
    }

    [MenuItem("Build/Android Major")]
    public static void BuildAndroidMajor()
    {
        GetVersion();

        if (currBuildVersion == null)
        {
            Debug.LogError("Cannot find BuildVersion.asset in Resources");
            return;
        }

        if (!currBuildVersion.isInitialVersion)
        {
            currBuildVersion.major++;
            currBuildVersion.minor = 0;
            currBuildVersion.patch = 0;
        }

        SaveVersion();

        Build();
    }

    private static void GetVersion()
    {
        currBuildVersion = AssetDatabase.LoadAssetAtPath<BuildVersion>
           ("Assets/Resources/BuildVersion.asset");

        oldBuildVersion = new BuildVersion(currBuildVersion);
    }

    private static void SaveVersion()
    {
        EditorUtility.SetDirty(currBuildVersion);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        currVersionString = currBuildVersion.GetVersionString();
    }

    private static void Build()
    {
        //Set version and apply passwords. Unity will reset the field blank everytime its closed so this will make things easier
        PlayerSettings.bundleVersion = currVersionString;
        PlayerSettings.keyaliasPass = "senshi";
        PlayerSettings.keystorePass = "senshi";

        appName = PlayerSettings.productName;
        
        string buildPath = Path.Combine("Build", "Android");
        string buildFullPath = Path.Combine(buildPath, appName + ".apk");

        //Create the build directory before building to prevent errors
        Directory.CreateDirectory(buildPath);


        //Dont need anything fancy
        BuildOptions bo = BuildOptions.None;

        //Actual build command
        var buildResult = BuildPipeline.BuildPlayer(
             EditorBuildSettings.scenes
            , buildFullPath
            , BuildTarget.Android
            , bo);

        if(buildResult.summary.result == BuildResult.Failed)
        {
            Debug.LogErrorFormat("Build failed: {0} errors", buildResult.summary.totalErrors);

            //Rollback the version data if the build failed
            currBuildVersion.SetVersion(oldBuildVersion);
            SaveVersion();
        }
        else if(buildResult.summary.result == BuildResult.Succeeded)
        {
            Debug.LogFormat("Build success");
        }
    }
}
