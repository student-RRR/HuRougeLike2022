using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ����
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
/// �Y/��l
/// </summary>
public class Head : ILimb
{

}

/// <summary>
/// �W�b��/�I��
/// </summary>
public class Chest : ILimb
{

}

/// <summary>
/// �v��/�y
/// </summary>
public class Haunch : ILimb
{

}

/// <summary>
/// ��/��� 
/// </summary>
public class Hand : ILimb
{
    public Hand()
    {
        // ��i�H���Z��
        canWeapon = true;
    }

}

/// <summary>
/// ��/�L
/// </summary>
public class Foot : ILimb
{

}