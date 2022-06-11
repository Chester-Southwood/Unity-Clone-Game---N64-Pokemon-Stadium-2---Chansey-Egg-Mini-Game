using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject EggsRemainingUi;
    [SerializeField] private GameObject PointsUi;

    private TextMeshProUGUI EggsRemainingUiText;
    private TextMeshProUGUI PointsUiText;

    private static UiManager _instance;

    public static UiManager Instance
    {
        get => _instance;
        private set => _instance = value;
    }

    private void Awake() => _instance = this;

    void Start()
    {
        EggsRemainingUiText = EggsRemainingUi.GetComponent<TextMeshProUGUI>();
        PointsUiText = PointsUi.GetComponent<TextMeshProUGUI>();
        EggsRemainingUiText.text = "100";
        PointsUiText.text = "0";
    }

    public string GetPointsUi() => PointsUiText.text;
    public void SetPointsUi(int value) => PointsUiText.text = "" + value;
    public void IncrementPointsUi() => PointsUiText.text = "" + (int.Parse(PointsUiText.text) + 1);
    public string GetEggsRemainingUi() => EggsRemainingUiText.text;
    public void SetEggsRemainingUi(int value) => EggsRemainingUiText.text = "" + value;
    public void DecrementEggsRemainingUi() => EggsRemainingUiText.text = "" + (int.Parse(EggsRemainingUiText.text) - 1);
}