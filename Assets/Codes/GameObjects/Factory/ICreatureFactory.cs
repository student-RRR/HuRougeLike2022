using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ͪ��u�t
/// �ݶ}�o:�����X�ո˳�����"�ͪ��سy��"
/// </summary>
public abstract class ICreatureFactory
{
    // �إߥͪ�
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

        // �ո�: �ͪ�(�ھڶǤJ)
        switch (_Creature)
        {
            case ENUM_Creature.Elf:
                LogServise.Log("�ո˥ͪ�:" + _Creature);
                theCreature = new CreatureElf();
                break;

            case ENUM_Creature.Ogre:
                LogServise.Log("�ո˥ͪ�:" + _Creature);
                theCreature = new CreatureOgre();
                break;

            default:
                LogServise.Log("�L�k�إ�:" + _Creature + " (�ո˥ͪ�����)");
                return null;
        }

        // �ո�: ��O��(�ھڶǤJ)
        switch (_Status)
        {
            case ENUM_Status.HumanChildStatus:
                LogServise.Log("�ո˯�O��:" + _Status);
                theCreature.SetState(new HumanChildStatus(theCreature));
                break;

            default:
                LogServise.Log("�L�k�إ�:" + _Status + " (�ո˯�O�ȥ���)");
                return null;
        }

        // �ո�: ����(�ھڶǤJ)
        switch (_Body)
        {
            case ENUM_Body.Humanoid:
                LogServise.Log("�ո˨���:Humanoid");
                break;

            default:
                LogServise.Log("�L�k�إ�:"+ _Body + " (�ո˨��Υ���)");
                return null;
        }

        // �ո�: AI(�ھڶǤJ)
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
                LogServise.Log("�L�k�إ�:" + _CreatureAI + " (�[�JAI����)");
                return null;
        }
        theCreature.SetAI(theAI);

        //�]�w�ҫ�
        GameObject CreatureModel = HuRougeLike2022Factory.GetAssetFactory().LoadCreatureModel(_Model);
        // ���J�ҫ�
        theCreature.SetGameObject(UnityEngine.Object.Instantiate(CreatureModel) as GameObject);
        //��m(���ե�)
        theCreature.GetGameObject().transform.position = new Vector3(5f, 5f, 0f);


        // UI
        // HP Bar
        theCreature.SetHPBar(new HPBar());
        theCreature.GetHPBar().barObj.transform.parent = theCreature.GetGameObject().transform;

        theCreature.GetHPBar().barObj.transform.position = new Vector3(5f, 4.8f, 0f);

        // �s���P�_
        theCreature.IsLive = true;

        return theCreature;
    }
}