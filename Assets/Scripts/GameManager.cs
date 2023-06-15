using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState gameState;
    public Renderer circleBackgroundRenderer;
    Color circleColor;
    public Renderer crossBackgroundRenderer;
    Color crossColor;
    public CanvasGroup resultMenu;


    Result result;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GridManager.Instance.Initialize();
        circleColor = circleBackgroundRenderer.material.color;
        crossColor = crossBackgroundRenderer.material.color;
    }

    public void ChangeState(GameState newState)
    {
        
        switch (newState)
        {
            case GameState.Initialize:
                GridManager.Instance.Initialize();
                circleBackgroundRenderer.material.color = circleColor;
                crossBackgroundRenderer.material.color = crossColor;
                break;
            case GameState.CircleTurn:
                gameState = GameState.CircleTurn;
                break;
            case GameState.CrossTurn:
                gameState = GameState.CrossTurn;
                break;
            case GameState.End:
                gameState = GameState.End;
                EndGame();
                break;
        }
    }
    public void Judge()
    {
        if(GridManager.Instance.CheckLines())
        {
            if(gameState == GameState.CircleTurn)
            {
                Debug.Log("Circle Win!");
                result = Result.CircleWon;
            }
            else
            {
                Debug.Log("Cross Win!");
                result = Result.CrossWon;
            }
            ChangeState(GameState.End);
        }
        else
        {
            if (GridManager.Instance.isFull())
            {
                result = Result.Draw;
                ChangeState(GameState.End);

            }
            else
            {
                if(gameState == GameState.CircleTurn)
                {
                    ChangeState(GameState.CrossTurn);
                }
                else
                {
                    ChangeState(GameState.CircleTurn);
                }
            }
        }
            

    }
    private void EndGame()
    {
        if(result == Result.CircleWon)
        {
            crossBackgroundRenderer.material.color = circleColor;
        }
        else if(result == Result.CrossWon)
        {
            circleBackgroundRenderer.material.color = crossColor;

        }
        else
        {
            
        }
        resultMenu.gameObject.SetActive(true);
    }
    

    public enum GameState
    {
        Initialize = 0,
        CircleTurn = 1,
        CrossTurn = 2,
        End = 3,
    }

    public enum Result
    {
        CircleWon = 0,
        CrossWon = 1,
        Draw = 2,
    }
}
