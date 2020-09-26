using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    #region Singleton Class: UIManager
    public static UIManager Instance;

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

    [SerializeField] TMP_Text levelText = default;

    [Header("Level Complete UI")]
    [SerializeField] Image fadePanel;

    int sceneOffset = 1;

    void Start()
    {
        fadeAtStart();
        SetLevelText();
    }

    void SetLevelText()
    {
        int level = SceneManager.GetActiveScene().buildIndex + sceneOffset;
        levelText.text = "LEVEL" + " " + level;
    }

    public void ShowLevelCompletedUI()
    {
        fadePanel.DOFade(1f, .4f).From(0f);
    }

    public void fadeAtStart()
    {
        fadePanel.DOFade(0f, .2f).From(1f);
    }
}
