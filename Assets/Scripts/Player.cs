using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class Player : MonoBehaviour
{
    private MouseManager m_MouseManager;
    private bool m_PlayerTurn = true;
    private bool m_PlayerMoving = false;
    private IAI m_Brain;
    private Block m_PlayerBlock;
    private float PlayerMovemntSpeed=3f;
    private void Update()
    {
        // Checking BAsic Conditions

        if (!m_PlayerTurn)
            return;
        if (m_PlayerMoving)
            return;
        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;
        if (m_MouseManager.HoveredBlock == null)
            return;

        if (m_MouseManager.HoveredBlock.CharacterPos.position != transform.position)
        {
            // Clicked Block Is Not PLayer Block
            // Start Movement Towards There

            StartCoroutine(MovePlayer(m_Brain.GetPath(m_PlayerBlock, m_MouseManager.HoveredBlock)));
        }
    }
    private IEnumerator MovePlayer(List<Block> Path)
    {
        m_PlayerMoving = true;
        foreach (Block block in Path)
        {
            float timeLapsed = 0;
            transform.LookAt(block.CharacterPos);
            Vector3 LastPos = transform.position;

            // Moveing The Player By Moving Through Path One By One

            while (transform.position - block.CharacterPos.position != Vector3.zero)
            {
                transform.position = Vector3.Lerp(LastPos, block.CharacterPos.position, timeLapsed);
                timeLapsed += Time.fixedDeltaTime * PlayerMovemntSpeed;
                yield return null;
            }
        }
        m_PlayerTurn = false;
        m_PlayerMoving = false;
        if (Path.Count > 0)
        {
            m_PlayerBlock.ChangeOccupied(false);
            m_PlayerBlock = Path[^1];
            m_PlayerBlock.ChangeOccupied(true);
        }
        GameManager.instance.OnPlayerTurnFinnish(m_PlayerBlock);

        // PLayer Has Finished Moving And Its Turn IS Over

    }
    public void AllowPlayerMove()
    {
        // Player Is Again Allowed To Move

        m_PlayerTurn = true;
    }
    public void SetUp(IAI AiUsed, MouseManager MouseManager, Block PlayerBlock)
    {
        m_Brain = AiUsed;
        m_MouseManager = MouseManager;
        m_PlayerBlock= PlayerBlock;
        PlayerBlock.ChangeOccupied(true);
    }
}
