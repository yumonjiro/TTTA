using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Tile tile;
    public Transform _cam;
    public int width, height;
    public static GridManager Instance;
    public GameObject warningImage;

    private Tile[,] Grid;
    public bool _canChangeGrid;


    void Awake()
    {
        Instance = this;
    }
    public void Initialize()
    {
        _canChangeGrid = true;
        if (Grid == null)
        {
            Grid = new Tile[width, height];
        }

        
        GenerateGrid();
        _cam.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        _cam.transform.position = new Vector3(1, 5, 1);
        GameManager.Instance.ChangeState(GameManager.GameState.CircleTurn);
    }

    public void GenerateGrid()
    {
        
        for(int z = 0; z < height; z++)
        {
            for(int x = 0; x < width; x++)
            {
                if (Grid[x, z] != null)
                {
                    Destroy(Grid[x, z].gameObject);
                }
                
                Grid[x, z] = Instantiate(tile, new Vector3(x, 0, z), Quaternion.identity);
                bool isOffset;
                if((x+z) % 2 == 1)
                {
                    isOffset = true;
                }
                else
                {
                    isOffset = false;
                }
                Grid[x,z].Init(isOffset);
                
            }
        }
    }

    public IEnumerator RndVanish()
    {
        int index_x = UnityEngine.Random.Range(0, 3);
        int index_z = UnityEngine.Random.Range(0, 3);
        if (Grid[index_x, index_z].tileState != Tile.TileState.Flat)
        {
            warningImage.SetActive(true);
            yield return new WaitForSeconds(0.5f);

            //var tmp = Grid[index_x, index_z].tileState;
            //yield return new WaitForSeconds(0.1f);
            //Grid[index_x, index_z].ChangeTile(Tile.TileState.Flat);
            //yield return new WaitForSeconds(0.1f);
            //Grid[index_x, index_z].ChangeTile(tmp);
            //yield return new WaitForSeconds(0.1f);

            Grid[index_x, index_z].ChangeTile(Tile.TileState.Flat);
            yield return new WaitForSeconds(0.5f);

            warningImage.SetActive(false);
        }
        GameManager.Instance.Judge();
        GridManager.Instance._canChangeGrid = true;

        yield return new WaitForSeconds(0.0f);
    }

    public bool isFull()
    {
        int tileCount = 0;
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                if (Grid[x,z].tileState != Tile.TileState.Flat)
                {
                    tileCount += 1;
                }

            }
        }

        if(tileCount >= width*height)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckLines()
    {
        // return true if game is end

        for(int x = 0; x < width; x++)
        {
            
            if ((Grid[x,0].tileState == Grid[x,1].tileState) &&
                (Grid[x,1].tileState == Grid[x,2].tileState) &&
                Grid[x,1].tileState != Tile.TileState.Flat)
            {
                return true;
            }
        }
        for (int z = 0; z < height; z++)
        {
            if ((Grid[0, z].tileState == Grid[1, z].tileState) &&
                (Grid[1, z].tileState == Grid[2, z].tileState) &&
                Grid[0, z].tileState != Tile.TileState.Flat)
            {
                return true;
            }
        }
        if ((Grid[0,0].tileState == Grid[1,1].tileState) &&
            (Grid[1,1].tileState == Grid[2,2].tileState) &&
            Grid[0, 0].tileState != Tile.TileState.Flat)
        {
            return true;
        }
        if ((Grid[2, 0].tileState == Grid[1, 1].tileState) &&
            (Grid[1, 1].tileState == Grid[0, 2].tileState) &&
            Grid[2, 0].tileState != Tile.TileState.Flat)
        {
            return true;
        }
        return false;
    }
    public bool CheckAvailability()
    {
        if (_canChangeGrid)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
