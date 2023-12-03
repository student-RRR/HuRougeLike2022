using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Գ��޲z�t��
/// </summary>
public class MapSystem : GameSystem
{
    private MapGenerator dungeonGenerator = new MapGenerator();     // �a�ϲ��;�
    private ArcMap m_arcmap = new ArcMap();             // ���a�a�ϥ���

    // �غc��
    public MapSystem(HuRougeLikeGame mediator) : base(mediator)
    {
        Initialize();
    }

    // ����-���a�a��
    public ArcMap CreateMap()
    {
        // �ͦ��a��
        LogServise.Log("�ͦ��a�Ϥ�");

        // ��l��
        dungeonGenerator.InitializeDungeon();

        // ���ͳ��a�a��
        dungeonGenerator.GenerateDungeon();

        // ���o�w���ͪ����a�a��, �æs�J����(m_mapManager)
        m_arcmap = dungeonGenerator.GetMapManager();

        return m_arcmap;
    }

    // �гy�ç�s-���aid�a��
    public void Create_and_Refleshfmap()
    {
        var length = m_arcmap.mapHeight;
        var height = m_arcmap.mapWidth;

        Field[,] fMap = new Field[length, height];

        // ����P�a�OID
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

    #region ���o�a�ϸ��

    // ���o�a��
    public ArcMap GetMap()
    {
        return m_arcmap;
    }

    // ���o���aid�a��
    public Field[,] GetFieldMap()
    {
        return m_arcmap.fieldMap;
    }

    // ���o��a��
    public Block[,] GetWall_01Map()
    {
        return m_arcmap.blockMap;
    }

    #endregion

    #region �]�w�a�ϸ��

    // �]�w ����
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

    // �]�w �a�O���
    public void SetFloorMap()
    {

    }

    #endregion

    // �~�Ӧ�IGameSystem
    public override void Initialize()
    {
        base.Initialize();
    }
    public override void Release() { Create_and_Refleshfmap(); }
    public override void Update() { }
}
