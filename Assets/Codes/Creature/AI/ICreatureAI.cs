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
/// AI���`���ê�
/// </summary>
public abstract class ICreatureAI
{
    // ���A�����H(�ͪ�)
    protected ICharacter m_character = null;

    // ��U���A(���A)
    protected IAIState m_AIState = null;

    // �غc
    public ICreatureAI(ICharacter _creature)
    {
        m_character = _creature;
    }

    // ����AI���A
    public virtual void ChangeAIState(IAIState NewAIState)
    {
        m_AIState = NewAIState;
        m_AIState.SetCreatureAI(this);
    }


    // ���b�l��
    public bool isHunt = false;

    // �F���ĤH
    public bool isAxisEnmy = false;

    // ��e�ķN�ؼ�
    public ICharacter mainEnmy;


    // ����
    public bool Attack(Position2D position)
    {
        // �S��
        EffectSystem.Effect(new IEffect(ENUM_Effect.attack), m_character.Pos, position);

        // �ˮ`�p��
        var atk = m_character.GetState().ATK;

        // ���o�Ӯ� �ͪ�ID
        var creId = HuRougeLikeGame.Instance.GetThatCreatureID(position);
        var cre = HuRougeLikeGame.Instance.GetCharacterByID(creId);

        var temphp = cre.GetState().HP;

        cre.GetState().GetDamage(atk);

        LogServise.Log("[" + m_character.GetName() + "]" + " ���� [" + cre.GetName() + "]  (dmg:" + atk + ")  HP " + temphp + "->" + cre.GetState().HP);
        return true;
    }
    
    // �E�i���첾���(?)
    public bool March(Direction direction)
    {
        //MapServise ���o��a��
        var wallMap = HuRougeLikeGame.Instance.GetWallMap();

        var creaturesList = HuRougeLikeGame.Instance.GetCreaturesList();


        bool isCreature = false;  // �O�_���ͪ�
        bool isWall = false;      // �O�_����

        int posX = m_character.Pos.x;
        int posY = m_character.Pos.y;

        int thatX = posX + DirectionValue.theDrect(direction).x;
        int thatY = posY + DirectionValue.theDrect(direction).y;

        Position2D targPos = new Position2D(thatX, thatY);

        // �P�_�Ӥ�V�F��O�_�� ��
        if (wallMap[thatX, thatY].wallType != null)// �N����
            isWall = true;

        // �P�_�Ӥ�V�F��O�_�� �ͪ�
        //????????????
        if (HuRougeLikeGame.Instance.isThatCreature(thatX, thatY))
            isCreature = true;

        // �i��E�i�첾
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

    #region ����
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


    // �V�W��
    private bool MoveUp()
    {
        m_character.MoveDirection(new Position2D(m_character.Pos.x,
                                                 m_character.Pos.y + 1));
        return true;
    }


    // �V�U��
    private bool MoveDown()
    {
        return m_character.MoveDirection(new Position2D(m_character.Pos.x,
                                                 m_character.Pos.y - 1));
    }


    // �V����
    private bool MoveLeft()
    {
        return m_character.MoveDirection(new Position2D(m_character.Pos.x - 1,
                                                 m_character.Pos.y));
    }


    // �V�k��
    private bool MoveRight()
    {
        return m_character.MoveDirection(new Position2D(m_character.Pos.x + 1,
                                                 m_character.Pos.y));
    }


    // �è�
    public bool Wander()
    {
        Random myObject = new Random();
        return m_character.MoveDirection(new Position2D(m_character.Pos.x + myObject.Next(-1, 2), 
                                                 m_character.Pos.y + myObject.Next(-1, 2)));
    }

    // �a��Ӯy�Ц�m
    public bool MoveToPos(Position2D tPos)
    {
        int tempPosX = CalculateMathf.AbsoluteValue(tPos.x - m_character.Pos.x);
        int tempPosY = CalculateMathf.AbsoluteValue(tPos.y - m_character.Pos.y);

        Position2D deltaPos = new Position2D(tempPosX, tempPosY);

        // �M�w������Y�b��X�b
        if (deltaPos.x > deltaPos.y)
        {
            // ��X�b
            // �M�w��V
            if (tPos.x > m_character.Pos.x)
                MoveRight();
            else
                MoveLeft();
        }
        else
        {
            // ��Y�b
            // �M�w��V
            if (tPos.y > m_character.Pos.y)
                MoveUp();
            else
                MoveDown();
        }

        return true;
    }

    #endregion

    // �ˬd�P��۾F�ͪ��� �y�Ц�m(�L�d��null)
    public List<Position2D> GetAxisCreaturePos()
    {
        return m_character.GetAxisCreaturePos();
    }

    // ���o�d�򤺩Ҧ��ͪ��y��
    public List<Position2D> GetRangeCreaturePos(Position2D[] range)
    {
        return m_character.GetRangeCreaturePos(range);
    }

    // ���o�ۨ��y�Ц�m
    public Position2D GetPos()
    {
        return m_character.Pos;
    }

    // ���o�ؼЮy�Ъ��ͪ�
    public ICharacter GetThatCreature(Position2D pos)
    {
        return m_character.GetThatCreature(pos);
    }

    // �ˬd�d�򤺬O�_�s�b�ӥؼ�id
    public List<Position2D> GetRangeThatCreaturePos(Position2D[] range, int id)
    {
        List<Position2D> tempPos = new List<Position2D>();
        foreach (var r in range)
        {
            // �s�b�ӥؼ�ID
            if (m_character.m_creatureSystem.GetCield(r.x, r.y).creature_ID == id)
            {
                tempPos.Add(r);
            }
        }

        if (tempPos.Count == 0)
            return null;
        return tempPos;
    }

    // ��sAI
    public void Update()
    {
        m_AIState.Update();
    }
}

/// <summary>
/// AI:�H��
/// </summary>
public class HumanAI : ICreatureAI
{
    // �غc
    public HumanAI(ICharacter creature):base (creature)
    {
        // ��l���A
        ChangeAIState(new DoNothingAIState());
    }
}

/// <summary>
/// AI:���C��
/// </summary>
public class WandererAI : ICreatureAI
{
    // �غc
    public WandererAI(ICharacter creature) : base(creature)
    {
        // ��l���A
        ChangeAIState(new WanderAIState());
    }
}

/// <summary>
/// AI:�y�H
/// </summary>
public class HunterAI : ICreatureAI
{
    // �غc
    public HunterAI(ICharacter creature) : base(creature)
    {
        // ��l���A-�I��
        ChangeAIState(new AmbushState());


    }



}
