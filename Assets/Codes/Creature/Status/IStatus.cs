using System.Collections;
using System.Collections.Generic;

public enum ENUM_Status
{
    HumanChildStatus,
}

public abstract class IStatus
{
    public abstract int ATK { get; set; }
    public abstract int HP { get; set; }
    public abstract int HP_Max { get; set; }

    public abstract bool GetDamage(int damage);
}

public class HumanChildStatus : IStatus
{
    ICharacter m_character = null;
    public HumanChildStatus(ICharacter _character)
    {
        m_character = _character;
    }


    private int atk = 10;
    public override int ATK 
    { 
        get => atk; 
        set => atk = value; 
    }

    private int hp = 25;
    public override int HP
    {
        get => hp;
        set
        {
            hp = value;
            m_character.GetHPBar().BarRate = (float)hp/hp_max;
            LogServise.Log("HP: " + hp + "/" + hp_max + " (" + ((float)hp / hp_max)*100 + "%)");

            if(hp<=0)// ­Y¦º¤`
            {
                m_character.IsLive = false;
            }
        } 
    }

    protected int hp_max = 25;
    public override int HP_Max { get => hp_max; set => hp_max = value; }

    public override bool GetDamage(int damage)
    {
        HP -= damage;
        return true;
    }

}
