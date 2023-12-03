using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MapGenerator
{
    MapServise mapServise;

    /// 地圖相關
    // 地圖大小
    public int mapWidth = 18;
    public int mapHeight = 18;
    // 房間數量
    public int roomNum = 15;

    /// 房間相關
    // 地圖邊距(>0)
    public int padding = 2;
    // 房間大小區間
    public int widthMinRoom = 5;
    public int widthMaxRoom = 8;
    public int heightMinRoom = 5;
    public int heightMaxRoom = 8;

    /// 視覺化相關
    // 地板
    public Tilemap floorTileMap;
    public Tile floorTile;
    // 牆壁
    public Tilemap blockTileMap;
    //public GameObject blockTile;

    // 初始化地城
    public void InitializeDungeon()
    {
        // 取出資源
        // 地板
        var gridA = new GameObject("FloorGrid").AddComponent<Grid>();

        var tilemapA = new GameObject("FloorTilemap").AddComponent<Tilemap>();
        tilemapA.gameObject.AddComponent<TilemapRenderer>();

        tilemapA.transform.SetParent(gridA.gameObject.transform);

        //牆塊
        var gridB = new GameObject("BlockGrid").AddComponent<Grid>();

        var tilemapB = new GameObject("BlockTilemap").AddComponent<Tilemap>();
        tilemapB.gameObject.AddComponent<TilemapRenderer>();

        tilemapB.transform.SetParent(gridB.gameObject.transform);


        // 地板
        floorTileMap = tilemapA;
        floorTile = HuRougeLike2022Factory.GetAssetFactory().LoadMapGroundModel(ENUM_MapGround.ModelRockGround);

        // 牆壁
        blockTileMap = tilemapB;
        //blockTile = HuRougeLike2022Factory.GetAssetFactory().LoadMapBlockModel(ENUM_MapBlock.ModelRockBlock);


        // 實體化
        mapServise = new MapServise(mapWidth, mapHeight);

        // 實體化
        mapServise.RoomAndAreaInit(roomNum);
    }

    // 產生地圖
    public void GenerateDungeon()
    {
        // 產生01實心地圖
        var map01 =  mapServise.CreateInitMap();

        // 產生01房間
        mapServise.CreateRooms(roomNum);

        // 取得01地形資料
        var all01RoomMap = mapServise.GetAllRoom01Map(mapServise.GetRoomList());

        // 挖出01房間
        var map01withRoom = mapServise.DigMap(map01, all01RoomMap);

        // 轉換 01地圖陣列 至 實際物件種類
        mapServise.MapArtilize(map01withRoom);

        // 地圖分析(需改名成 Area相關)
        MapAnalyse();


        DrawMap(mapServise.GetBlockMap(), mapServise.GetFloorMap());// 地圖視覺化(根據陣列生成地圖)
    }

    // 地圖分析
    public void MapAnalyse()
    {
        // 找出所有區域
        mapServise.FindAllArea();

        // 找出 並設定最大區域設為主區域
        mapServise.MainAreaSetting();
    }

    // 地圖視覺化(根據陣列生成地圖)
    void DrawMap(Block[,] b_map, Floor[,] f_map)
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                // 生成地板(2D)
                if (f_map[x, y].floorType == "Dirt")
                    floorTileMap.SetTile(new Vector3Int(x, y, 0), floorTile);

                // 生成牆壁(3D)
                if (b_map[x, y].wallType == "Wall")
                    Set3DTile(x, y, b_map[x,y].gameObject, blockTileMap);
            }
        }
    }

    // 取得地圖
    public ArcMap GetMapManager()
    {
        return mapServise.mapManager;
    }

    // 印出01陣列地圖
    void Print01ArrayMap(bool[,] room)
    {
        
        Debug.Log(LogServise.CreateArrayMapString(room));
    }

    // 放置牆體(3D)
    void Set3DTile(int x, int y, GameObject @object, Tilemap @parent)
    {
        var obj = UnityEngine.Object.Instantiate(@object, @parent.transform) as GameObject;
        //var obj = Instantiate(@object, @parent.transform);
        obj.transform.position = new Vector3(x, y, -0.5f);
    }
}
