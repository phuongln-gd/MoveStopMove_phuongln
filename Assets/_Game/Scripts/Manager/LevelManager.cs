using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Level[] levelPrefabs;
    public Player player;

    public Level currentLevel;


    private void Start()
    {
        OnInit();
        UIManager.Ins.OpenUI<MainMenu>();
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }
    public void OnInit()
    {
        LoadLevel(0);
        player.tf.position = currentLevel.startPoint.position;
        player.OnInit();
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
