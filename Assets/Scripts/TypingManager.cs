using Consystently.Essentials;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypingManager : MonoBehaviour
{
    #region TYPING
    string[] _words;
    int _currWordIdx;
    public string PlayerTypedWord;
    bool _isMatchSuccessful;
    int _currCharacterLimit;
    int _currScore;

    [SerializeField] TMP_InputField _inputField;
    [SerializeField] TMP_Text _currentWordText;
    #endregion

    #region FONTS
    List<FontStyle> _fontStyles;
    #endregion



    private void OnEnable()
    {
        _inputField.onValueChanged.AddListener(PlayerInputCheck);
        _inputField.onValueChanged.AddListener(FinalLetterCheck);
    }

    private void OnDisable()
    {
        _inputField.onValueChanged.RemoveListener(PlayerInputCheck);
        _inputField.onValueChanged.RemoveListener(FinalLetterCheck);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _words = new string[] { "Cat", "Dog", "Sun", "Red", "Tree", "Blue", "Moon", "Star", "Apple", "Chair", "Stone", "River", "Planet", "Silver", "Castle", "Forest" };
        _currWordIdx = 0;
        PlayerTypedWord = string.Empty;
        _isMatchSuccessful = false;
        SetCurrentWordText(_words[_currWordIdx]);

        _currCharacterLimit = 3;
        _inputField.characterLimit = _currCharacterLimit;

        _currScore = 0;

        _inputField.ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMatchSuccessful)
        {
            StartCoroutine(LittlePause());

            int nextIdx = _currWordIdx + 1;
            if (nextIdx < _words.Length)
            {
                SetCurrentWordText(_words[++_currWordIdx]);
            }

            TimeManager.Instance.AddTwoSeconds();
            _currScore++;
            CheckScore();

            _inputField.text = "";
            _isMatchSuccessful = false;
        }
    }

    void CheckScore()
    {
        if (_currScore % 4 == 0)
        {
            _inputField.characterLimit += 1;
        }
    }

    IEnumerator LittlePause()
    {
        yield return new WaitForSeconds(2f);
    }

    void SetCurrentWordText(string newWord) => _currentWordText.text = newWord;

    IEnumerator PlayerStop()
    {
        _inputField.interactable = false;
        yield return new WaitForSeconds(0.5f);
        _inputField.interactable = true;
        _inputField.ActivateInputField();
    }

    void PlayerInputCheck(string inputText)
    {

        if (inputText != _words[_currWordIdx].Substring(0, inputText.Length))
        {
            StartCoroutine(PlayerStop());
        }

    }

    void FinalLetterCheck(string inputText)
    {
        if (inputText.Length != _words[_currWordIdx].Length) return;
        if (inputText == _words[_currWordIdx]) _isMatchSuccessful = true;
    }
}
