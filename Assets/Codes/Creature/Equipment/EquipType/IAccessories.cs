using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 飾品
/// 項鍊, 披風, 戒指, 腰帶, 襪子
/// </summary>
public interface IAccessories : IEquipment
{

}

/// <summary>
/// 項鍊
/// </summary>
public abstract class necklace : IAccessories
{
    public ITemporalAttr m_EquipAttr { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}

/// <summary>
/// 披風
/// </summary>
public abstract class cloak : IAccessories
{
    public ITemporalAttr m_EquipAttr { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}

/// <summary>
/// 戒指
/// </summary>
public abstract class ring : IAccessories
{
    public ITemporalAttr m_EquipAttr { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}

/// <summary>
/// 腰帶
/// </summary>
public abstract class belt : IAccessories
{
    public ITemporalAttr m_EquipAttr { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}

/// <summary>
/// 襪子
/// </summary>
public abstract class sock : IAccessories
{
    public ITemporalAttr m_EquipAttr { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}
