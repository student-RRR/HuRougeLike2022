using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ���~
/// ����, �ܭ�, �٫�, �y�a, ���l
/// </summary>
public interface IAccessories : IEquipment
{

}

/// <summary>
/// ����
/// </summary>
public abstract class necklace : IAccessories
{
    public ITemporalAttr m_EquipAttr { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}

/// <summary>
/// �ܭ�
/// </summary>
public abstract class cloak : IAccessories
{
    public ITemporalAttr m_EquipAttr { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}

/// <summary>
/// �٫�
/// </summary>
public abstract class ring : IAccessories
{
    public ITemporalAttr m_EquipAttr { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}

/// <summary>
/// �y�a
/// </summary>
public abstract class belt : IAccessories
{
    public ITemporalAttr m_EquipAttr { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}

/// <summary>
/// ���l
/// </summary>
public abstract class sock : IAccessories
{
    public ITemporalAttr m_EquipAttr { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}
