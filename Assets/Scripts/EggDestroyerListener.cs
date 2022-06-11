using UnityEngine;

public class EggDestroyerListener : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) => Destroy(other.gameObject);
}