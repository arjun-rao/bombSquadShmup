using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float               speed = 2f;
    public int direction;
    private Rigidbody2D        rb;
    public Transform Explosion;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("Launch");
    }

    
    private IEnumerator Launch() {
        //yield return new WaitForSeconds(1);
        //rb.AddForce(transform.right * -1);
        rb.AddForce(transform.up * (speed * direction));
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
           case "Player":
            {
                Debug.Log("Hit Player");
                Destroy(this.gameObject);
                Instantiate(Explosion, this.transform.position, Quaternion.identity);
                // hit player.
                break;
            }
           
           case "wall":
           {
               Destroy(this.gameObject);
               break;
           }
           
           case "vehicle":
           {
               Destroy(this.gameObject);
               Instantiate(Explosion, this.transform.position, Quaternion.identity);
               VehicleScript.S.ReduceHealth();
               break;
           }
           case "bombsquad":
           {
               
               Instantiate(Explosion, this.transform.position, Quaternion.identity);
               Destroy(other.gameObject);
               Destroy(this.gameObject);
               GameManagerScript.S.GameOver();
               break;
           }
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "wall":
            {
                Destroy(this.gameObject);
                break;
            }

            case "trench":
            {
                Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                break;
            }

            case "enemy":
            {
                Instantiate(Explosion, this.transform.position, Quaternion.identity);
                EnemyManagerScript.S.AddKill();
                Destroy(other.gameObject);
                Destroy(this.gameObject);
                break;
            }
            
            case "vehicle":
            {
                Destroy(this.gameObject);
                break;
            }
        }
        
    }
}
