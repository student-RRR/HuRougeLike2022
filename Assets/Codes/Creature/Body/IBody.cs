using System.Collections;
using System.Collections.Generic;

public enum ENUM_Body
{
    Humanoid,
}

/// <summary>
/// 身形
/// </summary>
public abstract class IBody
{
    public List<ILimb> m_ilimbs = null;


}

/// <summary>
/// 人形
/// </summary>
public class Humanoid : IBody
{

}