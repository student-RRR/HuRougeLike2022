using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ����SCGPS
/// �U�l, ��A, ��M, �Ǥl, �c�l
/// </summary>
public interface IArmor: IEquipment
{

}

/// <summary>
/// �U�l
/// </summary>
public abstract class Hat : IArmor
{
    public ITemporalAttr m_EquipAttr { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}

/// <summary>
/// ��A
/// </summary>
public abstract class Cloth : IArmor
{
    public ITemporalAttr m_EquipAttr { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}

/// <summary>
/// ��M
/// </summary>
public abstract class Glove : IArmor
{
    public ITemporalAttr m_EquipAttr { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}

/// <summary>
/// �Ǥl
/// </summary>
public abstract class Pants : IArmor
{
    public ITemporalAttr m_EquipAttr { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}

/// <summary>
/// �c�l
/// </summary>
public abstract class Shoe : IArmor
{
    public ITemporalAttr m_EquipAttr { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}
