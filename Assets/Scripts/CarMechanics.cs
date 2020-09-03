using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMechanics : MonoBehaviour
{
    [SerializeField]
    int constantMovingSpeed;
    [SerializeField]
    int constantRotateSpeed;

    [SerializeField]
    Transform startPoint;
    [SerializeField]
    Transform endPoint;
    [SerializeField]
    Color color;

    List<Vector3> carTransformList;
    List<Quaternion> carRotationList;

    Quaternion startRotation;
    Renderer renderer;
    GameManager gameManager;

    public bool isPlayed = false;

    private void Awake()
    {
        startRotation = transform.rotation;
        renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;

        ResetCar();

        carTransformList = new List<Vector3>();
        carRotationList = new List<Quaternion>();
    }

    public void ResetCar()
    {
        isPlayed = false;
        transform.position = startPoint.position;
        transform.rotation = startRotation;

        StartAndEndPointStatus(false);
    }

    public void StartAndEndPointStatus(bool isActive)
    {
        startPoint.gameObject.SetActive(isActive);
        endPoint.gameObject.SetActive(isActive);
    }

    public void CarMoving()
    {

        transform.position += transform.up * constantMovingSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A) || (Input.GetMouseButton(0) && Input.mousePosition.x < Screen.width * 0.5f))
        {
            transform.Rotate(new Vector3(0, 0, constantRotateSpeed));
        }
        if (Input.GetKey(KeyCode.D) || (Input.GetMouseButton(0) && Input.mousePosition.x > Screen.width * 0.5f))
        {
            transform.Rotate(new Vector3(0, 0, -constantRotateSpeed));
        }

        Recording(transform.position, transform.rotation);

    }

    IEnumerator OnTriggerEnter(Collider collision)
    {
        if (!isPlayed)
        {
            if (collision.gameObject.tag == "EndPoint")
            {
                //GetComponent<BoxCollider>().enabled = false;
                yield return new WaitForSeconds(0.3f);
                gameManager.CurrentCarIndex++;
                gameManager.OnScoreIncrease();
                if (!gameManager.CheckForWin())
                {
                    gameManager.gameStates = GameManager.GameStates.Tap;
                } 
            }
            else
            {
                ClearRecords();
                gameManager.gameStates = GameManager.GameStates.Tap;
            }
        }
    }

    public void PlayRecord()
    {
        SetCarColor(color);
        StartCoroutine(IEPlayRecord());
    }

    IEnumerator IEPlayRecord()
    {
        isPlayed = true;
        StartAndEndPointStatus(false);
        for (int i = 0; i < carTransformList.Count; i++)
        {
            if (isPlayed)
            {
                transform.position = carTransformList[i];
                transform.rotation = carRotationList[i];
                yield return new WaitForEndOfFrame();
            }
        }
    }

    void Recording(Vector3 carTransform, Quaternion carRotation)
    {
        carTransformList.Add(carTransform);
        carRotationList.Add(carRotation);
    }

    void ClearRecords()
    {
        carTransformList.Clear();
        carRotationList.Clear();
    }

    void SetCarColor(Color _color)
    {
        renderer.material.color = _color;
    }

}
