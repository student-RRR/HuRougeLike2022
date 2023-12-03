using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ENUM_Character
{
    Human,
    Slime
}

/// <summary>
/// 生物界面
/// </summary>
public abstract class ICharacter
{
    public CreatureSystem m_creatureSystem;
    public ICharacter(CreatureSystem _creatureSystem)
    {
        m_creatureSystem = _creatureSystem;
    }

    // 參數
    protected int id { get; set; }                    // ID
    protected string name { get; set; }               // 名字        
    protected int LV { get; set; }                    // 等級
    protected GameObject m_GameObject { get; set; }   // Uniyt模型    
    protected IBody m_body { get; set; }              // 身形    
    protected IStatus m_status { get; set; }          // 能力值     
    protected ICreatureAI m_creatureAI { get; set; }  // AI
    protected HPBar m_hPBar { get; set; }             // HP Bar    

    private bool islive;                              // 存活與否
    public bool IsLive 
    {
        get => islive;
        set 
        {
            if(islive == true && value == false) // 若由生轉死
            {
                this.Dead();
            }
            islive = value;
        }
    }               

    #region 位置座標

    // 位置座標
    protected Position2D pos;
    public Position2D Pos
    {
        get 
        { 
            if(pos == null) return new Position2D(5,5);
            return pos; 
        }
        set
        {
            pos = value;
            this.m_GameObject.transform.position = new Vector3(pos.x, pos.y, 0f);

            m_creatureSystem.Refleshfmap(); // 更新生物地圖
        }
    }

    #endregion

    #region 基本資料

    // 取得名字
    public string GetName()
    {
        return this.name;
    }
    public void SetName(string _name)
    {
        this.name = _name;
    }


    public int GetID()
    {
        return this.id;
    }
    public void SetID(int _id)
    {
        this.id = _id;
    }

    public void SetState(IStatus status)
    {
        this.m_status = status;
    }

    public IStatus GetState()
    {
        return this.m_status;
    }

    #endregion

    #region Unity模型

    // 設定Unity模型
    public void SetGameObject(GameObject theGameObject)
    {
        m_GameObject = theGameObject;
    }

    // 取得Unity模型
    public GameObject GetGameObject()
    {
        return m_GameObject;
    }
    #endregion

    #region 移動相關
    // 測試用(移動)
    public bool MoveDirection(Position2D _pos)
    {
        // 判斷是否能移動
        if (!moveAble(_pos))
            return false;

        //LogServise.Log("[" + name + "] 移動至:(" + _pos.x + "," + _pos.y + ")");
        Pos = _pos;
        return true;
    }

    private bool moveAble(Position2D pos)
    {
        //MapServise
        var wallMap = HuRougeLikeGame.Instance.GetWallMap();
        //var creaturesList = HuRougeLikeGame.Instance.GetCreaturesList();

        if (wallMap[pos.x, pos.y].wallType != null)// 代表有牆
            return false;

        if (HuRougeLikeGame.Instance.isThatCreature(pos.x,pos.y))// 代表有生物
            return false;

        return true;
    }
    #endregion

    #region AI相關

    // 取得AI
    public ICreatureAI GetAI()
    {
        return m_creatureAI;
    }

    // 設定AI
    public void SetAI(ICreatureAI creatureAI)
    {
        m_creatureAI = creatureAI;
    }

    // 更新AI
    public void UpdateAI()
    {
        m_creatureAI.Update();
    }

    #endregion

    #region UI相關

    // HP Bar
    public void SetHPBar(HPBar _hPBar)
    {
        m_hPBar = _hPBar;
    }

    public HPBar GetHPBar()
    {
        return m_hPBar;
    }

    #endregion

    // 死亡
    public void Dead()
    {
        m_creatureSystem.RemoveCreature(this);
        GameObject.Destroy(this.m_GameObject);
    }

    // 建構者
    public ICharacter() { }

    // 周圍生物
    // 判斷周圍是否有生物
    // 檢查周圍相鄰生物的 座標位置(無責傳null)
    public List<Position2D> GetAxisCreaturePos()
    {
        var pos2D = m_creatureSystem.GetAxisCreaturePos(new Position2D(Pos.x, Pos.y));

        if(pos2D.Count == 0)
            return null;

        return pos2D;
    }


    // 判斷 範圍是否有生物 座標位置(無責傳null)
    public List<Position2D> GetRangeCreaturePos(Position2D[] range)
    {
        var pos2D = m_creatureSystem.GetRangeCreaturePos(range);
        if (pos2D.Count == 0)
            return null;

        return pos2D;
    }

    // 取得該座標生物
    public ICharacter GetThatCreature(Position2D pos)
    {
        // 代表有生物
        return HuRougeLikeGame.Instance.GetThatCreature(pos);
    }

}
