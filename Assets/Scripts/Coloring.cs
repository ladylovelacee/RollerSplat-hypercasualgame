using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coloring : MonoBehaviour
{
    public bool isColored;

    void Start()
    {
        isColored = false;
    }

    /// <summary>
    /// To paint ground's pieces
    /// </summary>
    /// <param name="color"></param>
    public void Colored(Color color)
    {
        //<MeshRenderer>().material.color = color;
        GetComponent<MeshRenderer>().material.DOColor(color, .1f);
        isColored = true;

        FindObjectOfType<GameManager>().CheckIfLevelComplete();
    }
}
