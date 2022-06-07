using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private TextMesh _CheckpointText;
    [SerializeField] private int _neededAmount;
    [SerializeField] private int _currentBallAmount;
    [SerializeField] private GameObject failCanvas;

    [Serializable] public class OnCheckpointPassed : UnityEvent{ }

    public OnCheckpointPassed CheckpointPassed ;
    private bool isChecked= false;
    private bool isPassed= false;
    [SerializeField] private GameObject ground; 
    [SerializeField] private GameObject leftHand; 
    [SerializeField] private GameObject rightHand; 

    // Start is called before the first frame update
    void Start()
    {
        _CheckpointText.text = _currentBallAmount + " / " + _neededAmount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isChecked)
        {
            StartCoroutine(CheckStatus());
            isChecked = true;
            Debug.Log("started");
        }

        if (!isPassed)
        {
            _currentBallAmount++;
            _CheckpointText.text = _currentBallAmount + " / " + _neededAmount;
        }
        
    }

    private IEnumerator CheckStatus()
    {
        yield return new WaitForSeconds(3);
        if (_currentBallAmount >= _neededAmount)
        {
            ground.transform.DOLocalMove(new Vector3(0.2041325f, -0.04700001f,2.048583f), 1);
            leftHand.transform.DORotate(new Vector3(0, 0, 90), 1);
            rightHand.transform.DORotate(new Vector3(0, 0, -90), 1);
            yield return new WaitForSeconds(1.2f);
            _CheckpointText.gameObject.SetActive(false);
            isPassed = true;
            Destroy(gameObject.transform.GetChild(0).gameObject);
            Destroy(gameObject.transform.GetChild(1).gameObject);
            Destroy(gameObject.transform.GetChild(2).gameObject);
            
            
            
            CheckpointPassed.Invoke();
            
        }
        else
        {
            failCanvas.SetActive(true);
        }
        
    }
    
}
