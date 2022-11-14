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
        LoadLevel(0);
        player.tf.position = currentLevel.startPoint.position;
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
