using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class MainMenuSwitch : MonoBehaviour
{
    [SerializeField] private int nextSceneIndex = 1; // Index of the next scene to load
    private bool isSwitching = false;

    private void OnEnable()
    {
        InputSystem.onAnyButtonPress.Call(OnAnyButtonPressed);
    }

    private void OnAnyButtonPressed(InputControl control)
    {
        //User must click any button on the keyboard
        if (isSwitching) return;

        isSwitching = true;
        StartGame();
    }

    void StartGame()
    {
        //Loading scene
        SceneManager.LoadSceneAsync(nextSceneIndex);
    }
}
