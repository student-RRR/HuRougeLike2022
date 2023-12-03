using UnityEditor;
using System.IO;

public class CreateAssetbundles
{

    [MenuItem("AssetsBundle/Build AssetBundles")]
    static void BuildAllAssetBundles()//進行打包
    {
        string dir = "AssetBundles";
        //判斷該目錄是否存在
        if (Directory.Exists(dir) == false)
        {
            Directory.CreateDirectory(dir);//在工程下創建AssetBundles目錄
        }
        //參數一爲打包到哪個路徑，參數二壓縮選項  參數三 平臺的目標
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }
}