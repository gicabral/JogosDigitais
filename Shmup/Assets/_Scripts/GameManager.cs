using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    public enum GameState { MENU, GAME, PAUSE, ENDGAME, NEWHIGHSCORE };

    public GameState gameState { get; private set; }
    public int vidas;
    public int pontos;
    public int highscore;

    public delegate void ChangeStateDelegate();
    public static ChangeStateDelegate changeStateDelegate;

    private static GameManager _instance;

    private GameManager()
    {
        vidas = 10;
        pontos = 0;
        highscore = 0;
        gameState = GameState.MENU;
    }

    public static GameManager GetInstance()
    {
        if(_instance == null)
        {
            _instance = new GameManager();
        }

        return _instance;
    }
    
    public void ChangeState(GameState nextState)
    {
        if(nextState == GameState.GAME) Reset();
        gameState = nextState;
        try{
            changeStateDelegate();
        }
        catch{
            Debug.LogWarning($"ChangeStateDelegate null");
        }    
    }

    private void Reset(){
        vidas = 10;
        pontos = 0;
        SceneManager.LoadScene(0);
    }

    public bool CheckForHighScore(int score)
    {
        int highScore = PlayerPrefs.GetInt("highScore");
        if(score > highScore){
            return true;
        }

        return false;
    }
}
