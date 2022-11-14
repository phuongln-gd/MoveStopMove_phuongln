using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform startPoint;
    public Transform ground;
   

    public int botAmount;
    public int maxBotOnGround;
    public int currentBotAmount => enemys.Count;
    private int spawnedBotAmount;
    public Player player;
    public Enemy enemyPrefab;
    [SerializeField] private List<Enemy> enemys = new List<Enemy>();
    [SerializeField] private List<Transform> startPoints_Bot = new List<Transform>();

    private void Start()
    {
        OnInit();
    }

    public void Update()
    {
        if (currentBotAmount < maxBotOnGround && spawnedBotAmount < botAmount)
        {
            SpawnNewBot();
        }
    }
    public void OnInit()
    {
        // TODO:
        player = GameObject.Find("Player").GetComponent<Player>();
        spawnedBotAmount = 0;
        for (int i = 0; i < maxBotOnGround; i++)
        {
            SpawnNewBot();
            spawnedBotAmount += 1;
        }
    }

    public void SpawnNewBot()
    {
        int num = Random.Range(0,startPoints_Bot.Count);
        Enemy enemy = Instantiate(enemyPrefab, startPoints_Bot[num].position , Quaternion.identity);
        enemy.OnInit();
        enemy.ground = ground;
        enemys.Add(enemy);
    }

    public void RemoveBot(Enemy enemy)
    {
        if(enemys.Count > 0 && enemy != null)
        {
            enemys.Remove(enemy);
        }
    }
}
