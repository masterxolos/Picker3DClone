using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int speed = 5;
    [SerializeField] private float controlSensitivity = 5f;
    private bool _isTouching;
    [SerializeField] private bool _isMoving = true;
    private bool _isCheckpoint1Passed = false;
    private bool _isCheckpoint2Passed = false;
    private Rigidbody _rb;
    
    [SerializeField]private List<float> taps = new List<float> ();
    private float tapsPerSecond;
    private bool _isPrizeZone = false; 
    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        
        if (_isPrizeZone)
        {
            _rb.velocity += new Vector3(0,0,tapsPerSecond*0.3f);
        }
        else if (_isTouching && _isMoving)
        {
            _rb.velocity = new Vector3(Input.GetAxis("Mouse X") * controlSensitivity, 0, speed);
            //transform.position = new Vector3(Input.GetAxis("Mouse X") * controlSensitivity, transform.position.y, transform.position.z + speed);
        }
        else if (_isMoving)
        {
            Debug.Log("running");
            _rb.velocity = Vector3.forward * speed ;
            //transform.position += Vector3.forward * speed *Time.deltaTime;
        }

    }

    void GetInput()
    {
        if (Input.GetMouseButton(0))
        {
            _isTouching = true;
        }
        else
        {
            _isTouching = false;
        }

        if (_isPrizeZone)
        {
            slider.gameObject.SetActive(true);
            if (Input.GetMouseButtonUp(0))
            {
                taps.Add(Time.timeSinceLevelLoad);
            }
            for (int i = 0; i < taps.Count; i++)
            {
                if (taps[i] <= Time.timeSinceLevelLoad - 1)
                {
                    taps.RemoveAt(i);
                }
            }
            tapsPerSecond = taps.Count;
            FillSlider();
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("checkpoint1"))
        {
            if (!_isCheckpoint1Passed)
            {
                Debug.Log("durdu");
                _isMoving = false;
                _rb.velocity = Vector3.zero;
                _isCheckpoint1Passed = true;
            }
        }
        else if (other.gameObject.CompareTag("checkpoint2"))
        {
            if (!_isCheckpoint2Passed)
            {
                Debug.Log("durdu");
                _isMoving = false;
                _rb.velocity = Vector3.zero;
                _isCheckpoint2Passed = true;
            }
        }
        else if (other.gameObject.CompareTag("prizeRamp"))
        {
            _isPrizeZone = true;
            _rb.mass = 5;
        }
        else if (other.gameObject.CompareTag("prizeRampEnd"))
        {
            _rb.constraints = RigidbodyConstraints.FreezeRotation;
            gameObject.transform.rotation = Quaternion.Euler(0,0,0);
            _isPrizeZone = false;
            _isMoving = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("levelend"))
        {
            
        }
    }

    public void ContinueAfterCheckpoint()
    {
        _isMoving = true;
        Debug.Log("hareket ediyor");
    }

    private void FillSlider()
    {
        slider.value = tapsPerSecond;
        fill.fillAmount = slider.value;
    }
}

