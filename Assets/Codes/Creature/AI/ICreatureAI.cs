using System;
using System.Collections;
using System.Collections.Generic;

public enum ENUM_CreatureAI
{
    HumanAI,
    WandererAI,
    HunterAI
}

/// <summary>
/// AI的總收藏者
/// </summary>
public abstract class ICreatureAI
{
    // 狀態持有人(生物)
    protected ICharacter m_character = null;

    // 當下狀態(狀態)
    protected IAIState m_AIState = null;

    // 建構
    public ICreatureAI(ICharacter _creature)
    {
        m_character = _creature;
    }

    // 切換AI狀態
    public virtual void ChangeAIState(IAIState NewAIState)
    {
        m_AIState = NewAIState;
        m_AIState.SetCreatureAI(this);
    }


    // 正在追擊
    public bool isHunt = false;

    // 鄰接敵人
    public bool isAxisEnmy = false;

    // 當前敵意目標
    public ICharacter mainEnmy;


    // 攻擊
    public bool Attack(Position2D position)
    {
        // 特效
        EffectSystem.Effect(new IEffect(ENUM_Effect.attack), m_character.Pos, position);

        // 傷害計算
        var atk = m_character.GetState().ATK;

        // 取得該格 生物ID
        var creId = HuRougeLikeGame.Instance.GetThatCreatureID(position);
        var cre = HuRougeLikeGame.Instance.GetCharacterByID(creId);

        var temphp = cre.GetState().HP;

        cre.GetState().GetDamage(atk);

        LogServise.Log("[" + m_character.GetName() + "]" + " 攻擊 [" + cre.GetName() + "]  (dmg:" + atk + ")  HP " + temphp + "->" + cre.GetState().HP);
        return true;
    }
    
    // 激進的位移行動(?)
    public bool March(Direction direction)
    {
        //MapServise 取得牆地圖
        var wallMap = HuRougeLikeGame.Instance.GetWallMap();

        var creaturesList = HuRougeLikeGame.Instance.GetCreaturesList();


        bool isCreature = false;  // 是否為生物
        bool isWall = false;      // 是否為牆

        int posX = m_character.Pos.x;
        int posY = m_character.Pos.y;

        int thatX = posX + DirectionValue.theDrect(direction).x;
        int thatY = posY + DirectionValue.theDrect(direction).y;

        Position2D targPos = new Position2D(thatX, thatY);

        // 判斷該方向鄰格是否有 牆
        if (wallMap[thatX, thatY].wallType != null)// 代表有牆
            isWall = true;

        // 判斷該方向鄰格是否有 生物
        //????????????
        if (HuRougeLikeGame.Instance.isThatCreature(thatX, thatY))
            isCreature = true;

        // 進行激進位移
        switch (direction)
        {
            case Direction.Up:
                {
                    if (isCreature) { Attack(targPos); break; }
                    if (!isWall) { MoveUp(); break; }
                    break;
                }
            case Direction.Down:
                {
                    if (isCreature) { Attack(targPos); break; }
                    if (!isWall) { MoveDown(); break; }
                    break;
                }
            case Direction.Left:
                {
                    if (isCreature) { Attack(targPos); break; }
                    if (!isWall) { MoveLeft(); break; }
                    break;
                }
            case Direction.Right:
                {
                    if (isCreature) { Attack(targPos); break; }
                    if (!isWall) { MoveRight(); break; }
                    break;
                }
            default:
                {
                    break;
                }
        }

        return true;
    }

    #region 移動
    public bool MoveTo(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                {
                    MoveUp();
                    break;
                }
            case Direction.Down:
                {
                    MoveDown();
                    break;
                }
            case Direction.Left:
                {
                    MoveLeft();
                    break;
                }
            case Direction.Right:
                {
                    MoveRight();
                    break;
                }
            default:
                {                    
                    break;
                }
        }

        return true;
    }


    // 向上走
    private bool MoveUp()
    {
        m_character.MoveDirection(new Position2D(m_character.Pos.x,
                                                 m_character.Pos.y + 1));
        return true;
    }


    // 向下走
    private bool MoveDown()
    {
        return m_character.MoveDirection(new Position2D(m_character.Pos.x,
                                                 m_character.Pos.y - 1));
    }


    // 向左走
    private bool MoveLeft()
    {
        return m_character.MoveDirection(new Position2D(m_character.Pos.x - 1,
                                                 m_character.Pos.y));
    }


    // 向右走
    private bool MoveRight()
    {
        return m_character.MoveDirection(new Position2D(m_character.Pos.x + 1,
                                                 m_character.Pos.y));
    }


    // 亂走
    public bool Wander()
    {
        Random myObject = new Random();
        return m_character.MoveDirection(new Position2D(m_character.Pos.x + myObject.Next(-1, 2), 
                                                 m_character.Pos.y + myObject.Next(-1, 2)));
    }

    // 靠近該座標位置
    public bool MoveToPos(Position2D tPos)
    {
        int tempPosX = CalculateMathf.AbsoluteValue(tPos.x - m_character.Pos.x);
        int tempPosY = CalculateMathf.AbsoluteValue(tPos.y - m_character.Pos.y);

        Position2D deltaPos = new Position2D(tempPosX, tempPosY);

        // 決定先移動Y軸或X軸
        if (deltaPos.x > deltaPos.y)
        {
            // 動X軸
            // 決定方向
            if (tPos.x > m_character.Pos.x)
                MoveRight();
            else
                MoveLeft();
        }
        else
        {
            // 動Y軸
            // 決定方向
            if (tPos.y > m_character.Pos.y)
                MoveUp();
            else
                MoveDown();
        }

        return true;
    }

    #endregion

    // 檢查周圍相鄰生物的 座標位置(無責傳null)
    public List<Position2D> GetAxisCreaturePos()
    {
        return m_character.GetAxisCreaturePos();
    }

    // 取得範圍內所有生物座標
    public List<Position2D> GetRangeCreaturePos(Position2D[] range)
    {
        return m_character.GetRangeCreaturePos(range);
    }

    // 取得自身座標位置
    public Position2D GetPos()
    {
        return m_character.Pos;
    }

    // 取得目標座標的生物
    public ICharacter GetThatCreature(Position2D pos)
    {
        return m_character.GetThatCreature(pos);
    }

    // 檢查範圍內是否存在該目標id
    public List<Position2D> GetRangeThatCreaturePos(Position2D[] range, int id)
    {
        List<Position2D> tempPos = new List<Position2D>();
        foreach (var r in range)
        {
            // 存在該目標ID
            if (m_character.m_creatureSystem.GetCield(r.x, r.y).creature_ID == id)
            {
                tempPos.Add(r);
            }
        }

        if (tempPos.Count == 0)
            return null;
        return tempPos;
    }

    // 更新AI
    public void Update()
    {
        m_AIState.Update();
    }
}

/// <summary>
/// AI:人類
/// </summary>
public class HumanAI : ICreatureAI
{
    // 建構
    public HumanAI(ICharacter creature):base (creature)
    {
        // 初始狀態
        ChangeAIState(new DoNothingAIState());
    }
}

/// <summary>
/// AI:漫遊者
/// </summary>
public class WandererAI : ICreatureAI
{
    // 建構
    public WandererAI(ICharacter creature) : base(creature)
    {
        // 初始狀態
        ChangeAIState(new WanderAIState());
    }
}

/// <summary>
/// AI:獵人
/// </summary>
public class HunterAI : ICreatureAI
{
    // 建構
    public HunterAI(ICharacter creature) : base(creature)
    {
        // 初始狀態-埋伏
        ChangeAIState(new AmbushState());


    }



}
