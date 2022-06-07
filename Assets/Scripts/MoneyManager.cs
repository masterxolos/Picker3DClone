using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private int _currentMoney;

    [SerializeField] private TextMeshProUGUI _moneyText;

    private static MoneyManager _instance;
    public static MoneyManager Instance
    {
        get { return _instance; }
        set => _instance = value;
    }

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        UpdateMoney();
    }
    public void AddMoney(int amount)
    {
        _currentMoney += amount;
        UpdateMoney();
    }

    private void UpdateMoney()
    {
        _moneyText.text = _currentMoney.ToString();
    }
}
