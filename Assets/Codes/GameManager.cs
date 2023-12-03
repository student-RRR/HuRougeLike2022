using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 已廢棄
/// </summary>
public class GameManager : MonoBehaviour
{
    MapGenerator dungeonGenerator;
    CreatureGenerator creatureGenerator;

    // Start is called before the first frame update
    void Start()
    {
        // 實體化
        //dungeonGenerator = GetComponent<MapGenerator>();
        dungeonGenerator = new MapGenerator();

        //creatureGenerator = GetComponent<CreatureGenerator>();

        // 初始化
        dungeonGenerator.InitializeDungeon();
        //creatureGenerator.InitializeDungeon();

        // 產生地圖
        dungeonGenerator.GenerateDungeon();

        // 產生生物
        //creatureGenerator.GenerateCreature();


    }

    // Update is called once per frame
    void Update()
    {
        //creatureGenerator.Update();
    }
}
