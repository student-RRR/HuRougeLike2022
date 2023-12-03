using System;
using System.Collections;
using System.Collections.Generic;

public static class MapMathf
{
    // 01疊加 兩個布林地圖
    public static bool[,] BoolMapAdd(bool[,] mapA, bool[,] mapB)
    {
        var width = mapA.GetLength(0);
        var height = mapA.GetLength(1);

        bool[,] mapResult = new bool[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                mapResult[x, y] = mapA[x, y] | mapB[x, y];
            }
        }

        return mapResult;
    }

    // 01減去 兩個布林地圖
    public static bool[,] BoolMapSub(bool[,] mapA, bool[,] mapB)
    {
        var width = mapA.GetLength(0);
        var height = mapA.GetLength(1);

        bool[,] mapResult = new bool[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                mapResult[x, y] = mapA[x, y] & !mapB[x, y];
            }
        }

        return mapResult;
    }


    // 疊加 一組布林地圖
    public static bool[,] BoolMapArrayAdd(List<Room> rooms)
    {
        if (rooms == null)
            return null;

        bool[,] mapResult = null;

        foreach (Room room in rooms)
        {
            if (room == null)
                break;

            if (mapResult == null)
            {
                mapResult = room.roomMap;
            }
            else
            {
                mapResult = BoolMapAdd(mapResult, room.roomMap);
            }
        }

        return mapResult;
    }

    // 產生01素體地圖(實心牆壁&地板)
    public static bool[,] CreateSdseeMap(int mapWidth, int mapHeight)
    {
        bool[,] roomBoolMap = new bool[mapWidth, mapHeight];

        // 地形初始化(塞滿牆壁與地板)
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                roomBoolMap[x, y] = true;
            }
        }

        return roomBoolMap;
    }

    // 產生01房間(二維布林陣列)
    public static bool[,] CreateRoom(int mapWidth, int mapHeight, int widthMinRoom = 5, int widthMaxRoom = 10, int heightMinRoom = 3, int heightMaxRoom = 8, int padding = 3)
    {
        Random random = new Random();
        bool[,] roomBoolMap = new bool[mapWidth, mapHeight];

        // 生成隨機房間中心(避開邊框)
        var x = random.Next(0 + padding, mapWidth - padding);
        var y = random.Next(0 + padding, mapHeight - padding);
        // 生成隨機房間長寬
        var roomWidth = random.Next(widthMinRoom, widthMaxRoom);
        var roomHeight = random.Next(heightMinRoom, heightMaxRoom);

        // 產生(挖出)房間
        for (int w = -roomWidth / 2; w < roomWidth / 2; w++)
        {
            for (int h = -roomHeight / 2; h < roomHeight / 2; h++)
            {
                var posX = x + w;
                var posY = y + h;
                if (posX > 0 && posX < mapWidth - 1 &&
                    posY > 0 && posY < mapHeight - 1)
                    roomBoolMap[posX, posY] = true;
            }
        }
        return roomBoolMap;
    }

    // 擴散n格
    public static bool[,] Map01Emit(bool[,] map, int n)
    {
        bool[,] filter = { { true, true, true }, 
                           { true, true, true }, 
                           { true, true, true } };

        bool[,] reaultMap = new bool[map.GetLength(0), map.GetLength(1)];
        for(int x=0;x< map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                reaultMap[x, y] = map[x, y];
            }
        }

        for(int i = n; i > 0; i--)
        {
            reaultMap = Filter(map, filter);
        }

        return reaultMap;
    }

    // 濾波
    public static bool[,] Filter(bool[,] map, bool[,] filter)
    {
        int mapW = map.GetLength(0);
        int mapH = map.GetLength(1);

        int filterW = filter.GetLength(0);
        int filterH = filter.GetLength(1);

        bool[,] reaultMap = new bool[mapW, mapH];

        for (int x = 0; x < mapW; x++)
        {
            for (int y = 0; y < mapH; y++)
            {
                var halfFilterW = (filterW - 1) / 2;
                var halfFilterH = (filterH - 1) / 2;

                for (int fx = -halfFilterW; fx <= halfFilterW; fx++)
                {
                    for (int fy = -halfFilterW; fy <= halfFilterH; fy++)
                    {
                        if (x + fx >= 0 && x + fx < mapW &&
                            y + fy >= 0 && y + fy < mapH)
                        {
                            reaultMap[x + fx, y + fy] = map[x, y] | filter[fx + halfFilterW, fy + halfFilterH];
                        }
                    }
                } 
            }
        }

        return reaultMap;
    }

    public static int mapAreaSum(bool[,] map)
    {
        int tempSum = 0;

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if(map[x,y] == true)
                {
                    tempSum += 1;
                }
            }
        }

        return tempSum;
    }
}
