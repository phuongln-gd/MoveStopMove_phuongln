using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Level[] levelPrefabs;
    public Player player;
    public Enemy enemyPrefab;
    public Transform ground => currentLevel.ground;
    public int characterAmount => currentLevel.botAmount + 1;

    public Level currentLevel;

    private List<Enemy> enemys = new List<Enemy>();

    private void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        /*CONTINUE
         Sinh ra  1 luong bot khi bat dau choi
         */
        player.tf.position = currentLevel.startPoint.position;
        for(int i = 0; i < currentLevel.currentBotAmount; i++)
        {
            SpawnNewBot();
        }
    }

    private void SpawnNewBot()
    {
        float posX = player.tf.position.x,
            posZ = player.tf.position.z;
        while (Mathf.Abs(posX-player.tf.position.x) < 10f)
        {
            posX = Random.Range(-ground.localPosition.x / 2, ground.localPosition.x / 2 + 1);
        }
        while (Mathf.Abs(posZ - player.tf.position.z) < 10f)
        {
            posZ = Random.Range(-ground.localPosition.y / 2, ground.localPosition.y / 2 + 1);
        }
        Enemy enemy = Instantiate(enemyPrefab, new Vector3(posX, player.tf.position.y, posZ), Quaternion.identity);
        enemy.OnInit();
        enemys.Add(enemy);
    }
    public void LoadLevel(int level) 
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        if(level < levelPrefabs.Length)
        {
            currentLevel = Instantiate(levelPrefabs[level]);
            currentLevel.OnInit();
        }
        else
        {
            //TODO: level vuot qua limit
        }
    }

    public void OnStartGame()
    {

    }

    public void OnFinishGame()
    {

    }

    public void OnReset()
    {

    }
}
