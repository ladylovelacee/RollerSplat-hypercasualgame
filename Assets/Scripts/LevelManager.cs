using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    #region Singleton Class: LevelManager
    public static LevelManager Instance;

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

    [SerializeField] SpriteRenderer bgFadeSprite = default;

    [Header("Level Colors-----------")]

    [Header("Background")]
    [SerializeField] Color cameraColor = default;
    [SerializeField] Color fadeColor = default;

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
    }

    void UpdateLevelColors()
    {
        Camera.main.backgroundColor = cameraColor;
        bgFadeSprite.color = fadeColor;
    }

    void OnValidate()
    {
        UpdateLevelColors();
    }
}
