using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    GamePlay,
    Setting,
    Victory,
    Lose
}
public class GameManager : Singleton<GameManager>
{
    public UserData userData;
    public List<Camera> cameras;
    private GameState gameState;

    public bool soundMode; // 0: off,1: on
    protected void Awake()
    {
        //base.Awake();
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
        userData.OnInitData();
        soundMode = userData.musicIsOn;
        //csv.OnInit();
        //userData?.OnInitData();

        //ChangeState(GameState.MainMenu);

        //UIManager.Ins.OpenUI<MianMenu>();
    }
    
    public void ChangeState(GameState gameState)
    {
        this.gameState = gameState;
    }

    public bool IsState(GameState gameState)
    {
        return this.gameState == gameState;
    }

    public void ChangeCameraGamePlay()
    {
        cameras[0].gameObject.SetActive(true);
        cameras[1].gameObject.SetActive(false); 
    }

    public void ChangeCameraMainMenu()
    {
        cameras[0].gameObject.SetActive(false);
        cameras[1].gameObject.SetActive(true);
    }

}
