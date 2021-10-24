using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    public GameObject deathFX;
    public GameObject hitVFX;
    GameObject parentGameObject;
    ScoreBoard scoreBoard;
    [SerializeField] int enemyHealth = 4;
    [SerializeField] int scorePerHit = 2;
    [SerializeField] int scorePerDeath = 15;



    private void OnParticleCollision(GameObject other)
    {
        KillEnemy();
        ProcessHit();
    }

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnRuntime");
        AddRigidBody();
    }


    private void AddRigidBody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void ProcessHit()
    {
        scoreBoard.IncraseScore(scorePerHit);


    }
    private void KillEnemy()
    {

        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);

        vfx.transform.parent = parentGameObject.transform;
        enemyHealth--;
        Debug.Log(this.name + " " + enemyHealth);
        if (enemyHealth < 1)
        {
            GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity); //düşman prefab olmadığı için kod yoluyla vfxi içine yerleştirdik.
            fx.transform.parent = parentGameObject.transform;
            Destroy(gameObject);
            scoreBoard.IncraseScore(scorePerDeath);
            Debug.Log(this.name + "get point of death: " + scorePerDeath);
        }
    }



}




