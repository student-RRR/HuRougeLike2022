using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �t�ζ��q:�@�ɶ��q
/// (�D�C�������q)
/// </summary>
public class GameState_World : IGameState
{


    public GameState_World(GameStateController gameStateController) : base(gameStateController)
    {
        LogServise.Log("-�t�ζ��q:�@�ɶ��q-");
    }

    public override void Handle()
    {

    }

    public override void StateBegin()
    {
        HuRougeLikeGame.Instance.Initialize();


    }

    // ����
    public override void StateEnd()
    {
        HuRougeLikeGame.Instance.Release();
    }

    // ��s
    public override void StateUpdate()
    {
        HuRougeLikeGame.Instance.Update();


    }
}