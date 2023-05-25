using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SousChef : MonoBehaviour
{
    [SerializeField] private Character _characterSO;
    [SerializeField] private GameObject _pizzaSpawnPoint;
    [SerializeField] private AudioSource[] _meowSounds;
    [SerializeField] private AudioSource _angryMeowSound;
    [SerializeField] private AudioSource _sleepingSound;
    [SerializeField] private GameObject _sleepingTextPrefab;
    [SerializeField] private Transform _textParent;
    [SerializeField] private float _floatingSpeed = 1f;
    [SerializeField] private float _floatingHeight = 1f;
    [SerializeField] private float _floatingDuration = 4f;
    private GameObject _sleepingTextObject;
    private Coroutine _floatingCoroutine;
    private ProgressBar _progressBar;
    private Character.Characters _character;
    private float _completionTime;
    private int _orderIndex;
    private bool _isMakingPizza;
    private bool _isChefAwake;

    private const float _collisionThreshold = 5f;

    // Check to endure the variables in the inspector are assigned
    // private void OnValidate() 
    // {
    //     if (_characterSO == null)
    //     {
    //         Debug.Log("Sous Chef is missing a Character scriptable object.", this.gameObject);
    //     }
    //     if (_pizzaSpawnPoint == null)
    //     {
    //         Debug.Log("Sous Chef is missing a pizza spawn point prefab.", this.gameObject);
    //     }
    // }

    private void Awake()
    {
        _progressBar = GetComponentInChildren<ProgressBar>();
        _textParent = transform.Find("TextParent");
    }

    private void Start()
    {
        _character = _characterSO.character;
        _completionTime = _characterSO.completionTime;
        _progressBar.lerpColorTDelta = _characterSO.characteristicDeltaValue;
        _orderIndex = 0;
        _isMakingPizza = false;
        _isChefAwake = true;
        createPizzaAtStart();
    }

    private void Update()
    {
        onCreatingPizza();
        updateChefCharateristic();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.relativeVelocity.magnitude > _collisionThreshold)
        {
            onGettingHit();
        }
    }

    private void onCreatingPizza()
    {
        // When pizza is completed
        if (!_isMakingPizza & _isChefAwake)
        {
            _isMakingPizza = true;
            Timer.Create(onPizzaComplete, _completionTime, _progressBar);
        }
    }

    private void createPizzaAtStart()
    {
        OrderManager.Instance.orderList(_character)[_orderIndex].instantiate(_pizzaSpawnPoint.transform);
        _orderIndex++;
    }

    private void onPizzaComplete()
    {
        OrderManager.Instance.orderList(_character)[_orderIndex].instantiate(_pizzaSpawnPoint.transform); // Spawn the pizza gameobject
        _meowSounds[UnityEngine.Random.Range(0, _meowSounds.Length)].Play(); // Play SFX
        _isMakingPizza = false; 
        _orderIndex++;
        
        // Cycle through the same orderList to determine the type of pizza to make whent the reaching the end of orderList
        if (_orderIndex >= OrderManager.Instance.orderList(_character).Count)
        {
            _orderIndex = 0;
        }
    }

    private void updateChefCharateristic()
    {
        if (!_progressBar.isActive & _isChefAwake)
        {
            if (_character == 0) // Chef X
            {
                laziness();
            }
            else // Chef Y
            {
                // Chef Y characteristic
            }
        }
    }

    private void laziness()
    {
        _isChefAwake = false;
        _progressBar.gameObject.SetActive(false);
        _sleepingSound.Play();
        _sleepingTextObject = Instantiate(_sleepingTextPrefab, _textParent);
        _sleepingTextObject.transform.localPosition = Vector3.zero;

        _floatingCoroutine = StartCoroutine(FloatingTextRoutine());
    }

    private void onGettingHit()
    {
        _angryMeowSound.Play();
        if (!_isChefAwake)
        {
            _progressBar.gameObject.SetActive(true);
            if (_character == 0) // Chef X
            {
                _sleepingSound.Stop();
                if (_sleepingTextObject != null)
                {
                    if (_floatingCoroutine != null)
                    {
                        StopCoroutine(_floatingCoroutine);
                    }
                    Destroy(_sleepingTextObject);
                }
                _progressBar.resetActiveness();
            }
            else // Chef Y
            {
                
            }
        }
            
        _isChefAwake = true;
    }

        private IEnumerator FloatingTextRoutine()
    {
        while (true)
        {
            float elapsedTime = 0f;
            Vector3 initialPosition = _sleepingTextObject.transform.localPosition;

            while (elapsedTime < _floatingDuration)
            {
                float normalizedTime = elapsedTime / _floatingDuration;
                float yOffset = Mathf.Sin(normalizedTime * Mathf.PI) * _floatingHeight;
                Vector3 targetPosition = initialPosition + new Vector3(0f, yOffset, 0f);

                _sleepingTextObject.transform.localPosition = targetPosition;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Wait for a short delay before starting the next loop
            yield return new WaitForSeconds(0.5f);
        }
    }
}
