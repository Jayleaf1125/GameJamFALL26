using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class GameplayHandler : MonoBehaviour
{
  [Header("Link Backgrounds Here")] // Made by Emmanuel
  public GameObject BG1;
  public GameObject BG2;
  public GameObject BG3;
  
  [Header("HUD")]
  
  [Space(5)]
  [SerializeField] private TextMeshProUGUI _score;
  [SerializeField] private TextMeshProUGUI _round;

  public int PlayerScore { get; private set; } = 0;
  public int CurrentRound { get; private set; } = 0;


  private ActiveWordVisualizer _activeWordVisualizer;
  private TimerVisualizer _timerVisualizer;

  public void Awake()
  {
    _activeWordVisualizer ??= FindFirstObjectByType<ActiveWordVisualizer>();
    _timerVisualizer ??= FindFirstObjectByType<TimerVisualizer>();
  }

  private void Start()
  {
    NewSession();
  }

  public void NewSession()
  {
    PlayerScore = 0;
    CurrentRound = 2;
    
    _score.text = $"Score: {PlayerScore}";
    _round.text = $"Round {CurrentRound}";
    
    _activeWordVisualizer.NewSession();
    _timerVisualizer.NewSession();

    NewRound();
  }

  public void NewRound()
  {
    CurrentRound++;
    _round.text = $"Round {CurrentRound}";

    PlayerInputController.Instance.Gameplay.Enable();

    _activeWordVisualizer.NewRound(CurrentRound);
    _timerVisualizer.NewRound(CurrentRound);
    SwitchBackground(); // Made by Emmanuel
  }

  public void AddToScore(int addition) 
  {
    PlayerScore += addition;
    _score.text = $"Score: {PlayerScore}";
  }

  public void SwitchBackground()  // Made by Emmanuel
  {
    if(CurrentRound == 1)
    {
      BG1.SetActive(true);
      BG2.SetActive(false);
      BG3.SetActive(false);
    }
    
    if(CurrentRound == 2)
    {
      BG1.SetActive(false);
      BG2.SetActive(true);
      BG3.SetActive(false);
    }
    
    if(CurrentRound == 3)
    {
      BG1.SetActive(false);
      BG2.SetActive(false);
      BG3.SetActive(true);
    }
  }
}
