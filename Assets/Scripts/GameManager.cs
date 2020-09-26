using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton Class: GameManager
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }
    #endregion

    Coloring[] groundPieces;

    void Start()
    {
        FindGroundPiecesOnStage();
        GameDatas.isGameOver = false;
    }

    //To find ground pieces on new stage
    void FindGroundPiecesOnStage()
    {
        groundPieces = FindObjectsOfType<Coloring>();
    }

    public void CheckIfLevelComplete()
    {
        GameDatas.isGameOver = true;

        //Check if it has been painted of every piece
        for (int i = 0; i < groundPieces.Length; i++)
        {
            //If detect any uncoloured piece, break the loop
            if (groundPieces[i].isColored == false)
            {
                GameDatas.isGameOver = false;
                break;
            }
        }
        //If all pieces are painted, load to next level
        if (GameDatas.isGameOver)
        {
            UIManager.Instance.ShowLevelCompletedUI();       
            Invoke("CompleteLevel", .6f);
        }
    }

    void CompleteLevel()
    {      
        LevelManager.Instance.LoadNextLevel();
    }
}
