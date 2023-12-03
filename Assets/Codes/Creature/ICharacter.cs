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
/// �ͪ��ɭ�
/// </summary>
public abstract class ICharacter
{
    public CreatureSystem m_creatureSystem;
    public ICharacter(CreatureSystem _creatureSystem)
    {
        m_creatureSystem = _creatureSystem;
    }

    // �Ѽ�
    protected int id { get; set; }                    // ID
    protected string name { get; set; }               // �W�r        
    protected int LV { get; set; }                    // ����
    protected GameObject m_GameObject { get; set; }   // Uniyt�ҫ�    
    protected IBody m_body { get; set; }              // ����    
    protected IStatus m_status { get; set; }          // ��O��     
    protected ICreatureAI m_creatureAI { get; set; }  // AI
    protected HPBar m_hPBar { get; set; }             // HP Bar    

    private bool islive;                              // �s���P�_
    public bool IsLive 
    {
        get => islive;
        set 
        {
            if(islive == true && value == false) // �Y�ѥ��঺
            {
                this.Dead();
            }
            islive = value;
        }
    }               

    #region ��m�y��

    // ��m�y��
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

            m_creatureSystem.Refleshfmap(); // ��s�ͪ��a��
        }
    }

    #endregion

    #region �򥻸��

    // ���o�W�r
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

    #region Unity�ҫ�

    // �]�wUnity�ҫ�
    public void SetGameObject(GameObject theGameObject)
    {
        m_GameObject = theGameObject;
    }

    // ���oUnity�ҫ�
    public GameObject GetGameObject()
    {
        return m_GameObject;
    }
    #endregion

    #region ���ʬ���
    // ���ե�(����)
    public bool MoveDirection(Position2D _pos)
    {
        // �P�_�O�_�ಾ��
        if (!moveAble(_pos))
            return false;

        //LogServise.Log("[" + name + "] ���ʦ�:(" + _pos.x + "," + _pos.y + ")");
        Pos = _pos;
        return true;
    }

    private bool moveAble(Position2D pos)
    {
        //MapServise
        var wallMap = HuRougeLikeGame.Instance.GetWallMap();
        //var creaturesList = HuRougeLikeGame.Instance.GetCreaturesList();

        if (wallMap[pos.x, pos.y].wallType != null)// �N����
            return false;

        if (HuRougeLikeGame.Instance.isThatCreature(pos.x,pos.y))// �N���ͪ�
            return false;

        return true;
    }
    #endregion

    #region AI����

    // ���oAI
    public ICreatureAI GetAI()
    {
        return m_creatureAI;
    }

    // �]�wAI
    public void SetAI(ICreatureAI creatureAI)
    {
        m_creatureAI = creatureAI;
    }

    // ��sAI
    public void UpdateAI()
    {
        m_creatureAI.Update();
    }

    #endregion

    #region UI����

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

    // ���`
    public void Dead()
    {
        m_creatureSystem.RemoveCreature(this);
        GameObject.Destroy(this.m_GameObject);
    }

    // �غc��
    public ICharacter() { }

    // �P��ͪ�
    // �P�_�P��O�_���ͪ�
    // �ˬd�P��۾F�ͪ��� �y�Ц�m(�L�d��null)
    public List<Position2D> GetAxisCreaturePos()
    {
        var pos2D = m_creatureSystem.GetAxisCreaturePos(new Position2D(Pos.x, Pos.y));

        if(pos2D.Count == 0)
            return null;

        return pos2D;
    }


    // �P�_ �d��O�_���ͪ� �y�Ц�m(�L�d��null)
    public List<Position2D> GetRangeCreaturePos(Position2D[] range)
    {
        var pos2D = m_creatureSystem.GetRangeCreaturePos(range);
        if (pos2D.Count == 0)
            return null;

        return pos2D;
    }

    // ���o�Ӯy�Хͪ�
    public ICharacter GetThatCreature(Position2D pos)
    {
        // �N���ͪ�
        return HuRougeLikeGame.Instance.GetThatCreature(pos);
    }

}
