using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����޲z�t��
/// </summary>
public class CreatureSystem : GameSystem
{
    private CreatureManager m_creatureManager = new CreatureManager();
    private CreMap m_creMap;

    // �غc�l
    public CreatureSystem(HuRougeLikeGame mediator) : base(mediator)
    {
        Initialize();
        m_creatureManager.creatureList = new List<ICharacter>();

    }

    #region ����޲z

    // ���o���w�ͪ�
    public ICharacter GetCharacter(int index)
    {
        return m_creatureManager.creatureList[index];
    }

    // ���o���w�ͪ�by ID
    public ICharacter GetCharacterByID(int id)
    {
        foreach(ICharacter character in m_creatureManager.creatureList)
            if(character.GetID() == id)
                return character;

        LogServise.Log("���o���w�ͪ�by ID ����, ID:" + id);
        return null;
    }

    // ���o�ͪ��C��
    public List<ICharacter> GetCharacterList()
    {
        return m_creatureManager.creatureList;
    }

    // �W�[�ͪ�
    public void AddCreature(ICharacter character)
    {
        character.m_creatureSystem = this;
        m_creatureManager.creatureList.Add(character);
    }

    // �����ͪ�
    public void RemoveCreature(ICharacter character)
    {
        m_creatureManager.creatureList.Remove(character);
    }

    #endregion


    #region �ͪ��a��

    // ���ͥͪ��a��
    public CreMap CreateFieldMap()
    {
       var cretureList = m_creatureManager.creatureList;

       m_creMap = new CreMap(); // �ͪ�ID�a��

        // �ݳB�z
        m_creMap.mapWidth = 18;
        m_creMap.mapHeight = 18;

        var length = m_creMap.mapHeight;
        var height = m_creMap.mapWidth;

        Cield[,] cMap = new Cield[length, height];

        // �ͪ��a�Ϫ�l��
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < height; j++)
            {
                // ��l��
                cMap[i, j] = new Cield();

                // ��J�ŭ�-1
                cMap[i, j].creature_ID = -1;
            }
        }

        // �ͪ�id
        LogServise.Log("�ͪ��ƶq: " + cretureList.Count);
        for (int k = 0; k < cretureList.Count; k++)
        {
            // ���o�ӥͪ�ID
            var tempCreatureId = cretureList[k].GetID();

            var tempC_posX = cretureList[k].Pos.x;
            var tempC_posY = cretureList[k].Pos.y;

            LogServise.Log("["+ cretureList[k].GetName() + "] �ӥͪ�id: "+ tempCreatureId + ",  �Ҧb��m:(" + cretureList[k].Pos.x + ", " + cretureList[k].Pos.y+")");
            cMap[tempC_posX, tempC_posY].creature_ID = tempCreatureId;
        }

        m_creMap.SetCieldMap(cMap);
        return m_creMap;
    }

    // ���o�ͪ��a��
    public Cield[,] GetCieldMap()
    {
        if (m_creMap.GetCieldMap() == null)
        {
            LogServise.Log("CieldMap is null");
            return null;
        }
            
        return m_creMap.GetCieldMap();
    }


    // ���o�ͪ��a��
    public Cield GetCield(int x, int y)
    {
        if (m_creMap.GetCield(x,y) == null)
        {
            LogServise.Log("Cield is null");
            return null;
        }

        return m_creMap.GetCield(x,y);
    }

    // ��s�ͪ��a��
    public void Refleshfmap()
    {
        m_creMap = CreateFieldMap();
    }

    // �P�_�Ӯ榳�S���ͪ�
    public bool isThatACreature(Position2D pos)
    {
        if(m_creMap.GetCield(pos.x, pos.y).creature_ID != -1)
            return true;

        return false;
    }

    // �P�_�P��ͪ���m
    public List<Position2D> GetAxisCreaturePos(Position2D position)
    {
        List<Position2D> position2Ds = new List<Position2D>();
        foreach(var pos in RangeMathf.Get8MazeRange(position))
        {
            // �Y�Ӯ榳�ͪ�
            if (isThatACreature(pos))
                position2Ds.Add(pos);
        }

        return position2Ds;
    }

    // �P�_�P��d�򪺥ͪ���m
    public List<Position2D> GetRangeCreaturePos(Position2D[] range)
    {
        List<Position2D> position2Ds = new List<Position2D>();
        foreach (var pos in range)
        {
            // �Y�Ӯ榳�ͪ�
            if (isThatACreature(pos))
                position2Ds.Add(pos);
        }

        return position2Ds;
    }

    #endregion


    #region ��s

    public override void Initialize()
    {
        base.Initialize();
    }
    public override void Release() { }

    public override void Update()
    {
        UpdateAI();
    }

    // ��sAI(����ͪ��C��, �Ҧ��ͪ����欰)
    public void UpdateAI()
    {
        var cretureList = m_creatureManager.creatureList;

        // ���a�ͪ������
        cretureList[0].UpdateAI();

        // �ھڨ�L�ͪ��t�צ��
        for(int i = 1; i < cretureList.Count; i++)
        {
            cretureList[i].UpdateAI();
        }

        //foreach (ICharacter creature in cretureList)
        //    creature.UpdateAI();
    }

    #endregion
}