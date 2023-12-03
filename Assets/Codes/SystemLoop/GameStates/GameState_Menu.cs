using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 系統階段:遊戲封面菜單
/// </summary>
public class GameState_Menu : IGameState
{
    public GameState_Menu(GameStateController gameStateController) : base(gameStateController)
    {
        LogServise.Log("-系統階段:遊戲封面菜單-");
    }

    public override void Handle()
    {

        m_stateController.SetGameState(new GameState_World(m_stateController));
    }

    // 開始
    public override void StateBegin()
    {
    }

    // 結束
    public override void StateEnd()
    {
    }

    // 更新
    public override void StateUpdate()
    {
    }
}