using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    public Transform startPoint;
   
    public int botAmount;
    public int aliveBot;
    public int maxBotOnGround;
    public int spawnedBotAmount;

    public List<Character> enemys = new List<Character>();
    
    public int currentBotAmount => enemys.Count;

    [SerializeField] private List<Transform> startPoints_Bot = new List<Transform>();
    [SerializeField] private NavMeshData navMeshData;


    public void Update()
    {
        if (GameManager.Ins.IsState(GameState.GamePlay))
        {
            if (currentBotAmount < maxBotOnGround && spawnedBotAmount < botAmount)
            {
                SpawnNewBot();
            }
        }
        
    }
    public void OnInit()
    {
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(navMeshData);

        spawnedBotAmount = 0;
        for (int i = 0; i < maxBotOnGround; i++)
        {
            SpawnNewBot();
        }
        aliveBot = botAmount;
    }
    
    public void SpawnNewBot()
    {
        Enemy enemy = SimplePool.Spawn<Enemy>(PoolType.Bot, startPoints_Bot[Random.Range(0, startPoints_Bot.Count)].position , Quaternion.identity);
        spawnedBotAmount += 1;
        enemy.OnInit();
        enemys.Add(enemy);
    }

    public void RemoveBot(Character enemy)
    {
        if (enemy != null)
        {
            aliveBot -= 1;
            enemys.Remove(enemy);
        }
    }
}
