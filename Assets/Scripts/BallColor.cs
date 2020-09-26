using UnityEngine;

public class BallColor : MonoBehaviour
{
#region Singleton Class: BallColor
    public static BallColor Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
        PaintBallWRandomColor();
    }
    #endregion

    [SerializeField] ParticleSystem paintParticles;
    public static Color randomColor;

    void PaintBallWRandomColor()
    {
        //Assing pretty light colors to ball
        randomColor = Random.ColorHSV(.5f, 1.0f);
        GetComponent<MeshRenderer>().material.color = randomColor;
        paintParticles.startColor = randomColor;
    }
}