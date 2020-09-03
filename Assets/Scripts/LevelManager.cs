using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void NextLevel()
    {
        Debug.Log("Next Level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
