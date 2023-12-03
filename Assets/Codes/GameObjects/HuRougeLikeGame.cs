using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主遊戲的系統終端(即時)
/// </summary>
public class HuRougeLikeGame
{
    #region Singleton模版

    // Singleton模版
    private static HuRougeLikeGame _instance = null;
    public static HuRougeLikeGame Instance
    {
        get
        {
            if (_instance == null)
                _instance = new HuRougeLikeGame();
            return _instance;
        }
    }

    #endregion

    // 地圖總存放者
    MapManager m_mapManager = new MapManager();

    // 遊戲系統
    private StageSystem m_stageSystem = null;
    private CreatureSystem m_creatureSystem = null;
    private MapSystem m_fieldSystem = null;
    private EffectSystem m_effectSystem = null;
    //private InputSystem m_inputSystem = null;
    private UISystem m_uiSystem = null;

    // 初始化
    public void Initialize()
    {
        LogServise.Log("-系統注入-");

        // 遊戲系統
        m_stageSystem = new StageSystem(this);      // 關卡系統
        m_creatureSystem = new CreatureSystem(this);// 生物系統
        m_fieldSystem = new MapSystem(this);        // 戰場系統
        m_effectSystem = new EffectSystem(this);
        //m_inputSystem = new InputSystem(this);    // 輸入系統
        m_uiSystem = new UISystem(this);            // UI系統
        ///////////////////////////////////////////////////////////

        m_mapManager.arcMap = m_fieldSystem.CreateMap();                  // 產生戰場地圖
        m_mapManager.creMap = m_creatureSystem.CreateFieldMap();     // 產生生物地圖

        // 建立角色工廠
        ICreatureFactory factory = HuRougeLike2022Factory.GetCreatureFactory();

        // 建立生物
        #region 測試
        ICharacter creature = factory.CreateCreature(ENUM_Creature.Elf,
                                                     ENUM_Body.Humanoid,
                                                     ENUM_Status.HumanChildStatus,
                                                     ENUM_CreatureAI.HumanAI,
                                                     ENUM_Model.ModelElf);
        creature.SetID(1);
        creature.SetName("精靈A");
        AddCreature(creature); // 加入生物紀錄中


        creature = factory.CreateCreature(ENUM_Creature.Elf,
                                                     ENUM_Body.Humanoid,
                                                     ENUM_Status.HumanChildStatus,
                                                     ENUM_CreatureAI.WandererAI,
                                                     ENUM_Model.ModelElf);
        creature.SetID(2);
        creature.SetName("精靈B");
        AddCreature(creature); // 加入生物紀錄中

        ///////////////////怪物

        creature = factory.CreateCreature(ENUM_Creature.Ogre,
                                                     ENUM_Body.Humanoid,
                                                     ENUM_Status.HumanChildStatus,
                                                     ENUM_CreatureAI.HunterAI,
                                                     ENUM_Model.ModelOgre);
        creature.SetID(3);
        creature.SetName("食人魔A");
        AddCreature(creature); // 加入生物紀錄中



        #endregion

        m_fieldSystem.Create_and_Refleshfmap(); // 更新戰場地圖
        m_creatureSystem.Refleshfmap();
    }

    public virtual void Release()
    {

    }

    // 遊戲系統更新
    public void Update()
    {
        
        // 同步更新
        if (InputProcess())// 玩家輸入
        {
            m_stageSystem.Update();     // 關卡系統更新
            m_fieldSystem.Update();
            m_creatureSystem.Update();  // 生物系統更新

            LogServise.Log("===============回合線=================");
            // 回合線=========================
            m_fieldSystem.Create_and_Refleshfmap(); // 更新戰場地圖
            m_creatureSystem.Refleshfmap(); // 更新生物地圖
        }

        // 非同步更新
        //m_inputSystem.Update();
        m_effectSystem.Update(); // 特效系統更新
        m_uiSystem.Update(); // UI系統更新
    }


    // 玩家輸入
    private bool InputProcess()
    {
        //LogServise.Log("等待玩家輸入...");

        var player = m_creatureSystem.GetCharacter(0);

        // 調試用(移動)
        if (Input.GetKeyDown(KeyCode.W)) { player.GetAI().March(Direction.Up); return true; }

        // 調試用(移動)
        if (Input.GetKeyDown(KeyCode.S)) { player.GetAI().March( Direction.Down); return true; }

        // 調試用(移動)
        if (Input.GetKeyDown(KeyCode.A)) { player.GetAI().March(Direction.Left); return true; }

        // 調試用(移動)
        if (Input.GetKeyDown(KeyCode.D)) { player.GetAI().March(Direction.Right); return true; }

        return false;
    }

    // 增加生物
    public void AddCreature(ICharacter theCreture)
    {
        if (m_creatureSystem != null)
            m_creatureSystem.AddCreature(theCreture);
        // 更新戰場系統中的生物列表
    }

    // 移除生物

    // 取得生物列表
    public List<ICharacter> GetCreaturesList()
    {
        if (m_creatureSystem != null)
            return m_creatureSystem.GetCharacterList();

        return null;        
    }

    // 地圖

    // 取得牆地圖
    public Block[,] GetWallMap()
    {
        return m_fieldSystem.GetWall_01Map();
    }


    // 取得指定格 生物ID
    public int GetThatCreatureID(Position2D position)
    {
        return m_creatureSystem.GetCieldMap()[position.x, position.y].creature_ID;
    }

    // 取得指定生物by ID
    public ICharacter GetCharacterByID(int id)
    {
        return m_creatureSystem.GetCharacterByID(id);
    }

    // 判斷指定格 生物
    public bool isThatCreature(int _x, int _y)
    {
        if (m_creatureSystem.GetCieldMap() == null)
        {
            LogServise.Log("creatureSystem.cieldMap is mull");
            return false;
        }

        LogServise.Log("格子_生物確認: x:"+_x+"y:"+_y+">>>>" + m_creatureSystem.GetCieldMap()[_x, _y].isCreature());
        return m_creatureSystem.GetCieldMap()[_x, _y].isCreature();
    }

    // 取得指定格 生物
    public ICharacter GetThatCreature(Position2D position)
    {
        int id = GetThatCreatureID(position);

        return GetCharacterByID(id);
    }

}
