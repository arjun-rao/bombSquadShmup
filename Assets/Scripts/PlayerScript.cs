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
    private bool _canDefuse = false, _canHeal = false, _canRepair = false;
    private float _defuseTimer = 0, _healTimer = 0, _repairTimer = 0;

    public static PlayerScript S;
    
    private void Awake()
    {
        S = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth();
    }

    private void UpdateHealth()
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

        if (Input.GetKey(KeyCode.Z) && _canDefuse)
        {
            _defusing = 1f;
            _defuseTimer += Time.deltaTime;
            if (_defuseTimer > 2)
            {
                _defuseTimer = 0;
                // increase defuse percentage.
                GameManagerScript.S.Defuse();
            }
        }
        else
        {
            _defuseTimer = 0;
            _defusing = 0f;
        }
        
        if (Input.GetKey(KeyCode.Z) && _canHeal)
        {
            _defusing = 1f;
            _healTimer += Time.deltaTime;
            if (_healTimer > 2)
            {
                _healTimer = 0;
                // increase health for player percentage.
                if (health < 1f)
                {
                    health += 0.3f;
                    UpdateHealth();
                }
                
            }
        }
        else
        {
            _healTimer = 0;
            _defusing = 0f;
        }
        
        if (Input.GetKey(KeyCode.Z) && _canRepair)
        {
            _defusing = 1f;
            _repairTimer += Time.deltaTime;
            if (_repairTimer > 2)
            {
                _repairTimer = 0;
                // increase health for vehicle
                VehicleScript.S.RepairVehicle();

            }
        }
        else
        {
            _repairTimer = 0;
            _defusing = 0f;
        }


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
            UpdateHealth();
            if (health <= 0)
            {
                GameManagerScript.S.GameOver();
            }
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("bombsquad"))
        {
            _canDefuse = true;
        }
        if (other.gameObject.CompareTag("health"))
        {
            _canHeal = true;
        }
        if (other.gameObject.CompareTag("vehicle"))
        {
            _canRepair = true;
        }
    }
    
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("bombsquad"))
        {
            _canDefuse = false;
        }
        
        if (other.gameObject.CompareTag("health"))
        {
            _canHeal = false;
        }
        if (other.gameObject.CompareTag("vehicle"))
        {
            _canRepair = false;
        }
    }
}

