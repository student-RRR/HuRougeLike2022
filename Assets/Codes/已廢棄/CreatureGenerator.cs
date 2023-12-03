using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureGenerator : MonoBehaviour
{
    // 角色工廠
    ICreatureFactory factory;
    ICharacter creature;


    // 初始化
    internal void InitializeDungeon()
    {
        // 取得角色工廠
        factory = HuRougeLike2022Factory.GetCreatureFactory();
    }

    // 產生生物
    internal void GenerateCreature()
    {

        // 建立生物
        creature = factory.CreateCreature(ENUM_Creature.Elf,
                                                     ENUM_Body.Humanoid,
                                                    ENUM_Status.HumanChildStatus,
                                                    ENUM_CreatureAI.HumanAI,
                                                    ENUM_Model.ModelElf);

        //wish = new Creture_Human();
        //wish.SetAI(new HumanAI(wish));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // 調試用(移動)
        if (Input.GetKeyDown(KeyCode.W))
        {
            creature.Pos = (new Position2D(creature.Pos.x, creature.Pos.y + 1));
        }

        // 調試用(移動)
        if (Input.GetKeyDown(KeyCode.S))
        {
            creature.Pos = (new Position2D(creature.Pos.x, creature.Pos.y - 1));
        }

        // 調試用(移動)
        if (Input.GetKeyDown(KeyCode.A))
        {
            creature.Pos = (new Position2D(creature.Pos.x - 1, creature.Pos.y));
        }

        // 調試用(移動)
        if (Input.GetKeyDown(KeyCode.D))
        {
            creature.Pos = (new Position2D(creature.Pos.x + 1, creature.Pos.y));
        }
    }
}
