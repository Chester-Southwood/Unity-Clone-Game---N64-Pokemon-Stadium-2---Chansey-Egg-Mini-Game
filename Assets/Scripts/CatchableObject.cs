using UnityEngine;

public class CatchableObject : MonoBehaviour
{
    public float speed;
    void Update() => transform.Translate(Vector3.down * speed * Time.deltaTime);
}