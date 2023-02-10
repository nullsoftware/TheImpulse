using Michsky.MUIP;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _saveDataTextInfo;
    [SerializeField] private GameObject _mainScreen;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private ProgressBar _progressBar;

    private void Start()
    {
        GameData gameData = GameData.Load(Constants.DefaultSaveFileName);

        if (gameData != null )
        {
            _saveDataTextInfo.text = $"Last played: {gameData.LastPlayTime:yyyy-MM-dd HH:mm}, {gameData.WinCount} Wins, {gameData.LoseCount} Loses.";
        }
        else
        {
            _saveDataTextInfo.gameObject.SetActive(false);
        }
    }

    public void Play()
    {
        _mainScreen.SetActive(false);
        _loadingScreen.SetActive(true);
        StartCoroutine(LoadGame());
    }

    public void Exit()
    {
        Application.Quit();
    }

    private IEnumerator LoadGame()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(Constants.PlaygroundScene);

        _progressBar.ChangeValue(0);

        while (!operation.isDone)
        {
            _progressBar.ChangeValue(operation.progress);
            yield return null;
        }
    }
}
