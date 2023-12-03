using System;
using System.Collections;
using System.Collections.Generic;
//using static MapMathf;
public class MapServise
{
    IAssetFactory assetFactory = HuRougeLike2022Factory.GetAssetFactory();

    public ArcMap mapManager = new ArcMap();

    // �a�Ϥj�p
    public int mapWidth;
    public int mapHeight;

    public MapServise(int _mapWidth, int _mapHeight)
    {
        mapWidth = _mapWidth;
        mapHeight = _mapHeight;

        // �����
        mapManager.blockMap = new Block[_mapWidth, _mapHeight];
        mapManager.floorsMap = new Floor[_mapWidth, _mapHeight];
        mapManager.fieldMap = new Field[_mapWidth, _mapHeight];
    }

    public void RoomAndAreaInit(int roomNum)
    {
        mapManager.rooms = new List<Room>();
        mapManager.areas = new List<Area>();
    }

    // ���ͯ���a��(������&�a�O)
    public bool[,] CreateInitMap()
    {
        return MapMathf.CreateSdseeMap(mapWidth, mapHeight);
    }

    // ���ͫ��w�ƶq�ж�
    public void CreateRooms(int roomNum)
    {
        Random random = new Random();

        // �b�H����m�W���ͦU�өж�
        for (int n = 0; n < roomNum; n++)
        {
            // ���ͩж�
            bool[,] createdRoom = MapMathf.CreateRoom(mapWidth, mapHeight);

            // �O���ж���T
            mapManager.rooms.Add(new Room());
            mapManager.rooms[n].roomMap = createdRoom;
        }

    }

    // ���X�a��
    public bool[,] DigMap(bool[,] map, bool[,] area)
    {
        // ����(���X)�ж�
        return MapMathf.BoolMapSub(map, area);
    }

    // Ū��01�a�ϰ}�C, ���ȩ�U���
    public void MapArtilize(bool[,] map)
    {
        // ���L�}�C �� ��ڪ���r��
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                // ���
                mapManager.blockMap[x, y] = new Block();
                mapManager.blockMap[x, y].xPosition = x;
                mapManager.blockMap[x, y].yPosition = y;
                if (map[x, y] == true)
                {
                    mapManager.blockMap[x, y].wallType = "Wall";
                    mapManager.blockMap[x, y].gameObject = assetFactory.LoadMapBlockModel(ENUM_MapBlock.ModelRockBlock);
                }

                // �a�O
                mapManager.floorsMap[x, y] = new Floor();
                mapManager.floorsMap[x, y].xPosition = x;
                mapManager.floorsMap[x, y].yPosition = y;
                mapManager.floorsMap[x, y].floorType = "Dirt";
            }
        }
    }

    // ��X�Ҧ��ϰ�
    public void FindAllArea()
    {
        int mark = 0;
        int[] ptr = new int[mapManager.rooms.Count];

        /// ���o���s�}�C
        // �_�I
        int nowRoom = -1;

        // ��_�I
        for (int i = 0; i < mapManager.rooms.Count; i++)
            if (ptr[i] == 0)
            {
                nowRoom = i;

                //�W��
                ptr[nowRoom] = 1;

                break;
            }

        // Error
        if (nowRoom == -1)
            return;

        while (Array.IndexOf(ptr, 0) != -1)
            ptr = AreaGrouping(mapManager.rooms, ptr, ++mark, nowRoom);


        // �Τ��s�}�C ��z�X Area

        // ���U�Ӹs
        for (int i = 1; i <= mark; i++)
        {
            LogServise.Log(i.ToString());

            List<Room> tempRoomList = new List<Room>();

            // ���s���U��Room
            for (int p = 0; p < ptr.Length; p++)
            {
                // �Y��Room�ݩ�P�s, �h��J�ȦsArea


                if (ptr[p] == i)
                    tempRoomList.Add(mapManager.rooms[p]);
            }

            mapManager.areas.Add(
                    new Area
                    {
                        // �ȦsArea ��J
                        roomList = tempRoomList
                    }
                );
        }
    }

    // ��X�̤j�ϰ�]���D�ϰ�
    public void MainAreaSetting()
    {
        // ��X�̤j�ϰ�
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


    // ���s
    public int[] AreaGrouping(List<Room> roomList, int[] ptr, int mark, int nowPtr)
    {
        // ��s���I
        for (int i = 0; i < roomList.Count; i++)
        {
            if (ptr[i] == 1)
                continue;

            if (isAxisRoom(roomList[nowPtr], roomList[i]))
            {
                //�W��
                ptr[i] = mark;
                AreaGrouping(roomList, ptr, mark, i);
            }
        }

        return ptr;
    }

    // �O�_�۾F
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

    // ���J�ж�list��X�ϰ�
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
