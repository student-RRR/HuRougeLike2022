using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Character����
public enum ENUM_Creature
{
    Null = 0,
    Elf = 1,// ���F
    Troll = 2,// �s��
    Ogre = 3,// �Ǫ�
    Catpive = 4,// �R�L
    Max,
}

// Character����ɭ�
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
