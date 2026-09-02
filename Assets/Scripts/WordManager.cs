using UnityEngine;
using Consystently.Essentials;
using TMPro;
using System.Collections.Generic;

public class WordManager : Manager<WordManager>
{
  #region TYPING
  [SerializeField] TextMeshProUGUI _currentWordText;

  public string CurrentWord { get; private set; } = string.Empty;
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

  void Start()
  {
    
  }

  // Update is called once per frame
  void Update()
  {
      
  }

  public void SetMaxLength(int newLength) => _maxLength = Mathf.Clamp(newLength, 3, 10);

  public void SetActiveWord()
  {
    int random;
    string newWord = string.Empty;

    // re-roll for a word if the new word is the same as the current word.
    while (newWord == CurrentWord || newWord == string.Empty)
    {
      random = Random.Range(0, WordBank[_maxLength].Length);
      newWord = WordBank[_maxLength][random];
    }

    CurrentWord = newWord;
  }

  public void SetActiveFontStyle()
  {
    
  }


}
