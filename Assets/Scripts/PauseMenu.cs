using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private StarterAssetsInputs _input;
    [SerializeField] private CinemachineVirtualCamera _normalCamera;
    [SerializeField] private CinemachineVirtualCamera _aimCamera;

    private bool _isPaused;

    private void Update()
    {
        if (_input.Pause)
        {
            _input.Pause = false;

            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (_isPaused)
            Time.timeScale = 1.0f;
        else
            Time.timeScale = 0.0f;

        _isPaused = !_isPaused;
        _pauseMenu.SetActive(_isPaused);
        _normalCamera.enabled = !_isPaused;
        _aimCamera.enabled = !_isPaused;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(Constants.MainMenuScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
