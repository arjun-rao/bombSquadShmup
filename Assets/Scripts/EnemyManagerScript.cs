using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyManagerScript : MonoBehaviour {
    public Transform enemy;
    public Color[] brickColors;

    public float xSpacing, ySpacing;
    public float xOrigin, yOrigin;
    public int numRows, numColumns;
    public Text killText;
    public static EnemyManagerScript S;
    public int maxEnemies = 25;
    private int enemies = 0;
    public int kills = 0;

    public float speed = 2f;
    public float amplitude = 0.5f;

    private void Awake()
    {
        S = this;
    }

    public void AddKill()
    {
        kills++;
        enemies -= 1;
        killText.text = $"{kills}";
    }

    // Start is called before the first frame update
    void Start()
    {
        // for (int i = 0; i < numRows; i++) {
        //     for (int j = 0; j < numColumns; j++) {
        //         Transform go = Instantiate(enemy);
        //         go.transform.parent = this.transform;
        //         
        //         Vector2 loc = new Vector2(xOrigin + (i * xSpacing), yOrigin - (j * ySpacing));
        //         go.transform.position = loc;
        //         
        //         SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        //     }
        // }
        StartCoroutine(nameof(SpawnEnemies));
    }

    private void Update()
    {
        
    }

    private IEnumerator SpawnEnemies()
    {
     
        // Randomly spown some enemies. Change 3 to public variable on script.
        while(true)
        {
            if (enemies < maxEnemies)
            {
                int enemyCount = Random.Range(0, 2);
                for (int i = 0; i < enemyCount; i++)
                {
                    Transform go = Instantiate(enemy);
                    go.transform.parent = this.transform;

                    Vector2 loc = new Vector2(Random.Range(-10, 10) + xSpacing, yOrigin);
                    go.transform.position = loc;
                    enemies++;
                }
            }

            yield return new WaitForSeconds(1);
        }

        

    }
}
