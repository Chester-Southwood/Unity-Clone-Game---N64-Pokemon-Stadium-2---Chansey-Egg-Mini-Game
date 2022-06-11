using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class Spawner : MonoBehaviour
    {
        public GameObject goodObject;
        public GameObject badObject;

        private float timeBetweenSpawn;
        public float startBetweenSpawn;
        public float decreaseTime;
        public float minTime = 0.65f;
        
        [SerializeField] public int catchableObjectSpeed = 2;

        public List<GameObject> SpawnerLocations;

        void Update() => SpawnObject();

        private void SpawnObject()
        {
            if (timeBetweenSpawn <= 0)
            {
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

        private void InstantiateCatchableObjects()
        {
            int positionRan = Random.Range(1, 7);

            GameObject dropableObject1 = Random.Range(1, 3) == 1 ? goodObject : badObject;
            dropableObject1.GetComponent<CatchableObject>().speed = catchableObjectSpeed;
            GameObject dropableObject2 = Random.Range(1, 3) == 1 ? goodObject : badObject;
            dropableObject2.GetComponent<CatchableObject>().speed = catchableObjectSpeed;

            if (dropableObject1.Equals(goodObject)) UiManager.Instance.DecrementEggsRemainingUi();
            if (dropableObject2.Equals(goodObject)) UiManager.Instance.DecrementEggsRemainingUi();

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
}