using DG.Tweening;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public GameObject[] eggs;
    public void AnimateDropEggEffect()
    {
        foreach(GameObject egg in eggs)
        {
            GameObject instantiatedEgg = Instantiate(egg);
            DOTweenPath path = instantiatedEgg.GetComponent<DOTweenPath>();
            path.DORestart();
            Tween tween = path.GetTween();

            tween.timeScale = 0.5f;

            tween.SetLoops(1, LoopType.Restart)
                 .SetDelay(0f)
                 .SetAutoKill(false)
                 .OnComplete(() => Destroy(instantiatedEgg))
                 .Play();
        }
    }
}
