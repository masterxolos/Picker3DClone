using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private int _currentCheckpoint = 0;
    
    [SerializeField] private TextMeshProUGUI _currentLevelText;
    [SerializeField] private TextMeshProUGUI _nextLevelText;

    [SerializeField] private GameObject _checkpoint1;
    [SerializeField] private GameObject _checkpoint2;



    void Start()
    {
        _currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
        _currentLevelText.text = _currentLevel.ToString();
        _nextLevelText.text = (_currentLevel + 1).ToString();
    }

    public void IncreaseLevel()
    {
        _currentLevel++;
    }
    
    public void IncreaseCheckpoint()
    {
        _currentCheckpoint++;
        if (_currentCheckpoint == 1)
            _checkpoint1.GetComponent<Image>().color = new Color32(255, 98, 47, 255);
        else if (_currentCheckpoint == 2)
            _checkpoint2.GetComponent<Image>().color = new Color32(255, 98, 47, 255);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
