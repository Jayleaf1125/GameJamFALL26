using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class TimerVisualizer : MonoBehaviour
{
  [Header("Time Settings")]
  
  [Space(5)]
  [Range (0.5f, 3.0f)]
  [SerializeField] private float timeGain = 2.0f;

  [Space(5)]
  [Range (0.25f, 1f)]
  [SerializeField] private float timeLoss = 0.5f;

  public float TimeRemaining { get; private set; }
  private bool _timerPaused = true;
  private const float MAX_TIME = 10.0f;
  private TextMeshProUGUI _timer;
  private Slider _radialSlider;

  private GameplayHandler _gameplayHandler;
  private ActiveWordVisualizer _activeWordVisualizer;

  private void Awake()
  {
    _gameplayHandler ??= FindFirstObjectByType<GameplayHandler>();
    _activeWordVisualizer ??= FindFirstObjectByType<ActiveWordVisualizer>();
  }

  private void FixedUpdate()
  {
    if(_timerPaused == false)
      if(TimeRemaining > 0.0f)
      {
        TimeRemaining -= Time.deltaTime;
        _timer.text = TimeRemaining.ToString("F1") + "s";
        _radialSlider.value = TimeRemaining;
      }
  }

  public void NewSession()
  {
    ResetTimer();
  }

  public void NewRound(int currentRound)
  {
    _timerPaused = false;
  }

  public void ResetTimer()
  {
    _timer ??= GetComponentInChildren<TextMeshProUGUI>();
    _radialSlider ??= GetComponentInChildren<Slider>();
    _radialSlider.maxValue = MAX_TIME;

    TimeRemaining = MAX_TIME;
    _timer.text = MAX_TIME.ToString("F1") + "s";
    _radialSlider.value = MAX_TIME;
  }

  public void AddTime()
  {
    if(TimeRemaining > 0.0f)
    {
      TimeRemaining = Mathf.Clamp(TimeRemaining + timeGain, 0, MAX_TIME);
      _timer.text = TimeRemaining.ToString("F1") + "s";
    }
  }

  public void SubtractTime()
  {
    if(TimeRemaining > 0.0f)
    {
      TimeRemaining = Mathf.Clamp(TimeRemaining - timeLoss, 0, MAX_TIME);
      _timer.text = TimeRemaining.ToString("F1") + "s";
    }
  }  
}
