using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    public Transform startPoint;
   
    public int botAmount; // so luong bot cua man choi
    public int aliveBot; // so luong bot con song
    public int maxBotOnGround; // so luong bot toi da tren stage
    public int spawnedBotAmount; // so luong bot da sinh

    public List<Character> enemys = new List<Character>(); // list kiem soat bot hien tai tren stage
    
    public int currentBotAmount => enemys.Count; // so luong bot hien tai tren stage

    [SerializeField] private List<Transform> startPoints_Bot = new List<Transform>();
    [SerializeField] private NavMeshData navMeshData;

    private List<Vector3> listPos = new List<Vector3>();
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

        for(int i =0; i < startPoints_Bot.Count; i++)
        {
            listPos.Add(startPoints_Bot[i].position);
        }

        spawnedBotAmount = 0;
        for (int i = 0; i < maxBotOnGround; i++)
        {
            SpawnNewBotFirstTime();
        }
        aliveBot = botAmount;

        LevelManager.Ins.player.tf.position = LevelManager.Ins.currentLevel.startPoint.position;
        LevelManager.Ins.player.OnInit();
        LevelManager.Ins.player.skin.localScale = Vector3.one;
    }
    
    public void SpawnNewBotFirstTime()
    {
        int randomIndex = Random.Range(0, listPos.Count);
        Enemy enemy = SimplePool.Spawn<Enemy>(PoolType.Bot, listPos[randomIndex], Quaternion.identity);
        listPos.RemoveAt(randomIndex);
        spawnedBotAmount += 1;
        enemy.OnInit();
        enemys.Add(enemy);
    }

    public void SpawnNewBot()
    {
        int randomIndex = Random.Range(0, startPoints_Bot.Count);
        Enemy enemy = SimplePool.Spawn<Enemy>(PoolType.Bot, startPoints_Bot[randomIndex].position, Quaternion.identity);
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
            int characterAmount = aliveBot + 1;
            UIManager.Ins.GetUI<GamePlay>().SetAliveText("Alive: " + characterAmount);
        }
    }
}
