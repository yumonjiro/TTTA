using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Color _baseColor, _offsetColor;
    public GameObject _highlight;
    public Renderer _renderer;
    public GameObject circle;
    public GameObject cross;

    
    public TileState tileState;

    public void Init(bool isOffset)
    {
        if(isOffset)
        {
            _renderer.material.color = _offsetColor;
        }
        else
        {
            _renderer.material.color = _baseColor;
        }
        ChangeTile(TileState.Flat);
        UpdateState();
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (GridManager.Instance.CheckAvailability() != true)
        {
            return;
        }
        
        Debug.Log("Clicked");
        if(tileState != TileState.Flat || GameManager.Instance.gameState == GameManager.GameState.End)
        {
            return;
        }
        else
        {
            if (GameManager.Instance.gameState == GameManager.GameState.CircleTurn)
            {
                ChangeTile(TileState.Circle);
                
            }
            else
            {
                ChangeTile(TileState.Cross);

            }
            UpdateState();
            GridManager.Instance._canChangeGrid = false;
            Debug.Log(GridManager.Instance.CheckAvailability());
            StartCoroutine(GridManager.Instance.RndVanish());
            Debug.Log(GridManager.Instance.CheckAvailability());

            
        }
        

    }
    void UpdateState()
    {
        switch(tileState)
        {
            case TileState.Flat:
                circle.SetActive(false);
                cross.SetActive(false);
                break;
            case TileState.Circle:
                circle.SetActive(true);
                break;
            case TileState.Cross:
                cross.SetActive(true);
                break;
            default:
                break;
        }
            

    }
    public void ChangeTile(TileState tState)
    {
        tileState = tState;
        UpdateState();
    }

    public enum TileState
    {
        Flat = 0,
        Circle = 1,
        Cross = 2,
    }
}