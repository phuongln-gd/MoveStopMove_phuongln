using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Level[] levelPrefabs;
    public Player player;

    public Level currentLevel;
    public int levelIndex;

    public TargetIndicatorManager targetIndicatorManager;
    private void Start()
    {
        OnInit();
        UIManager.Ins.OpenUI<MainMenu>();
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }
    public void OnInit()
    {
        levelIndex = GameManager.Ins.userData.PlayingLevel;
        LoadLevel(levelIndex);
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
            levelIndex = 0;
            GameManager.Ins.userData.SetIntData(UserData.Key_PlayingLevel, ref GameManager.Ins.userData.PlayingLevel, levelIndex);
            LoadLevel(levelIndex);
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
