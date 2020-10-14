using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyScript : MonoBehaviour
{
    public Transform enemyProjectile, firePoint;
    
    public float speed  = 2f;
    private Vector2 lastUpdatePos = Vector2.zero;
    private Vector2 dist;
    public Animator animator;
    public float currentSpeed;
    public bool isColliding = false;
    
    // Start is called before the first frame update
    void Start()
    {
        // float delay = Random.Range(1f, 3f);
        // float rate = Random.Range(1f, 3f);
        float delay = 0f;
        float rate = 0.5f;
        InvokeRepeating(nameof(Fire), delay, rate);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position;
        if (Random.Range(0, 100) > 40)
        {
            position = Vector2.MoveTowards(transform.position, PlayerScript.S.transform.position,
                speed * Time.deltaTime);
            transform.position = position;
        }
        else
        {
            position = transform.position;
        }

        dist = position - lastUpdatePos;
        currentSpeed = dist.magnitude / Time.deltaTime;
        lastUpdatePos = position;
        animator.SetBool("standing", isColliding && currentSpeed < 10);
    }

    private void Fire()
    {
        
        if (Random.Range(0, 100) > 40)
        {
            Instantiate(enemyProjectile, firePoint.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isColliding = true;
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        isColliding = false;
    }
}
