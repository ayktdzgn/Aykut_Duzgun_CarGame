using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    GameObject tapAnimation;

    void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.OnScoreIncrease += ScoreIncrease;
        gameManager.OnTapNotifier += TapAnimation;

        ScoreIncrease();
    }

    void ScoreIncrease()
    {
        if (scoreText != null)
        {
            scoreText.text = gameManager.CurrentCarIndex - 1 + "/" + gameManager.Cars.CarsArray.Length;
        }
        else
        {
            Debug.LogError("Need to implement text complement");
        }
        
    }

    void TapAnimation(bool isActive)
    {
        tapAnimation.SetActive(isActive);
    }


    private void OnDestroy()
    {
        gameManager.OnScoreIncrease -= ScoreIncrease;
    }
}
