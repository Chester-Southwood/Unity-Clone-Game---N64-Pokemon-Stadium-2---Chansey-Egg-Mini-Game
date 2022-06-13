using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    private Sequence tweenJump;
    private Vector3 initialRotation;
    private Vector3 initialPosition;
    private Vector3 initialScale;

    private readonly float MOVEMENT_DURATION = 0.1f;

    [SerializeField] public GameObject LightningAnimation;

    private enum State
    {
        ATTEMPT_CATCH,
        JUMP,
        IDLE
    }

    private State _currentState;
    private State CurrentState
    {
        get => _currentState;
        set
        {
            if (!value.Equals(_currentState))
            {
                //Debug.Log($"State modified from {_currentState} to {value});
                _currentState = value;
            }
        }
    }

    void Start()
    {
        initialRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        initialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        initialScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        CurrentState = State.IDLE;
    }

    void Update()
    {
        OnJump(KeyCode.Space, ref this.tweenJump);
        OnAttemptCatch();
    }

    private void OnAttemptCatch()
    {
        if (!CurrentState.Equals(State.IDLE)) return;


        if (Input.GetKey(KeyCode.A))
        {
            CurrentState = State.ATTEMPT_CATCH;
            transform.DOMove(new Vector3(-0.12f, -0.05f, initialPosition.z), MOVEMENT_DURATION);
            transform.DORotate(new Vector3(initialRotation.x, initialRotation.y + 30f, -17.343f), MOVEMENT_DURATION).OnComplete(() => { CurrentState = State.IDLE; }); ;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            CurrentState = State.ATTEMPT_CATCH;
            transform.DOMove(new Vector3(0.12f, 0.05f, initialPosition.z), MOVEMENT_DURATION);
            transform.DORotate(new Vector3(initialRotation.x, initialRotation.y - 30f, 17.343f), MOVEMENT_DURATION).OnComplete(() => { CurrentState = State.IDLE; }); ;
        }
        else
        {
            transform.DOMove(initialPosition, MOVEMENT_DURATION);
            transform.DORotate(initialRotation, MOVEMENT_DURATION).OnComplete(() => { CurrentState = State.IDLE; });
        }
    }

    private void OnJump(KeyCode keyCode, ref Sequence sequence)
    {
        if (!CurrentState.Equals(State.IDLE) || !IsSequenceOpenToJump(ref sequence)) return;

        if (Input.GetKeyDown(keyCode))
        {
            sequence = NormalPerformJump();
        }
    }

    private bool IsSequenceOpenToJump(ref Sequence sequence)
    {
        if (sequence != null)
        {
            if (!sequence.IsActive())
            {
                sequence = null;
            }
            else if (sequence.IsPlaying())
            {
                CurrentState = State.JUMP;
                return false;
            }
        }
        CurrentState = State.IDLE;
        return true;
    }

    private Sequence NormalPerformJump()
    {
        CurrentState = State.JUMP;
        transform.DOShakeScale(duration: 0.5f, strength: 0.7f);
        GameObject.FindGameObjectWithTag(Tags.DroppedEggAnimator.ToString()).GetComponent<MovementScript>().AnimateDropEggEffect();

        return transform.DOJump(
            endValue: gameObject.transform.position,
            jumpPower: 2,
            numJumps: 1,
            duration: 0.5f,
            snapping: false
        ).SetEase(Ease.Linear)
         .OnComplete(() => CurrentState = State.IDLE);
    }

    public void ForcePeformJump()
    {
        LightningAnimation.SetActive(true);
        SphereCollider pouchCollider = GameObject.FindGameObjectWithTag(Tags.Pouch.ToString()).GetComponent<SphereCollider>();

        pouchCollider.enabled = false;

        AudioController.Instance.PlayThunderShock();
        transform.DOMove(initialPosition, MOVEMENT_DURATION);
        transform.DORotate(initialRotation, MOVEMENT_DURATION);
        tweenJump = NormalPerformJump();
        tweenJump.OnComplete(() =>
        {
            LightningAnimation.SetActive(false);
            pouchCollider.enabled = true;
            CurrentState = State.IDLE;
            AudioController.Instance.StopThunderShock();
        });
    }

    public void ForceSquish()
    {
        transform.DOScaleY(initialScale.y - 0.02f, 0.1f).OnComplete(() =>
        {
            transform.DORewind();
            CurrentState = State.IDLE;
        });
    }
}