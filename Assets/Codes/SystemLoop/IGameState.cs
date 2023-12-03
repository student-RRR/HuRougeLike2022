public abstract class IGameState
{
    protected GameStateController m_stateController = null;

    public IGameState(GameStateController _gameStateController)
    {
        m_stateController = _gameStateController;
    }

    public abstract void Handle();

    // �}�l
    public abstract void StateBegin();

    // ����
    public abstract void StateEnd();

    // ��s
    public abstract void StateUpdate();
}


