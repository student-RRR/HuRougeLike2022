using UnityEngine;
using System.IO;
using System.Collections;
public class LoadFromFileExample : MonoBehaviour
{

    IEnumerator Start()
    {
        string path = "AssetBundles/test.abc";
        //�Ĥ@�إ[��AB���覡 LoadFromMemoryAsync
        //���B�[��
        AssetBundleCreateRequest request = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(path));
        yield return request;
        AssetBundle ab = request.assetBundle;
        //�P�B�覡
        //AssetBundle ab=  AssetBundle.LoadFromMemory(File.ReadAllBytes(path));

        //�ϥ��ح����귽
        Object[] obj = ab.LoadAllAssets<GameObject>();//�[���X�ө�J�Ʋդ�
        // �ЫإX��
        foreach (Object o in obj)
        {
            Instantiate(o);
        }
    }
}