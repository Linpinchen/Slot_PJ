using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class MyTools : Editor
{
    /// <summary>
    /// MacOS編輯器使用
    /// </summary>
    [MenuItem("MyTools/CreatBundle/Build_StandaloneOSX")]
    static void CreatBundle_StandaloneOSX()
    {

        string pathname = "Bundle_StandaloneOSX";

        if (!Directory.Exists(pathname))
        {

            Directory.CreateDirectory(pathname);

        }

        BuildPipeline.BuildAssetBundles(pathname, BuildAssetBundleOptions.None, BuildTarget.StandaloneOSX);//Mac用這個

        Debuger.Log("CreateAssetBundle_Finish");


    }


    [MenuItem("MyTools/CreatBundle/Build_StandaloneWindows64")]
    static void CreatBundle_StandaloneWindows64()
    {

        string pathname = "Bundle_StandaloneWindows64";

        if (!Directory.Exists(pathname))
        {

            Directory.CreateDirectory(pathname);

        }

        BuildPipeline.BuildAssetBundles(pathname, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);//Windows用這個

        Debuger.Log("CreateAssetBundle_Finish");


    }




    [MenuItem("MyTools/CreatBundle/Build_WebGL")]
    static void CreatBundle_WebGL()
    {

        string pathname = "Bundle_WebGL";

        if (!Directory.Exists(pathname))
        {

            Directory.CreateDirectory(pathname);

        }

        BuildPipeline.BuildAssetBundles(pathname, BuildAssetBundleOptions.None, BuildTarget.WebGL);//Windows用這個

        Debuger.Log("CreateAssetBundle_Finish");


    }

    [MenuItem("MyTools/CreatBundle/Build_Android")]
    static void CreatBundle_Android()
    {

        string pathname = "Bundle_Android";

        if (!Directory.Exists(pathname))
        {

            Directory.CreateDirectory(pathname);

        }

        BuildPipeline.BuildAssetBundles(pathname, BuildAssetBundleOptions.None, BuildTarget.Android);//Windows用這個

        Debuger.Log("CreateAssetBundle_Finish");


    }


    [MenuItem("MyTools/CreatBundle/Build_Iphone")]
    static void CreatBundle_Iphone()
    {

        string pathname = "Bundle_Iphone";

        if (!Directory.Exists(pathname))
        {

            Directory.CreateDirectory(pathname);

        }

        BuildPipeline.BuildAssetBundles(pathname, BuildAssetBundleOptions.None, BuildTarget.iOS);//Windows用這個

        Debuger.Log("CreateAssetBundle_Finish");


    }


}

