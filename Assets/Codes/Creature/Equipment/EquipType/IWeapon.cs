using System.Collections;
using System.Collections.Generic;


public interface IWeapon : IEquipment
{

}

/// <summary>
/// �C
/// </summary>
public abstract class Sword : IWeapon
{
    public ITemporalAttr m_EquipAttr { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}

/// <summary>
/// ��
/// </summary>
public abstract class wand : IWeapon
{
    public ITemporalAttr m_EquipAttr { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}
