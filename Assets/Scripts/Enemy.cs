using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class Enemy : MonoBehaviour
{
    private IAI m_Brain;
    private Block m_EnemyBlock;
    private float enemyMovemntSpeed = 2;
    private IEnumerator MoveEnemy(List<Block> Path,Block Player)
    {
        // Currently We Could Sort Of Combine Player ANd Enemy Into one Parent And One child Class 
        // But That may Be Changed Depending On The Complexity Of Game

        foreach (Block block in Path)
        {
            float timeLapsed = 0;
            transform.LookAt(block.CharacterPos);
            Vector3 LastPos = transform.position;

            // Moveing The Enemy By Moving Through Path One By One

            while (transform.position - block.CharacterPos.position != Vector3.zero)
            {
                transform.position = Vector3.Lerp(LastPos, block.CharacterPos.position, timeLapsed);
                timeLapsed += Time.fixedDeltaTime * enemyMovemntSpeed;
                yield return null;
            }
        }
        transform.LookAt(Player.CharacterPos);
        if (Path.Count > 0)
        {
            m_EnemyBlock.ChangeOccupied(false);
            m_EnemyBlock = Path[^1];
            m_EnemyBlock.ChangeOccupied(true);
            
        }
        GameManager.instance.OnEnemyTurnFinnish();

        // Enemy Has Finished Moving And Its Turn IS Over
    }
    public void OnMove(Block PlayerBlock)
    {
        // Enemy Has To Move Now

        StartCoroutine(MoveEnemy(m_Brain.GetPath(m_EnemyBlock, PlayerBlock),PlayerBlock));
    }
    public void SetUp(IAI AiUsed, Block Enemyblock)
    {
        m_Brain = AiUsed;
        m_EnemyBlock = Enemyblock;
        Enemyblock.ChangeOccupied(true);
    }
}
