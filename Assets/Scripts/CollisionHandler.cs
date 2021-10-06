using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    public ParticleSystem expolisionParticle;

    private void Update()
    {

    }
    void OnCollisionEnter(Collision other)
    {

        Debug.Log(this.name + "Collided with--" + other.gameObject.name);

    }
    private void OnTriggerEnter(Collider other)
    {
        StartCrashSeq();
        Debug.Log($"{this.name} **triggered by** {other.gameObject.name}");
    }

    void StartCrashSeq()
    {
        expolisionParticle.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<PlayerControls>().enabled = false;
        Invoke("ReloadScene", 1f);



    }

    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}

