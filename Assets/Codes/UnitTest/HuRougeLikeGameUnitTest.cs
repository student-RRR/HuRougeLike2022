using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuRougeLikeGameUnitTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnitTest_CreatureCreate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnitTest_CreatureCreate()
    {
        // 取得角色工廠
        ICreatureFactory factory = HuRougeLike2022Factory.GetCreatureFactory();

        // 建立生物
        ICharacter creature = factory.CreateCreature(ENUM_Creature.Elf,
                                                     ENUM_Body.Humanoid,
                                                     ENUM_Status.HumanChildStatus,
                                                     ENUM_CreatureAI.HumanAI,
                                                    ENUM_Model.ModelElf);
        //creature.UpdateAI();
    }
}
