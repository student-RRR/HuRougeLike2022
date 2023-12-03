using System.Collections;
using System.Collections.Generic;
using System;
public abstract class IAIState
{
    // 生物AI
    protected ICreatureAI m_creatureAI = null;

    // 建構
    public IAIState()
    {

    }

    // 設定狀態擁有者
    public void SetCreatureAI(ICreatureAI _creatureAI)
    {
        m_creatureAI = _creatureAI;
    }

    public abstract void Update();
}

/// <summary>
/// 狀態:漫步
/// </summary>
public class WanderAIState : IAIState
{
    public override void Update()
    {
        //LogServise.Log("wish wandering~");
        m_creatureAI.Wander();
    }
}

/// <summary>
/// 狀態:不作為
/// </summary>
public class DoNothingAIState : IAIState
{
    public override void Update()
    {
        //LogServise.Log("wish DoNothing~");
    }
}

/// <summary>
/// 狀態:埋伏
/// </summary>
public class AmbushState : IAIState
{
    public override void Update()
    {


        // 若相鄰, 則攻擊
        List<Position2D> positions = m_creatureAI.GetAxisCreaturePos();
        if (positions != null)
            foreach (var pos in positions)
            {
                m_creatureAI.Attack(pos);
                return;
            }

        // 定義"周圍"
        Position2D[] range = RangeMathf.GetScopeRange(m_creatureAI.GetPos(),3);
        // 感測範圍餒是否有生物
        if (m_creatureAI.GetRangeCreaturePos(range) != null) // 若有生物
        {
            LogServise.Log("進入追擊模式");
            m_creatureAI.ChangeAIState(new HuntState());
            // 進追擊模式
        }
    }
}

/// <summary>
/// 狀態:追擊
/// </summary>
public class HuntState : IAIState
{
    public override void Update()
    {

        // 定義"周圍"
        Position2D[] range = RangeMathf.GetScopeRange(m_creatureAI.GetPos(), 4);
        // 取得範圍內生物列表
        List<Position2D> characterList = m_creatureAI.GetRangeCreaturePos(range);

        // 感測範圍餒是否有生物
        if (m_creatureAI.GetRangeCreaturePos(range) == null) // 若沒有生物
        {
            LogServise.Log("進入埋伏模式");
            m_creatureAI.ChangeAIState(new AmbushState());
            // 進埋伏模式
        }
        else // 若有生物
        {
            // 鎖定生物
            m_creatureAI.mainEnmy = m_creatureAI.GetThatCreature(characterList[0]);
        }


        // 追擊

        // 若存在目標, 且目標存活
        if (m_creatureAI.mainEnmy != null && m_creatureAI.mainEnmy.IsLive)
        {
            // 取得範圍內的攻擊目標(如果有的話)
            List<Position2D> targetPositions = m_creatureAI.GetRangeThatCreaturePos(RangeMathf.Get8MazeRange(m_creatureAI.GetPos()), m_creatureAI.mainEnmy.GetID());
            // 若目標在攻擊範圍內
            if (targetPositions != null)
            {
                // 攻擊
                m_creatureAI.Attack(targetPositions[0]);
            }
            else
            {
                // 移動
                m_creatureAI.MoveToPos(m_creatureAI.mainEnmy.Pos);
            } 
        }
        else
        {
            LogServise.Log("m_creatureAI.mainEnmy == null");
            return;
        }





    }
}

/// <summary>
/// 狀態:攻擊
/// (攻擊周圍的指定目標)
/// </summary>
public class AttackState : IAIState
{
    public override void Update()
    {
        //// 走訪所有方位
        //foreach(Direction direct in Enum.GetValues(typeof(Direction)))
        //{
        //    // 走訪所有方位 的 座標向量值
        //    var dValue = DirectionValue.theDrect(direct);


        //}
        List<Position2D> positions = m_creatureAI.GetAxisCreaturePos();
        if (positions == null)
            return;

        foreach(var pos in positions)
        {
            m_creatureAI.Attack(pos);
        }
    }
}