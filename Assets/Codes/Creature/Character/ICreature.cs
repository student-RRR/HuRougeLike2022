using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Character類型
public enum ENUM_Creature
{
    Null = 0,
    Elf = 1,// 精靈
    Troll = 2,// 山妖
    Ogre = 3,// 怪物
    Catpive = 4,// 俘兵
    Max,
}

// Character角色界面
public abstract class ICreature : ICharacter
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
