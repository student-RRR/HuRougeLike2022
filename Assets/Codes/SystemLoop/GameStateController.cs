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
        LogServise.Log("GameStateController�o�X����State�ШD");
        m_gameState.Handle();
    }

    public void StateUpdate()
    {
        // �Y������
        if (!isNotBegin && m_gameState != null)
        {
            m_gameState.StateBegin();
            isNotBegin = true;
        }

        // State��s
        if (m_gameState != null)
            m_gameState.StateUpdate();
    }
}
