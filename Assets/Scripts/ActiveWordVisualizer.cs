using UnityEngine;
using TMPro;
using System.Collections.Generic;

[RequireComponent(typeof(TextMeshProUGUI))]
[DisallowMultipleComponent]
public class ActiveWordVisualizer : MonoBehaviour
{
  #region TYPING
  private TextMeshProUGUI _textMesh;

  public string ActiveWord { get; private set; } = string.Empty;
  public string PlayerWordInput { get; private set; } = string.Empty;
  int _inputIndex = 0;
  bool _charMatch = false;
  int _playerScore = 0;
  int _maxLength = 3;

  #endregion
  
  #region TEXT
  [Header("Text")]
  [Space(5)]
  public List<TMP_FontAsset> FontStyles;

  public TMP_FontAsset ActiveFontStyle { get; private set; }
  public Dictionary<int, string[]> WordBank => Words.Bank;
  #endregion

  public void Awake()
  {
    _textMesh ??= GetComponent<TextMeshProUGUI>();
    Reset();
  }

  public void Reset()
  {
    ActiveWord = string.Empty;
    _textMesh.text = ActiveWord;
    PlayerWordInput = string.Empty;
    _inputIndex = 0;
    _charMatch = false;
    _playerScore = 0;
    _maxLength = 3;
  }

  public void SetMaxLength(int newLength) => _maxLength = Mathf.Clamp(newLength, 3, 10);

  public void SetActiveWord()
  {
    int random;
    string newWord = string.Empty;

    // re-roll for a word if the new word is the same as the current word.
    while (newWord == ActiveWord || newWord == string.Empty)
    {
      random = Random.Range(0, WordBank[_maxLength].Length);
      newWord = WordBank[_maxLength][random];
    }

    ActiveWord = newWord;
    _textMesh.text = ActiveWord;
  }

  public void SetActiveFontStyle()
  {
    
  }
}
