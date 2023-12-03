using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 防具SCGPS
/// 帽子, 衣服, 手套, 褲子, 鞋子
/// </summary>
public interface IArmor: IEquipment
{

}

/// <summary>
/// 帽子
/// </summary>
public abstract class Hat : IArmor
{
    public ITemporalAttr m_EquipAttr { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}

/// <summary>
/// 衣服
/// </summary>
public abstract class Cloth : IArmor
{
    public ITemporalAttr m_EquipAttr { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}

/// <summary>
/// 手套
/// </summary>
public abstract class Glove : IArmor
{
    public ITemporalAttr m_EquipAttr { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}

/// <summary>
/// 褲子
/// </summary>
public abstract class Pants : IArmor
{
    public ITemporalAttr m_EquipAttr { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}

/// <summary>
/// 鞋子
/// </summary>
public abstract class Shoe : IArmor
{
    public ITemporalAttr m_EquipAttr { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}
