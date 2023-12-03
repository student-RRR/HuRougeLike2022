using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生物工廠
/// 待開發:分離出組裝部分到"生物建造者"
/// </summary>
public abstract class ICreatureFactory
{
    // 建立生物
    public abstract ICharacter CreateCreature(ENUM_Creature _Creature,
                                             ENUM_Body _Body,
                                             ENUM_Status _Status,
                                             ENUM_CreatureAI _CreatureAI,
                                             ENUM_Model _Model);

}

public class CreatureFactory : ICreatureFactory
{
    public override ICharacter CreateCreature(ENUM_Creature _Creature,
                                             ENUM_Body _Body, 
                                             ENUM_Status _Status,
                                             ENUM_CreatureAI _CreatureAI,
                                             ENUM_Model _Model)
    {
        ICharacter theCreature;

        // 組裝: 生物(根據傳入)
        switch (_Creature)
        {
            case ENUM_Creature.Elf:
                LogServise.Log("組裝生物:" + _Creature);
                theCreature = new CreatureElf();
                break;

            case ENUM_Creature.Ogre:
                LogServise.Log("組裝生物:" + _Creature);
                theCreature = new CreatureOgre();
                break;

            default:
                LogServise.Log("無法建立:" + _Creature + " (組裝生物失敗)");
                return null;
        }

        // 組裝: 能力值(根據傳入)
        switch (_Status)
        {
            case ENUM_Status.HumanChildStatus:
                LogServise.Log("組裝能力值:" + _Status);
                theCreature.SetState(new HumanChildStatus(theCreature));
                break;

            default:
                LogServise.Log("無法建立:" + _Status + " (組裝能力值失敗)");
                return null;
        }

        // 組裝: 身形(根據傳入)
        switch (_Body)
        {
            case ENUM_Body.Humanoid:
                LogServise.Log("組裝身形:Humanoid");
                break;

            default:
                LogServise.Log("無法建立:"+ _Body + " (組裝身形失敗)");
                return null;
        }

        // 組裝: AI(根據傳入)
        ICreatureAI theAI = null;
        switch (_CreatureAI)
        {
            case ENUM_CreatureAI.HumanAI:
                theAI = new HumanAI(theCreature);
                break;
            case ENUM_CreatureAI.WandererAI:
                theAI = new WandererAI(theCreature);
                break;
            case ENUM_CreatureAI.HunterAI:
                theAI = new HunterAI(theCreature);
                break;
            default:
                LogServise.Log("無法建立:" + _CreatureAI + " (加入AI失敗)");
                return null;
        }
        theCreature.SetAI(theAI);

        //設定模型
        GameObject CreatureModel = HuRougeLike2022Factory.GetAssetFactory().LoadCreatureModel(_Model);
        // 載入模型
        theCreature.SetGameObject(UnityEngine.Object.Instantiate(CreatureModel) as GameObject);
        //位置(測試用)
        theCreature.GetGameObject().transform.position = new Vector3(5f, 5f, 0f);


        // UI
        // HP Bar
        theCreature.SetHPBar(new HPBar());
        theCreature.GetHPBar().barObj.transform.parent = theCreature.GetGameObject().transform;

        theCreature.GetHPBar().barObj.transform.position = new Vector3(5f, 4.8f, 0f);

        // 存活與否
        theCreature.IsLive = true;

        return theCreature;
    }
}