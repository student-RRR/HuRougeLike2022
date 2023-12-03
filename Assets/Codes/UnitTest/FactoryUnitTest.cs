using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LogServise.Log("���ն}�l");

        //Creator_MethodType theCreator_MethodType = new ConcreteCreator_MethodType();
        //theCreator_MethodType.FactoryMethod(1);

        // �إߨ���u�t
        ICreatureFactory factory = HuRougeLike2022Factory.GetCreatureFactory();

        // �إߥͪ�
        ICharacter creature = factory.CreateCreature(ENUM_Creature.Elf,
                                                     ENUM_Body.Humanoid,
                                                    ENUM_Status.HumanChildStatus, 
                                                    ENUM_CreatureAI.HumanAI,
                                                    ENUM_Model.ModelElf);
        creature.UpdateAI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
