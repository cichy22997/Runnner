using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public float multiplier;
    public int cashAmount;
    public int distanceRace;
    public int subs;
    public Text CashText, DistanceText, TotalText, multiplierText;
    public static Score Instance { set; get; }

    void Awake()
    {
        multiplier = 1.0f;
        cashAmount = 0;
        distanceRace = 0;
        subs = 0;
    }
    private void Start()
    {
        Instance = this;
    }
    void Update()
    {
        distanceRace = (int)(GameObject.FindGameObjectWithTag("Player").transform.position.x / 10);
        multiplier = GameObject.FindGameObjectWithTag("Player").transform.position.x /100;
        subs = cashAmount * (int)(multiplier*10);
        UpdateScore();
    }

    private void UpdateScore()
    {
        DistanceText.text = distanceRace.ToString();
        CashText.text = cashAmount.ToString();
        multiplierText.text = "x" + multiplier.ToString("0.0");
        TotalText.text = subs.ToString();

    }
    public void ProgressUpdate()
    {
        PlayerProgress.Instance.AddScore();

    }
}
