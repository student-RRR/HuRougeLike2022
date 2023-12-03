using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureGenerator : MonoBehaviour
{
    // ����u�t
    ICreatureFactory factory;
    ICharacter creature;


    // ��l��
    internal void InitializeDungeon()
    {
        // ���o����u�t
        factory = HuRougeLike2022Factory.GetCreatureFactory();
    }

    // ���ͥͪ�
    internal void GenerateCreature()
    {

        // �إߥͪ�
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

        // �ոե�(����)
        if (Input.GetKeyDown(KeyCode.W))
        {
            creature.Pos = (new Position2D(creature.Pos.x, creature.Pos.y + 1));
        }

        // �ոե�(����)
        if (Input.GetKeyDown(KeyCode.S))
        {
            creature.Pos = (new Position2D(creature.Pos.x, creature.Pos.y - 1));
        }

        // �ոե�(����)
        if (Input.GetKeyDown(KeyCode.A))
        {
            creature.Pos = (new Position2D(creature.Pos.x - 1, creature.Pos.y));
        }

        // �ոե�(����)
        if (Input.GetKeyDown(KeyCode.D))
        {
            creature.Pos = (new Position2D(creature.Pos.x + 1, creature.Pos.y));
        }
    }
}
