using UnityEngine;

public class EggCatcherListener : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.GoodCatchable.ToString()))
        {
            AudioController.Instance.PlayFallableObjectCaught();
            UiController.Instance.IncrementPointsUi();
            gameObject.transform.parent.GetComponentInChildren<PlayerMovement>().ForceSquish();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag(Tags.BadCatchable.ToString()))
        {
            int points = int.Parse(UiController.Instance.GetPointsUi());
            UiController.Instance.SetPointsUi(points - 5 < 0 ? 0 : points - 5);
            gameObject.transform.parent.GetComponentInChildren<PlayerMovement>().ForcePeformJump();
            Destroy(other.gameObject);
        }
    }
}