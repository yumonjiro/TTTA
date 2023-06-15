using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public CanvasGroup resultMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ButtonClicked()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.Initialize);
        resultMenu.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
