using System.Collections;
using System.Collections.Generic;

public enum ENUM_Body
{
    Humanoid,
}

/// <summary>
/// ����
/// </summary>
public abstract class IBody
{
    public List<ILimb> m_ilimbs = null;


}

/// <summary>
/// �H��
/// </summary>
public class Humanoid : IBody
{

}