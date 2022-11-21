using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Level[] levelPrefabs;
    public Player player;
    public int characterAmount => currentLevel.botAmount + 1;

    public Level currentLevel;


    private void Start()
    {
        OnInit();
        //UIManager.Ins.OpenUI<MainMenu>();
    }
    public void OnInit()
    {
        LoadLevel(0);
        player.OnInit();
        player.tf.position = currentLevel.startPoint.position;
        currentLevel.OnInit();
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
    }
    public void OnStartGame()
    {
        GameManager.Ins.ChangeState(GameState.GamePlay); 
    }
    public void OnFinishGame()
    {

    }
    public void OnReset()
    {

    }
}
