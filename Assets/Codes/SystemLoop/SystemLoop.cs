using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 遊戲整體上的SystemLoop
/// </summary>
public class SystemLoop : MonoBehaviour
{
    // 場景狀態
    GameStateController m_GameStateController = new GameStateController();

    void Awake()
    {
        // 切換場景不會被刪除
        GameObject.DontDestroyOnLoad(this.gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        // 進入菜單State
        m_GameStateController.SetGameState(new GameState_Menu(m_GameStateController));
        
    }

    // Update is called once per frame
    void Update()
    {
        // 調試用(從菜單進世界)
        if (Input.anyKeyDown)
        {
            m_GameStateController.StateRequest();
        }


        // 狀態更新
        m_GameStateController.StateUpdate();
    }
}
