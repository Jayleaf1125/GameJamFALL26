using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;
using FMODUnity;

[RequireComponent(typeof(TextMeshProUGUI))]
[DisallowMultipleComponent]
public class ActiveWordVisualizer : MonoBehaviour
{
  #region TYPING

  public string ActiveWord { get; private set; } = string.Empty;
  public string PlayerWordInput { get; private set; } = string.Empty;
  public int MaxWordLength { get; private set; } = 3;
  int i = 0;
  bool match = true;

  #endregion
  
  #region TEXT
  [Header("Text")]
  [Space(5)]
  [SerializeField] private TextMeshProUGUI _textMesh;
  public List<TMP_FontAsset> FontStyles;
  public Dictionary<int, string[]> WordBank => Words.Bank;
  #endregion

  #region STYLING
  public TMP_FontAsset ActiveFontStyle { get; private set; }
  
  private const string GRN_COL_TAG = "<style=\"G\">";
  private const string YLW_COL_TAG = "<style=\"Y\">";
  private const string RED_COL_TAG = "<style=\"R\">";
    #endregion

  #region AUDIO

    [SerializeField] EventReference TypeEvent;
    public GameObject Player;

    [SerializeField] EventReference WrongEvent;
    [SerializeField] EventReference CorrectEvent;

  #endregion

  private GameplayHandler _gameplayHandler;
  private TimerVisualizer _timerVisualizer;

  private void Awake()
  {
    _gameplayHandler ??= FindFirstObjectByType<GameplayHandler>();
    _timerVisualizer ??= FindFirstObjectByType<TimerVisualizer>();
  }

  private void OnDisable()
  {
    PlayerInputController.Instance.OnLetterKeyPressed -= CompareLetterKey;
  }

  public void NewSession()
  {
    Reset();
    PlayerInputController.Instance.OnLetterKeyPressed += CompareLetterKey;
  }

  public void NewRound(int currentRound)
  {
    // Determine word length and font here
    SetActiveWord();
  }

  public void Reset()
  {
    ActiveWord = string.Empty;
    PlayerWordInput = string.Empty;
    i = 0;
    MaxWordLength = 3;
  }

  private void CompareLetterKey(string letter)
  {
    if(letter.Length > 1)
      return;

    if(letter[0] == ActiveWord[i])
    {
      // Match
      PlayerWordInput += letter[0];
      i++;

      if(PlayerWordInput == ActiveWord)
    {
        RuntimeManager.PlayOneShotAttached(CorrectEvent, Player);
        TypeSound();
        _timerVisualizer.AddTime();
        SetActiveWord();
      }
      else
      {
        TypeSound();
        match = true;
        UpdateWordStyling();
      }
    }
    else
    {
      // Incorrect
      TypeSound();
      _timerVisualizer.SubtractTime();
      match = false;
      UpdateWordStyling();
    }
  }

  public void SetMaxLength(int newLength) => MaxWordLength = Mathf.Clamp(newLength, 3, 10);

  public void SetActiveWord()
  {
    int random;
    string newWord = string.Empty;

    PlayerWordInput = string.Empty;
    i = 0;

    // re-roll for a word if the new word is the same as the current word.
    while (newWord == ActiveWord || newWord == string.Empty)
    {
      random = Random.Range(0, WordBank[MaxWordLength].Length);
      newWord = WordBank[MaxWordLength][random];
    }

    ActiveWord = newWord;
    UpdateWordStyling();
  }
  private void UpdateWordStyling()
  {
    string styledInput, styledIndex, substring;

    styledInput = $"{GRN_COL_TAG}{PlayerWordInput}";
    styledIndex = $"{(match == true ? YLW_COL_TAG : RED_COL_TAG)}{ActiveWord[i]}<style=\"Normal\">";
    substring = i < ActiveWord.Length ? ActiveWord.Substring(i + 1) : string.Empty;

    _textMesh.text = styledInput + styledIndex + substring;
    if (match == false)
    {
      RuntimeManager.PlayOneShotAttached(WrongEvent, Player);
      match = true;
      Invoke(nameof(UpdateWordStyling), 0.3f);
    }
  }

  public void SetActiveFontStyle()
  {
    // in Words.cs there are strings for each of the font's names that you can use to pick one out from the list. bring those strings to this script if you must.
  }

    void TypeSound()
    {
        RuntimeManager.PlayOneShotAttached(TypeEvent, Player);
    }

}
