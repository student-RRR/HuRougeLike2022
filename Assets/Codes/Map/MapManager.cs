using System.Collections;
using System; // So the script can use the serialization commands
using System.Collections.Generic;
using UnityEngine;
using static MapMathf;


[Serializable] // Makes the class serializable so it can be saved out to a file
public class MapManager
{
    public ArcMap arcMap;
    public CreMap creMap;
}

[Serializable] // Makes the class serializable so it can be saved out to a file
public class ArcMap
{
    // 地圖大小
    public int mapWidth;
    public int mapHeight;

    public Block[,] blockMap; // 紀錄牆壁
    public Floor[,] floorsMap; // 紀錄地板

    public List<Room> rooms; // 紀錄地圖上的房間
    public List<Area> areas; // 紀錄地圖上的區域(相連的房間組成區域)

    public Field[,] fieldMap; // 紀錄id地圖(可多重存在)
}

[Serializable] // Makes the class serializable so it can be saved out to a file
public class CreMap
{
    private Cield[,] cieldMap; // 生物ID地圖

    // 地圖大小
    public int mapWidth;
    public int mapHeight;

    public Cield GetCield(int _x, int _y)
    {
        if (_x < 0 || _x >= mapWidth || _y < 0 || _y >= mapHeight)
        {
            //???
            var temp = new Cield();
            temp.creature_ID = -1;
            return temp;
        }

        return cieldMap[_x, _y];
    }

    public Cield[,] GetCieldMap()
    {
        return cieldMap;
    }

    public void SetCieldMap(Cield[,] cields)
    {
        cieldMap = cields;
    }

}

[Serializable] // Makes the class serializable so it can be saved out to a file
public class Block
{ // Holds all the information for each tile on the map
    public int id;
    public int xPosition; // the position on the x axis
    public int yPosition; // the position on the y axis
    [NonSerialized]
    public GameObject gameObject; // the map game object attached to that position: a floor, a wall, etc.
    public string wallType; // 牆壁種類
}

[Serializable]
public class Floor
{
    public int id;
    public int xPosition; // the position on the x axis
    public int yPosition; // the position on the y axis
    public string floorType; // 地板種類

    public bool isMist; // 是否迷霧壟罩
}


[Serializable] // Makes the class serializable so it can be saved out to a file
public class Field
{
    public int wall_ID;
    public int floor_ID;

    public bool isWall()
    {
        return (wall_ID != -1) ? true : false;
    }
    public bool isFloor()
    {
        return (floor_ID != -1) ? true : false;
    }
}

[Serializable] // Makes the class serializable so it can be saved out to a file
public class Cield
{
    public int creature_ID;

    public bool isCreature()
    {
        //LogServise.Log((creature_ID != -1) ? "是生物" : "不是生物");
        //LogServise.Log("creature_ID: " + creature_ID);
        return (creature_ID != -1) ? true : false;
    }
}

[Serializable]
public class Room
{
    //public int xPosition; // the position on the x axis
    //public int yPosition; // the position on the y axis
    //public int width; // 房間長寬
    //public int height; // 房間長寬

    //public string roomType;// 房間種類
    public bool[,] roomMap; // 房間遮罩陣列

}

[Serializable]
public class Area
{
    public bool isMainArea = false;// 是否為主要區域
    public bool isMistArea = true;// 是否為迷霧區域
    public List<Room> roomList;// 該區域包含的所有房間

    // 區域面積
    public int areaCulculate
    {
        get 
        {
            int tempArea = 0;

            foreach(var room in roomList)
            {
                tempArea += MapMathf.mapAreaSum(room.roomMap);
            }
            return areaCulculate; 
        }
    }

    public bool[,] areaMap
    {
        get
        {
            return MapMathf.BoolMapArrayAdd(roomList);
        }
    }// 區域遮罩陣列
}