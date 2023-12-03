public abstract class IGameState
{
    protected GameStateController m_stateController = null;

    public IGameState(GameStateController _gameStateController)
    {
        m_stateController = _gameStateController;
    }

    public abstract void Handle();

    // 開始
    public abstract void StateBegin();

    // 結束
    public abstract void StateEnd();

    // 更新
    public abstract void StateUpdate();
}


