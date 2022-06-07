using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Prize : MonoBehaviour
{
    [SerializeField] private int _prize;
    private TextMeshPro _textMeshPro;
    private MoneyManager _moneyManager;
    [SerializeField] private GameObject _levelCompletedCanvas;
    
    private bool _isTaken = false;
    void Start()
    {
        _moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        _textMeshPro = gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
        _textMeshPro.text = _prize.ToString();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_isTaken)
        {
            _moneyManager.AddMoney(_prize);
            _isTaken = true;
            _levelCompletedCanvas.SetActive(true);
        }
    }
}
