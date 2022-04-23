using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(AudioSource))]
public class CollisionHandler : MonoBehaviour
{
    private const string FINISH_TAG = "Finish";
    private const string FRIENDLY_TAG = "Friendly";

    [SerializeField] private float loadDelay = 1.0f;
    [SerializeField] private AudioClip crashSfx = null;
    [SerializeField] private AudioClip successSfx = null;
    [SerializeField] private ParticleSystem successVfx = null;
    [SerializeField] private ParticleSystem crashVfx = null;

    private Movement myMovement = null;
    private AudioSource myAudioSource = null;

    private bool isTransitioning = false;
    private bool collisionDisabled = false;

    private void Awake()
    {
        myMovement = GetComponent<Movement>();
        myAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();    
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled) { return; }

        switch (collision.gameObject.tag)
        {
            case FRIENDLY_TAG:
                break;
            case FINISH_TAG:
                StartNextLevelSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        isTransitioning = true;
        myMovement.enabled = false;

        myAudioSource.Stop();
        myAudioSource.PlayOneShot(crashSfx);
        crashVfx.Play();

        Invoke("ReloadLevel", loadDelay); //ReloadLevel();
    }

    private void StartNextLevelSequence()
    {
        isTransitioning = true;
        myMovement.enabled = false;

        myAudioSource.Stop();
        myAudioSource.PlayOneShot(successSfx);
        successVfx.Play();

        Invoke("LoadNextLevel", loadDelay); //LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        int currentSceneId = SceneManager.GetActiveScene().buildIndex;
        int nextSceneId = (currentSceneId + 1) % SceneManager.sceneCountInBuildSettings;

        SceneManager.LoadScene(nextSceneId);
    }

    private void ReloadLevel()
    {
        int currentSceneId = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneId);
    }
}
