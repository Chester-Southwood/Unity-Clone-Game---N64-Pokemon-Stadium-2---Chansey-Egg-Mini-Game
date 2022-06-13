using UnityEngine;

public class MissedEggListener : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.GoodCatchable.ToString()))
        {
            AudioController.Instance.PlayFallableObjectMissed();
        }
    }
}
