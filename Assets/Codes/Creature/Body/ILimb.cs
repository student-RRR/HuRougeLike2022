using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 肢體
/// </summary>
public abstract class ILimb
{
    public IArmor m_armor = null;
    public bool canArmor = true;

    public IAccessories m_accessories = null;
    public bool canAccessories =true;

    public IWeapon m_weapon = null;
    public bool canWeapon = false;

}

/// <summary>
/// 頭/脖子
/// </summary>
public class Head : ILimb
{

}

/// <summary>
/// 上半身/背部
/// </summary>
public class Chest : ILimb
{

}

/// <summary>
/// 臀部/腰
/// </summary>
public class Haunch : ILimb
{

}

/// <summary>
/// 手/手指 
/// </summary>
public class Hand : ILimb
{
    public Hand()
    {
        // 手可以拿武器
        canWeapon = true;
    }

}

/// <summary>
/// 足/腿
/// </summary>
public class Foot : ILimb
{

}