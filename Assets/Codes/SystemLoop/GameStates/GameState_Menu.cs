using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �t�ζ��q:�C���ʭ����
/// </summary>
public class GameState_Menu : IGameState
{
    public GameState_Menu(GameStateController gameStateController) : base(gameStateController)
    {
        LogServise.Log("-�t�ζ��q:�C���ʭ����-");
    }

    public override void Handle()
    {

        m_stateController.SetGameState(new GameState_World(m_stateController));
    }

    // �}�l
    public override void StateBegin()
    {
    }

    // ����
    public override void StateEnd()
    {
    }

    // ��s
    public override void StateUpdate()
    {
    }
}