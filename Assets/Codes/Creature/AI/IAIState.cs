using System.Collections;
using System.Collections.Generic;
using System;
public abstract class IAIState
{
    // �ͪ�AI
    protected ICreatureAI m_creatureAI = null;

    // �غc
    public IAIState()
    {

    }

    // �]�w���A�֦���
    public void SetCreatureAI(ICreatureAI _creatureAI)
    {
        m_creatureAI = _creatureAI;
    }

    public abstract void Update();
}

/// <summary>
/// ���A:���B
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
/// ���A:���@��
/// </summary>
public class DoNothingAIState : IAIState
{
    public override void Update()
    {
        //LogServise.Log("wish DoNothing~");
    }
}

/// <summary>
/// ���A:�I��
/// </summary>
public class AmbushState : IAIState
{
    public override void Update()
    {


        // �Y�۾F, �h����
        List<Position2D> positions = m_creatureAI.GetAxisCreaturePos();
        if (positions != null)
            foreach (var pos in positions)
            {
                m_creatureAI.Attack(pos);
                return;
            }

        // �w�q"�P��"
        Position2D[] range = RangeMathf.GetScopeRange(m_creatureAI.GetPos(),3);
        // �P���d��k�O�_���ͪ�
        if (m_creatureAI.GetRangeCreaturePos(range) != null) // �Y���ͪ�
        {
            LogServise.Log("�i�J�l���Ҧ�");
            m_creatureAI.ChangeAIState(new HuntState());
            // �i�l���Ҧ�
        }
    }
}

/// <summary>
/// ���A:�l��
/// </summary>
public class HuntState : IAIState
{
    public override void Update()
    {

        // �w�q"�P��"
        Position2D[] range = RangeMathf.GetScopeRange(m_creatureAI.GetPos(), 4);
        // ���o�d�򤺥ͪ��C��
        List<Position2D> characterList = m_creatureAI.GetRangeCreaturePos(range);

        // �P���d��k�O�_���ͪ�
        if (m_creatureAI.GetRangeCreaturePos(range) == null) // �Y�S���ͪ�
        {
            LogServise.Log("�i�J�I��Ҧ�");
            m_creatureAI.ChangeAIState(new AmbushState());
            // �i�I��Ҧ�
        }
        else // �Y���ͪ�
        {
            // ��w�ͪ�
            m_creatureAI.mainEnmy = m_creatureAI.GetThatCreature(characterList[0]);
        }


        // �l��

        // �Y�s�b�ؼ�, �B�ؼЦs��
        if (m_creatureAI.mainEnmy != null && m_creatureAI.mainEnmy.IsLive)
        {
            // ���o�d�򤺪������ؼ�(�p�G������)
            List<Position2D> targetPositions = m_creatureAI.GetRangeThatCreaturePos(RangeMathf.Get8MazeRange(m_creatureAI.GetPos()), m_creatureAI.mainEnmy.GetID());
            // �Y�ؼЦb�����d��
            if (targetPositions != null)
            {
                // ����
                m_creatureAI.Attack(targetPositions[0]);
            }
            else
            {
                // ����
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
/// ���A:����
/// (�����P�򪺫��w�ؼ�)
/// </summary>
public class AttackState : IAIState
{
    public override void Update()
    {
        //// ���X�Ҧ����
        //foreach(Direction direct in Enum.GetValues(typeof(Direction)))
        //{
        //    // ���X�Ҧ���� �� �y�ЦV�q��
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