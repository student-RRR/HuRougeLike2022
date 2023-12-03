using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �S��
public enum ENUM_Effect
{
    attack
}

public class IEffect
{
    IAssetFactory assetFactory = HuRougeLike2022Factory.GetAssetFactory();
    GameObject effectObject;

    // �غc��
    public IEffect(ENUM_Effect _effect)
    {
        ENUM_EffectModel enum_effectModel = new ENUM_EffectModel();
        switch (_effect)
        {
            case ENUM_Effect.attack:
                {
                    enum_effectModel = ENUM_EffectModel.ModelAttack;
                    break;
                }
            default:
                break;
        }

        // �]�w�ҫ�
        GameObject CreatureModel = assetFactory.LoadEffectModel(enum_effectModel);

        // ���J�ҫ�
        SetGameObject(UnityEngine.Object.Instantiate(CreatureModel) as GameObject);

    }


    // �]�wUnity�ҫ�
    public void SetGameObject(GameObject theGameObject)
    {
        effectObject = theGameObject;

        GameObject.Destroy(effectObject, 0.1f);
    }

    public GameObject GetGameObject()
    {
        return effectObject;
    }

}
