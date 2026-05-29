using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandeler : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 2f;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isControllable = true;
    bool isCollidable = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }
    void Update()
    {
        RespondToDebugKeys();    
    }

    void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
            LoadNextLevel();
        else if (Keyboard.current.cKey.wasPressedThisFrame)
            isCollidable = !isCollidable;
    }

    void OnCollisionEnter(Collision other)
    {
        if (!isControllable || !isCollidable) { return; }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSqeuence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }
    private void StartSuccessSqeuence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX, 0.3f);
        successParticles.Play();
        Invoke("LoadNextLevel", LevelLoadDelay);
        GetComponent<Movement>().enabled = false;
    }
    void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX, 0.3f);
        crashParticles.Play();
        Invoke("RealoadLevel", LevelLoadDelay);
        GetComponent<Movement>().enabled = false;
    }
    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
    void RealoadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
