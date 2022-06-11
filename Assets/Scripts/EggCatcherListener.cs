using UnityEngine;

public class EggCatcherListener : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoodCatchable"))
        {
            UiManager.Instance.IncrementPointsUi();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("BadCatchable"))
        {
            int points = int.Parse(UiManager.Instance.GetPointsUi());
            UiManager.Instance.SetPointsUi(points - 5);
            gameObject.GetComponentInParent<PlayerMovement>().ForcePeformJump();
            Destroy(other.gameObject);
        }
    }
}