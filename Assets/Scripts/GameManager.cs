using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameStates
    {   Tap,
        Start,
        Play,
        PlayRecord,
        End
    }

    public delegate void OnScore();
    public OnScore OnScoreIncrease;

    public delegate void OnTap(bool status);
    public OnTap OnTapNotifier;

    [HideInInspector]
    public GameStates gameStates;

    int currentCarIndex;
    static GameManager instance;
    Cars cars;
    LevelManager levelManager;

    public int CurrentCarIndex { get => currentCarIndex; set => currentCarIndex = value; }
    public static GameManager Instance { get => instance; set => instance = value; }
    public Cars Cars { get => cars; set => cars = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        levelManager = GetComponent<LevelManager>();


        currentCarIndex = 1;
        gameStates = GameStates.Tap;

        cars = FindObjectOfType<Cars>();
        HideCars();
    }

    private void Update()
    {
        switch (gameStates)
        {
            case GameStates.Tap:
                OnTapNotifier(true);
                ResetCarPositions();
                ShowCurrentCar(currentCarIndex);
                CheckForStart();
                break;
            case GameStates.Start:
                OnTapNotifier(false);
                ShowAvaibleCars(currentCarIndex);
                CheckForPlay();
                break;
            case GameStates.PlayRecord:
                MoveRecordedCars(currentCarIndex);
                break;
            case GameStates.Play:
                MoveCurrentCar(currentCarIndex);
                break;
            case GameStates.End:
                NextLevel();
                break;
            default:
                break;
        }
    }

    void ResetCarPositions()
    {
        foreach (var car in cars.CarsArray)
        {
            car.GetComponentInChildren<CarMechanics>().ResetCar();
        }
    }

    void CheckForStart()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameStates = GameStates.Start;
        }
    }

    void CheckForPlay()
    {
        gameStates = GameStates.PlayRecord;
    }

    public bool CheckForWin()
    {
        if (currentCarIndex > cars.CarsArray.Length)
        {
            gameStates = GameStates.End;
            return true;
        }
        return false;
    }

    void MoveRecordedCars(int carIndex)
    {
        if (carIndex <= cars.CarsArray.Length)
        {
            for (int i = 0; i < carIndex - 1; i++)
            {
                cars.CarsArray[i].GetComponentInChildren<CarMechanics>().PlayRecord();
            }
            gameStates = GameStates.Play;
        }
    }

    void MoveCurrentCar(int carIndex)
    {
        CarMechanics currentCar;
        if (carIndex <= cars.CarsArray.Length && cars.CarsArray[carIndex - 1].GetComponentInChildren<CarMechanics>())
        {
            currentCar = cars.CarsArray[carIndex - 1].GetComponentInChildren<CarMechanics>();
        }
        else
        {
            return;
        }

        if (!currentCar.isPlayed)
        {
            currentCar.CarMoving();
        }

    }

    void ShowCurrentCar(int carIndex)
    {
        HideCars();
        if (carIndex<= cars.CarsArray.Length && !cars.CarsArray[carIndex - 1].gameObject.activeSelf)
        {
            cars.CarsArray[carIndex - 1].gameObject.SetActive(true);
            cars.CarsArray[carIndex - 1].gameObject.GetComponentInChildren<CarMechanics>().StartAndEndPointStatus(true);
        }
    }

    void ShowAvaibleCars(int carIndex)
    {
        if (carIndex <= cars.CarsArray.Length)
        {
            for (int i = 0; i < carIndex; i++)
            {
                if (!cars.CarsArray[i].gameObject.activeSelf)
                {
                    cars.CarsArray[i].gameObject.GetComponentInChildren<CarMechanics>().isPlayed = true;
                    cars.CarsArray[i].gameObject.SetActive(true);
                }
            }
        }
    }

    void HideCars()
    {
        foreach (var car in cars.CarsArray)
        {
            car.gameObject.SetActive(false);
        }
    }

    void NextLevel()
    {
        levelManager.NextLevel();
    }
}
