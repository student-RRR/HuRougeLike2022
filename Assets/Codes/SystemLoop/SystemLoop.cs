using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �C������W��SystemLoop
/// </summary>
public class SystemLoop : MonoBehaviour
{
    // �������A
    GameStateController m_GameStateController = new GameStateController();

    void Awake()
    {
        // �����������|�Q�R��
        GameObject.DontDestroyOnLoad(this.gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        // �i�J���State
        m_GameStateController.SetGameState(new GameState_Menu(m_GameStateController));
        
    }

    // Update is called once per frame
    void Update()
    {
        // �ոե�(�q���i�@��)
        if (Input.anyKeyDown)
        {
            m_GameStateController.StateRequest();
        }


        // ���A��s
        m_GameStateController.StateUpdate();
    }
}
