using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色管理系統
/// </summary>
public class CreatureSystem : GameSystem
{
    private CreatureManager m_creatureManager = new CreatureManager();
    private CreMap m_creMap;

    // 建構子
    public CreatureSystem(HuRougeLikeGame mediator) : base(mediator)
    {
        Initialize();
        m_creatureManager.creatureList = new List<ICharacter>();

    }

    #region 角色管理

    // 取得指定生物
    public ICharacter GetCharacter(int index)
    {
        return m_creatureManager.creatureList[index];
    }

    // 取得指定生物by ID
    public ICharacter GetCharacterByID(int id)
    {
        foreach(ICharacter character in m_creatureManager.creatureList)
            if(character.GetID() == id)
                return character;

        LogServise.Log("取得指定生物by ID 失敗, ID:" + id);
        return null;
    }

    // 取得生物列表
    public List<ICharacter> GetCharacterList()
    {
        return m_creatureManager.creatureList;
    }

    // 增加生物
    public void AddCreature(ICharacter character)
    {
        character.m_creatureSystem = this;
        m_creatureManager.creatureList.Add(character);
    }

    // 移除生物
    public void RemoveCreature(ICharacter character)
    {
        m_creatureManager.creatureList.Remove(character);
    }

    #endregion


    #region 生物地圖

    // 產生生物地圖
    public CreMap CreateFieldMap()
    {
       var cretureList = m_creatureManager.creatureList;

       m_creMap = new CreMap(); // 生物ID地圖

        // 待處理
        m_creMap.mapWidth = 18;
        m_creMap.mapHeight = 18;

        var length = m_creMap.mapHeight;
        var height = m_creMap.mapWidth;

        Cield[,] cMap = new Cield[length, height];

        // 生物地圖初始化
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < height; j++)
            {
                // 初始化
                cMap[i, j] = new Cield();

                // 塞入空值-1
                cMap[i, j].creature_ID = -1;
            }
        }

        // 生物id
        LogServise.Log("生物數量: " + cretureList.Count);
        for (int k = 0; k < cretureList.Count; k++)
        {
            // 取得該生物ID
            var tempCreatureId = cretureList[k].GetID();

            var tempC_posX = cretureList[k].Pos.x;
            var tempC_posY = cretureList[k].Pos.y;

            LogServise.Log("["+ cretureList[k].GetName() + "] 該生物id: "+ tempCreatureId + ",  所在位置:(" + cretureList[k].Pos.x + ", " + cretureList[k].Pos.y+")");
            cMap[tempC_posX, tempC_posY].creature_ID = tempCreatureId;
        }

        m_creMap.SetCieldMap(cMap);
        return m_creMap;
    }

    // 取得生物地圖
    public Cield[,] GetCieldMap()
    {
        if (m_creMap.GetCieldMap() == null)
        {
            LogServise.Log("CieldMap is null");
            return null;
        }
            
        return m_creMap.GetCieldMap();
    }


    // 取得生物地圖
    public Cield GetCield(int x, int y)
    {
        if (m_creMap.GetCield(x,y) == null)
        {
            LogServise.Log("Cield is null");
            return null;
        }

        return m_creMap.GetCield(x,y);
    }

    // 更新生物地圖
    public void Refleshfmap()
    {
        m_creMap = CreateFieldMap();
    }

    // 判斷該格有沒有生物
    public bool isThatACreature(Position2D pos)
    {
        if(m_creMap.GetCield(pos.x, pos.y).creature_ID != -1)
            return true;

        return false;
    }

    // 判斷周圍生物位置
    public List<Position2D> GetAxisCreaturePos(Position2D position)
    {
        List<Position2D> position2Ds = new List<Position2D>();
        foreach(var pos in RangeMathf.Get8MazeRange(position))
        {
            // 若該格有生物
            if (isThatACreature(pos))
                position2Ds.Add(pos);
        }

        return position2Ds;
    }

    // 判斷周圍範圍的生物位置
    public List<Position2D> GetRangeCreaturePos(Position2D[] range)
    {
        List<Position2D> position2Ds = new List<Position2D>();
        foreach (var pos in range)
        {
            // 若該格有生物
            if (isThatACreature(pos))
                position2Ds.Add(pos);
        }

        return position2Ds;
    }

    #endregion


    #region 更新

    public override void Initialize()
    {
        base.Initialize();
    }
    public override void Release() { }

    public override void Update()
    {
        UpdateAI();
    }

    // 更新AI(執行生物列表中, 所有生物的行為)
    public void UpdateAI()
    {
        var cretureList = m_creatureManager.creatureList;

        // 玩家生物先行動
        cretureList[0].UpdateAI();

        // 根據其他生物速度行動
        for(int i = 1; i < cretureList.Count; i++)
        {
            cretureList[i].UpdateAI();
        }

        //foreach (ICharacter creature in cretureList)
        //    creature.UpdateAI();
    }

    #endregion
}