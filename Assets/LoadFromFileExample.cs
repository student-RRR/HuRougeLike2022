using UnityEngine;
using System.IO;
using System.Collections;
public class LoadFromFileExample : MonoBehaviour
{

    IEnumerator Start()
    {
        string path = "AssetBundles/test.abc";
        //第一種加載AB的方式 LoadFromMemoryAsync
        //異步加載
        AssetBundleCreateRequest request = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(path));
        yield return request;
        AssetBundle ab = request.assetBundle;
        //同步方式
        //AssetBundle ab=  AssetBundle.LoadFromMemory(File.ReadAllBytes(path));

        //使用裏面的資源
        Object[] obj = ab.LoadAllAssets<GameObject>();//加載出來放入數組中
        // 創建出來
        foreach (Object o in obj)
        {
            Instantiate(o);
        }
    }
}