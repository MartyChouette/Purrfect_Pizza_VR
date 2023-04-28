using System.Collections;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GeneralTimer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;
    public Transform clockTransform;

    [Header("Timer Settings")]
    public float currentTime;
    private TimeSpan timePlaying;

    // [Header("Audio Settings")]
    // private AudioClip loseSound;
    // private AudioSource audioSource;

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;

    //[Header("Shake Settings")]
    //private float shakeIntensity = 0.1f;
    private Vector3 initialPosition;

    private void Awake()
    {
        // loseSound = (AudioClip)Resources.Load("lose");
        // audioSource = GetComponent<AudioSource>();
        initialPosition = clockTransform.position;
    }

    private void Start()
    {
       
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (hasLimit && currentTime <= timerLimit)
        {
            currentTime = timerLimit;
            setTimerText();
            timerText.color = Color.red;
            enabled = false;
            LevelManager.Instance.onTimeout();
            // StartCoroutine(PlaySound());
        }

        // else if (currentTime <= 5f) // Adjust the threshold for when to start the shake animation
        // {
        //     StartCoroutine(ShakeClock());
        // }


        setTimerText();
    }

    private void setTimerText()
    {
        timePlaying = TimeSpan.FromSeconds(currentTime);
        timerText.text =  timePlaying.ToString("mm':'ss':'ff");
    }

//    private IEnumerator ShakeClock()
//     {
//         Vector3 originalPosition = clockTransform.localPosition; // Store the original position of the parent game object
//         float shakeTimer = 0f;
//         while (shakeTimer < 5)
//         {
//             float offsetX = UnityEngine.Random.Range(-1f, 1f) * shakeIntensity;
//             float offsetY = UnityEngine.Random.Range(-1f, 1f) * shakeIntensity;

//             // Apply the shake offset to the parent game object
//             clockTransform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0f);

//             shakeTimer += Time.deltaTime;
//             yield return null;
//         }

//         // Reset the position of the parent game object
//         clockTransform.localPosition = originalPosition;
//     }


    // IEnumerator PlaySound()
    // {
    //     audioSource.clip = loseSound;
    //     audioSource.Play();
    //     yield return new WaitForSeconds(2);
    //     SceneManager.LoadScene("TimeUp");
    // }


}