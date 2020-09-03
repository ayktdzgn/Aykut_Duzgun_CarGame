using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateCarEditor : Editor
{
    [MenuItem("Car Game/Setup Scene")]
    static void SetupScene()
    {
        if (FindObjectOfType<Canvas>() == null)
        {
            GameObject canvas = Resources.Load<GameObject>("Prefabs/Canvas");
            canvas = Instantiate(canvas);
            canvas.GetComponent<Canvas>().worldCamera = Camera.main;
        }

        if (FindObjectOfType<GameManager>() == null)
        {
            GameObject gameManager = Resources.Load<GameObject>("Prefabs/GameManager");
            gameManager = Instantiate(gameManager);
        }
    }


    [MenuItem("Car Game/Create Car")]
    static void CreateCar()
    {
        GameObject carPackage = Resources.Load<GameObject>("Prefabs/Car&Start_Stop");

        if (FindObjectOfType<Cars>() == null)
        {
            Debug.LogError("Need to Setup Scene  --- Cars GameObject is Missing");
        }
        else
        {
            GameObject cars = FindObjectOfType<Cars>().gameObject;
            Instantiate(carPackage, cars.transform);
        }

        

    }

    [MenuItem("Car Game/Create Obstacle")]
    static void CreateObstacle()
    {
        GameObject obstacles = FindObjectOfType<Obstacles>().gameObject;
        GameObject carPackage = Resources.Load<GameObject>("Prefabs/Obstacle");

        if (obstacles == null)
        {
            Debug.LogError("Need to Setup Scene  --- Obstacle GameObject is Missing");
        }
        else
        {
            Instantiate(carPackage, obstacles.transform);
        }

        

    }
}
