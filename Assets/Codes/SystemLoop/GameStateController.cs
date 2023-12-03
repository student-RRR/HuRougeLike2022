public class GameStateController
{
    IGameState m_gameState = null;

    protected bool isNotBegin = false;


    public void SetGameState(IGameState theState)
    {
        isNotBegin = false;

        if (m_gameState != null)
            m_gameState.StateEnd();


        m_gameState = theState;
    }

    public void StateRequest()
    {
        LogServise.Log("GameStateController發出切換State請求");
        m_gameState.Handle();
    }

    public void StateUpdate()
    {
        // 若為首次
        if (!isNotBegin && m_gameState != null)
        {
            m_gameState.StateBegin();
            isNotBegin = true;
        }

        // State更新
        if (m_gameState != null)
            m_gameState.StateUpdate();
    }
}
