using System;
using System.Collections;
using System.Collections.Generic;
//using static MapMathf;
public class MapServise
{
    IAssetFactory assetFactory = HuRougeLike2022Factory.GetAssetFactory();

    public ArcMap mapManager = new ArcMap();

    // 地圖大小
    public int mapWidth;
    public int mapHeight;

    public MapServise(int _mapWidth, int _mapHeight)
    {
        mapWidth = _mapWidth;
        mapHeight = _mapHeight;

        // 實體化
        mapManager.blockMap = new Block[_mapWidth, _mapHeight];
        mapManager.floorsMap = new Floor[_mapWidth, _mapHeight];
        mapManager.fieldMap = new Field[_mapWidth, _mapHeight];
    }

    public void RoomAndAreaInit(int roomNum)
    {
        mapManager.rooms = new List<Room>();
        mapManager.areas = new List<Area>();
    }

    // 產生素體地圖(實心牆壁&地板)
    public bool[,] CreateInitMap()
    {
        return MapMathf.CreateSdseeMap(mapWidth, mapHeight);
    }

    // 產生指定數量房間
    public void CreateRooms(int roomNum)
    {
        Random random = new Random();

        // 在隨機位置上產生各個房間
        for (int n = 0; n < roomNum; n++)
        {
            // 產生房間
            bool[,] createdRoom = MapMathf.CreateRoom(mapWidth, mapHeight);

            // 記錄房間資訊
            mapManager.rooms.Add(new Room());
            mapManager.rooms[n].roomMap = createdRoom;
        }

    }

    // 挖出地圖
    public bool[,] DigMap(bool[,] map, bool[,] area)
    {
        // 產生(挖出)房間
        return MapMathf.BoolMapSub(map, area);
    }

    // 讀取01地圖陣列, 附值於各單位
    public void MapArtilize(bool[,] map)
    {
        // 布林陣列 轉 實際物件字串
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                // 牆壁
                mapManager.blockMap[x, y] = new Block();
                mapManager.blockMap[x, y].xPosition = x;
                mapManager.blockMap[x, y].yPosition = y;
                if (map[x, y] == true)
                {
                    mapManager.blockMap[x, y].wallType = "Wall";
                    mapManager.blockMap[x, y].gameObject = assetFactory.LoadMapBlockModel(ENUM_MapBlock.ModelRockBlock);
                }

                // 地板
                mapManager.floorsMap[x, y] = new Floor();
                mapManager.floorsMap[x, y].xPosition = x;
                mapManager.floorsMap[x, y].yPosition = y;
                mapManager.floorsMap[x, y].floorType = "Dirt";
            }
        }
    }

    // 找出所有區域
    public void FindAllArea()
    {
        int mark = 0;
        int[] ptr = new int[mapManager.rooms.Count];

        /// 取得分群陣列
        // 起點
        int nowRoom = -1;

        // 找起點
        for (int i = 0; i < mapManager.rooms.Count; i++)
            if (ptr[i] == 0)
            {
                nowRoom = i;

                //上色
                ptr[nowRoom] = 1;

                break;
            }

        // Error
        if (nowRoom == -1)
            return;

        while (Array.IndexOf(ptr, 0) != -1)
            ptr = AreaGrouping(mapManager.rooms, ptr, ++mark, nowRoom);


        // 用分群陣列 整理出 Area

        // 對於各個群
        for (int i = 1; i <= mark; i++)
        {
            LogServise.Log(i.ToString());

            List<Room> tempRoomList = new List<Room>();

            // 對於群中各個Room
            for (int p = 0; p < ptr.Length; p++)
            {
                // 若該Room屬於同群, 則塞入暫存Area


                if (ptr[p] == i)
                    tempRoomList.Add(mapManager.rooms[p]);
            }

            mapManager.areas.Add(
                    new Area
                    {
                        // 暫存Area 填入
                        roomList = tempRoomList
                    }
                );
        }
    }

    // 找出最大區域設為主區域
    public void MainAreaSetting()
    {
        // 找出最大區域
        int max = -1;
        int maxPtr = -1;

        for(int a = 0; a<mapManager.areas.Count; a++)
        {
            int tempMax = MapMathf.mapAreaSum(mapManager.areas[a].areaMap);

            if (tempMax > max)
            {
                max = tempMax;
                maxPtr = a;
            }
        }

        mapManager.areas[maxPtr].isMainArea = true;
        LogServise.Log(maxPtr.ToString()+" is mainArea!!!");
    }


    // 分群
    public int[] AreaGrouping(List<Room> roomList, int[] ptr, int mark, int nowPtr)
    {
        // 找連接點
        for (int i = 0; i < roomList.Count; i++)
        {
            if (ptr[i] == 1)
                continue;

            if (isAxisRoom(roomList[nowPtr], roomList[i]))
            {
                //上色
                ptr[i] = mark;
                AreaGrouping(roomList, ptr, mark, i);
            }
        }

        return ptr;
    }

    // 是否相鄰
    public bool isAxisRoom(Room roomA, Room roomB)
    {
        var ra = MapMathf.Map01Emit(roomA.roomMap, 1);
        LogServise.Log(LogServise.CreateArrayMapString(ra));

        var rb = roomB.roomMap;

        int mapW = roomA.roomMap.GetLength(0);
        int mapH = roomA.roomMap.GetLength(1);

        for (int x = 0; x < mapW; x++)
        {
            for (int y = 0; y < mapH; y++)
            {
                if (ra[x, y] & rb[x, y])
                {
                    return true;
                }
            }
        }

        return false;
    }

    // 餵入房間list輸出區域
    public bool[,] GetAllRoom01Map(List<Room> rooms)
    {
        return MapMathf.BoolMapArrayAdd(rooms);
    }

    public Block[,] GetBlockMap()
    {
        return mapManager.blockMap;
    }

    public Floor[,] GetFloorMap()
    {
        return mapManager.floorsMap;
    }

    public List<Room> GetRoomList()
    {
        return mapManager.rooms;
    }

    public List<Area> GetBoolArea()
    {
        return mapManager.areas;
    }
}
