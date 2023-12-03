public static class HuRougeLike2022Factory
{
    private static ICreatureFactory m_creatureFactory = null; // �ͪ��u�t
    private static IAssetFactory m_assetFactory = null; // �����u�t
    //private static IEffectFactory m_effactFactory = null; // �S�Ĥu�t

    public static ICreatureFactory GetCreatureFactory()
    {
        if (m_creatureFactory == null)
            m_creatureFactory = new CreatureFactory();

        return m_creatureFactory;
    }

    public static IAssetFactory GetAssetFactory()
    {
        if (m_assetFactory == null)
            m_assetFactory = new AssetFactory();

        return m_assetFactory;
    }

    //public static IEffectFactory GetEffectFactory()
    //{
    //    if (m_effactFactory == null)
    //        m_effactFactory = new EffectFactory();

    //    return m_effactFactory;
    //}
}