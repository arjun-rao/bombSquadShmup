using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
    private float     xPos, yPos;
    public float      speed = 5f;
    public Rigidbody2D rb;
    public Transform projectile, firePoint;
    public KeyCode fireKey;
    public float health = 1f;
    public Image healthBar;
    private Vector2 _movement;
    public Animator animator;
    private float _walking, _defusing;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.fillAmount = health;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + _movement * (speed * Time.fixedDeltaTime));
    }

    // Update is called once per frame
    void Update()
    {

        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        if (_movement.y == 0)
        {
            _walking = _movement.x == 0 ? 0f : 1f;
        }
        else
        {
            _walking = _movement.y;
        }
        
        _defusing = Input.GetKey(KeyCode.Z) ? 1f : 0f;
        animator.SetFloat("Walking", _walking);
        animator.SetFloat("Defusing", _defusing);
        animator.SetFloat("Idling", ((_movement.magnitude < 0.01f)&&(_defusing ==0))? 2f: 0f );
        
        // Shoot
        if (Input.GetKeyDown(fireKey))
        {
            Instantiate(this.projectile, firePoint.position, Quaternion.identity);
            
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemyProjectile"))
        {
            Destroy(other.gameObject);
            health -= 0.1f;
            healthBar.fillAmount = health;
            // Do game over check here.
        }
    }
}

