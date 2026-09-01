using Consystently.Essentials;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypingManager : MonoBehaviour
{
    string[] _words;
    int _currWordIdx;
    public string PlayerTypedWord;
    bool _isMatchSuccessful;

    [SerializeField] TMP_InputField _inputField;
    [SerializeField] TMP_Text _currentWordText;

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
        _words = new string[] { "Red", "Apple", "Little", "Favorite" };
        _currWordIdx = 0;
        PlayerTypedWord = string.Empty;
        _isMatchSuccessful = false;
        SetCurrentWordText(_words[_currWordIdx]);
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

            _inputField.text = "";
            _isMatchSuccessful = false;
        }
    }

    IEnumerator LittlePause()
    {
        yield return new WaitForSeconds(2f);
    }

    void SetCurrentWordText(string newWord) => _currentWordText.text = newWord;

    void PlayerInputCheck(string inputText)
    {
        //Debug.Log(_words[_currWordIdx].Substring(0, inputText.Length));
        // inputText.Length > 0 && 

        if (inputText == _words[_currWordIdx].Substring(0, inputText.Length))
        {
            Debug.Log("Yes");
        }
        else
        {
            Debug.Log("No");
        }

        //Debug.Log(inputText);
    }

    void FinalLetterCheck(string inputText)
    {
        if (inputText.Length != _words[_currWordIdx].Length) return;
        if (inputText == _words[_currWordIdx]) _isMatchSuccessful = true;

    }
}
