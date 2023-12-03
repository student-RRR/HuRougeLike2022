using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 戰場管理系統
/// </summary>
public class MapSystem : GameSystem
{
    private MapGenerator dungeonGenerator = new MapGenerator();     // 地圖產生器
    private ArcMap m_arcmap = new ArcMap();             // 場地地圖本體

    // 建構者
    public MapSystem(HuRougeLikeGame mediator) : base(mediator)
    {
        Initialize();
    }

    // 產生-場地地圖
    public ArcMap CreateMap()
    {
        // 生成地圖
        LogServise.Log("生成地圖中");

        // 初始化
        dungeonGenerator.InitializeDungeon();

        // 產生場地地圖
        dungeonGenerator.GenerateDungeon();

        // 取得已產生的場地地圖, 並存入載體(m_mapManager)
        m_arcmap = dungeonGenerator.GetMapManager();

        return m_arcmap;
    }

    // 創造並更新-場地id地圖
    public void Create_and_Refleshfmap()
    {
        var length = m_arcmap.mapHeight;
        var height = m_arcmap.mapWidth;

        Field[,] fMap = new Field[length, height];

        // 牆體與地板ID
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < height; j++)
            {
                fMap[i, j] = new Field();

                var tempBlockId = m_arcmap.blockMap[i, j].id;
                var tempFloorId = m_arcmap.floorsMap[i, j].id;
                //LogServise.Log(" m_mapManager.blockMap[i, j].id:" + m_mapManager.blockMap[i, j].id);

                //fMap[i, j].wall_ID = tempBlockId != 0 ? tempBlockId : -1;
                //fMap[i, j].creature_ID = tempFloorId != 0 ? tempFloorId : -1;
                fMap[i, j].wall_ID = 1;
            }
        }

        m_arcmap.fieldMap = fMap;
    }

    #region 取得地圖資料

    // 取得地圖
    public ArcMap GetMap()
    {
        return m_arcmap;
    }

    // 取得場地id地圖
    public Field[,] GetFieldMap()
    {
        return m_arcmap.fieldMap;
    }

    // 取得牆地圖
    public Block[,] GetWall_01Map()
    {
        return m_arcmap.blockMap;
    }

    #endregion

    #region 設定地圖資料

    // 設定 牆資料
    public void SetWallMap(Block[,] blocks)
    {
        for (int i = 0; i < m_arcmap.fieldMap.GetLength(0); i++)
        {
            for (int j = 0; j < m_arcmap.fieldMap.GetLength(1); j++)
            {
                m_arcmap.fieldMap[i, j].floor_ID = blocks[i, j].id;
            }
        }
    }

    // 設定 地板資料
    public void SetFloorMap()
    {

    }

    #endregion

    // 繼承自IGameSystem
    public override void Initialize()
    {
        base.Initialize();
    }
    public override void Release() { Create_and_Refleshfmap(); }
    public override void Update() { }
}
