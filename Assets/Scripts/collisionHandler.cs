
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class collisionHandler : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 1.3f;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip successAudio;
    [SerializeField] ParticleSystem SuccessParticle;
    [SerializeField] ParticleSystem CrashParticle;
    AudioSource myRocketAudio;

    bool isTransitioning = false;
     void Start()
    {
        myRocketAudio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {

        if (isTransitioning)
            return;

        switch (other.gameObject.tag)
        {
            case "Finish":
                //Debug.Log("Game finished");
                
                StartSuccessSequence();
                break;

            case "Friendly":
                Debug.Log("Friendly"); // for launch pad
                break;
            default:
                CrashSequenceStart();
                break;
        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        myRocketAudio.Stop();
        myRocketAudio.PlayOneShot(successAudio); // second parameter volume scale
        SuccessParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", LevelLoadDelay);
    }

    void CrashSequenceStart()
    {
        isTransitioning = true;
        // stop all sound before playing next one
        myRocketAudio.Stop();
        myRocketAudio.PlayOneShot(crashAudio);
        CrashParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("SceneReload", LevelLoadDelay);
    }
     void LoadNextLevel()
    {
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int NextSceneIndex = currentSceneIndex + 1;
        // if reach to end of the level loop from start
        if (NextSceneIndex == SceneManager.sceneCountInBuildSettings)
            NextSceneIndex = 0;
            SceneManager.LoadScene(NextSceneIndex);
    }
    void SceneReload()
    {
        //SceneManager.LoadScene(0);// string reference can also be used by scene name
        // or we can reload active scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
