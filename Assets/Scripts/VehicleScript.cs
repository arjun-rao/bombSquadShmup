using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleScript : MonoBehaviour
{
    public float speed = 20f;
    public float maxHealth = 80;
    public float health = 80;
    private Vector2 lastUpdatePos = Vector2.zero;
    private Vector2 dist;
    public Transform Explosion;
    private bool isAlive = true;
    public float currentSpeed;
    public static VehicleScript S;
    public Sprite partialDamage, fullDamage;

    private void Awake()
    {
        S = this;
    }
    private void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        Vector2 targetPosition = new Vector2(8, position.y);
        position = Vector2.MoveTowards(position, targetPosition, speed * Time.deltaTime);
        transform.position = position;
        dist = position - lastUpdatePos;
        currentSpeed = dist.magnitude / Time.deltaTime;
        lastUpdatePos = position;
    }

    private void DestroySelf()
    {
        Instantiate(Explosion, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    private void CheckHealth()
    {
        if (health < 0.5 * maxHealth && health > 0)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = partialDamage;
        } else if (health <= 0 && isAlive)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = fullDamage;
            isAlive = false;
            Invoke(nameof(DestroySelf), 3);
        }
    }
    public void ReduceHealth()
    {
        health -= 1;
        CheckHealth();
        
    }
    
    public void RepairVehicle()
    {
        if (health < maxHealth)
        {
            health += 10;
        }
        else
        {
            health = maxHealth;
        }

        CheckHealth();
        
    }
}
