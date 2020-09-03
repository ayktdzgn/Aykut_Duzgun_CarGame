using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cars : MonoBehaviour
{

    Car[] carsArray;

    public Car[] CarsArray { get => carsArray; set => carsArray = value; }

    private void Awake()
    {
        carsArray = GetComponentsInChildren<Car>(true);
    }
}
