using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    [Header("Collision")]
    [SerializeField, Tooltip("Increase and deacrease")] float sizeChange = 0.5f;
    [SerializeField] float spawnProtectionDuration = 1f;
    [SerializeField] bool doSizeChange;
    [SerializeField] bool isFriendly;
    [Header("Wobble")]
    [SerializeField] float wobbleForce;
    [SerializeField] float maxSize;
    [SerializeField] float minSize;
    [SerializeField] bool doWobble;
    [Header("Move")]
    [SerializeField] float moveForce;
    [SerializeField] bool doRandomMove;
    [Header("Seek")]
    [SerializeField] float seekSpeed;
    [SerializeField, Tooltip("Tag")] string follow;
    [SerializeField] bool doSeek;
    [Header("Spin")]
    [SerializeField] float spinSpeed;
    [SerializeField] float rotationSize;
    [SerializeField] bool doSpin;
    [Header("Health")]
    [SerializeField] float lifeTime;

    Vector2 originalScale;
    bool canDamage;

    Rigidbody2D rigidbody2;
    PlayerMovement playerMovement;

    private void Awake()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Start()
    {
        originalScale = transform.localScale;

        StartCoroutine(SpawnProtection());
        LifeTime();
    }

    void FixedUpdate()
    {
        if (doWobble) 
        {
            Wobble();
        }
        if (doRandomMove)
        {
            RandomMove();
        }
        if (doSeek)
        {
            Seek();
        }
        if (doSpin) 
        { 
            Spin();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isFriendly && canDamage)
        {
            playerMovement.Death();
        }
        if (other.gameObject.CompareTag("RedEnemy") && doSizeChange)
        {
            transform.localScale += new Vector3(sizeChange, sizeChange, 0f);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Antidote") && gameObject.CompareTag("RedEnemy"))
        {
            Destroy(gameObject);
        }
    }

    void Wobble() 
    {
        float randomWobble = Random.Range(-wobbleForce, wobbleForce); //Makes it so that enemies can't look streched.
        transform.localScale = transform.localScale + new Vector3(randomWobble, randomWobble, 0f);

        if (transform.localScale.x > maxSize || transform.localScale.x < minSize) 
        { 
            transform.localScale = originalScale; 
        }
    }

    void RandomMove() 
    {
       rigidbody2.velocity += new Vector2(Random.Range(-moveForce, moveForce), Random.Range(-moveForce, moveForce)) * Time.fixedDeltaTime;
    }

    void Seek() 
    {
        GameObject moveTowardsThis = GameObject.FindWithTag(follow);

        if (GameObject.FindWithTag(follow)) 
        {   
            transform.position = Vector3.MoveTowards(transform.position, moveTowardsThis.transform.position, seekSpeed * Time.deltaTime);
        }
    }

    void Spin() 
    {
        transform.Translate(rotationSize, 0f, 0f);
        transform.Rotate(0f, 0f, spinSpeed, Space.Self);
    }

    public bool CanDie() 
    {
        return canDamage;
    }

    void LifeTime()
    {
        Destroy(gameObject, lifeTime);
    }

    IEnumerator SpawnProtection() 
    { 
        canDamage = false;

        yield return new WaitForSeconds(spawnProtectionDuration);

        canDamage = true;
    }
}
