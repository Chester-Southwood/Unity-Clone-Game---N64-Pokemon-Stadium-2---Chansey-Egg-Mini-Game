using System.Collections.Generic;
using UnityEngine;

public class CatchableObjectSpawnerManager : MonoBehaviour
{
    public GameObject goodObject;
    public GameObject badObject;

    private GameObject dropableObject1;
    private GameObject dropableObject2;

    private float timeBetweenSpawn;
    public float startBetweenSpawn;
    public float decreaseTime;
    public float minTime = 0.65f;
        
    [SerializeField] public int catchableObjectSpeed = 2;

    public List<GameObject> SpawnerLocations;

    void Start() => AudioController.Instance.PlayGameMusic();

    void Update() => SpawnCatchableObjects();
        
    private void SpawnCatchableObjects()
    {
        if (int.Parse(UiController.Instance.GetEggsRemainingUi()) == 0) return;

        if (timeBetweenSpawn <= 0)
        {
            ConfigureCatchableObjects();
            UpdateDecrementEggsRemainingUi();
            InstantiateCatchableObjects();

            timeBetweenSpawn = startBetweenSpawn;
            if (startBetweenSpawn > minTime)
            {
                startBetweenSpawn -= decreaseTime;
            }
        }
        else
        {
            timeBetweenSpawn -= Time.deltaTime;
        }
    }

    private void ConfigureCatchableObjects()
    {
        dropableObject1 = Random.Range(1, 3) == 1 ? goodObject : badObject;
        dropableObject1.GetComponent<CatchableObjectMovements>().speed = catchableObjectSpeed;
        dropableObject2 = Random.Range(1, 3) == 1 ? goodObject : badObject;
        dropableObject2.GetComponent<CatchableObjectMovements>().speed = catchableObjectSpeed;

        if (dropableObject1.Equals(goodObject) && dropableObject2.Equals(goodObject))
        {
            if (Random.Range(1,3) == 1)
            {
                dropableObject1 = badObject;
            }
            else
            {
                dropableObject2 = badObject;
            }
        }
    }

    private void UpdateDecrementEggsRemainingUi()
    {
        if (dropableObject1.Equals(goodObject)) UiController.Instance.DecrementEggsRemainingUi();
        if (dropableObject2.Equals(goodObject)) UiController.Instance.DecrementEggsRemainingUi();
    }

    private void InstantiateCatchableObjects()
    {
        int positionRan = Random.Range(1, 7);

        if (positionRan == 1)
        {
            Instantiate(dropableObject1, SpawnerLocations[0].transform.position, Quaternion.identity);
        }
        else if (positionRan == 2)
        {
            Instantiate(dropableObject1, SpawnerLocations[1].transform.position, Quaternion.identity);
        }
        else if (positionRan == 3)
        {
            Instantiate(dropableObject1, SpawnerLocations[2].transform.position, Quaternion.identity);
        }
        else if (positionRan == 4)
        {
            Instantiate(dropableObject1, SpawnerLocations[0].transform.position, Quaternion.identity);
            Instantiate(dropableObject2, SpawnerLocations[1].transform.position, Quaternion.identity);
        }
        else if (positionRan == 5)
        {
            Instantiate(dropableObject1, SpawnerLocations[0].transform.position, Quaternion.identity);
            Instantiate(dropableObject2, SpawnerLocations[2].transform.position, Quaternion.identity);
        }
        else if (positionRan == 6)
        {
            Instantiate(dropableObject1, SpawnerLocations[1].transform.position, Quaternion.identity);
            Instantiate(dropableObject2, SpawnerLocations[2].transform.position, Quaternion.identity);
        }
    }
}