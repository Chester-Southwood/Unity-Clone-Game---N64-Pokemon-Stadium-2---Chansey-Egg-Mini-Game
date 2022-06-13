using UnityEngine;

public class CatchableObjectMovements : MonoBehaviour
{
    public float speed;
    void Update() => transform.Translate(speed * Time.deltaTime * Vector3.down);
}