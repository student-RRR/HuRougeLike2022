using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 系統階段:世界階段
/// (主遊戲的階段)
/// </summary>
public class GameState_World : IGameState
{


    public GameState_World(GameStateController gameStateController) : base(gameStateController)
    {
        LogServise.Log("-系統階段:世界階段-");
    }

    public override void Handle()
    {

    }

    public override void StateBegin()
    {
        HuRougeLikeGame.Instance.Initialize();


    }

    // 結束
    public override void StateEnd()
    {
        HuRougeLikeGame.Instance.Release();
    }

    // 更新
    public override void StateUpdate()
    {
        HuRougeLikeGame.Instance.Update();


    }
}