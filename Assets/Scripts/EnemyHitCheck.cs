using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitCheck : MonoBehaviour
{
    GameManager gameman;
    Move move;
    Renderer objectRenderer;
    SoundManager soundman;


    private int[,] crush = new int[6, 7]{
        {0,0,1,1,1,0,0},
        {0,0,1,1,1,0,0},
        {0,1,1,1,1,1,0},
        {1,0,1,0,1,0,1},
        {0,1,0,1,0,1,0},
        {1,0,1,0,1,0,1},};

    string clone = "(Clone)";
    private void Start()
    {
        gameman = GameManager.Instance;
        move = Move.Instance;
        ;    }


    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        soundman = SoundManager.Instance;
    }

    private void OnEnable()
    {
        if (transform.position.y > 13)
        {
            objectRenderer.material.color = new Color(0.694f, 0.0f, 1.0f); // 紫
        }
        else if (transform.position.y > 9)
        {
            objectRenderer.material.color = new Color(0.5f, 1.0f, 0.0f); // 黄緑
        }
        else if (transform.position.y > 7)
        {
            objectRenderer.material.color = Color.cyan; // 水色
        }
        else if (transform.position.y > 5)
        {
            objectRenderer.material.color = new Color(0.694f, 0.0f, 1.0f); // 紫
        }
        else if (transform.position.y > 3)
        {
            objectRenderer.material.color = Color.yellow;
        }
        else if (transform.position.y > 0)
        {
            objectRenderer.material.color = Color.red;
        }
        else if (transform.position.y > -2)
        {
            objectRenderer.material.color = Color.cyan; // 水色
        }
        else
        {
            objectRenderer.material.color = Color.red;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.name == "Square(Clone)")
        {
            Instantiate(gameman.Bomb, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject.transform.parent.gameObject);
            return;
        }

        if (collider.gameObject.transform.parent.gameObject == gameman.jetset)
        {
            gameman.life--;
            Instantiate(gameman.Bomb, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject.transform.parent.gameObject);
            gameman.damage = true;
            soundman.PlaySound("JetBomb");
            return;
        }

        if (collider.gameObject.transform.parent.gameObject.name == gameman.mylaser.name + clone)
        {
            move.lasercheck = true;
            Instantiate(gameman.Bomb, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject.transform.parent.gameObject);
            Destroy(collider.gameObject.transform.parent.gameObject);
            soundman.PlaySound("JetBomb");
            return;
            
        }

        for (int x = 0; x < 23; x++)
        {
            for (int y = 0; y < 17; y++)
            {
                if (collider.gameObject.transform.parent.gameObject == gameman.wallobject1[y,x])
                {
                    wallbreak(gameman.wallobject1, gameman.wallboard1, x, y);
                    Instantiate(gameman.Bomb, gameObject.transform.position, Quaternion.identity);
                    Destroy(gameObject.transform.parent.gameObject);
                    soundman.PlaySound("JetBomb");
                    return;
                }

                if (collider.gameObject.transform.parent.gameObject == gameman.wallobject2[y, x])
                {
                    wallbreak(gameman.wallobject2, gameman.wallboard2, x, y);
                    Instantiate(gameman.Bomb, gameObject.transform.position, Quaternion.identity);
                    Destroy(gameObject.transform.parent.gameObject);
                    soundman.PlaySound("JetBomb");
                    return;
                }

                if (collider.gameObject.transform.parent.gameObject == gameman.wallobject3[y, x])
                {
                    wallbreak(gameman.wallobject3, gameman.wallboard3, x, y);
                    Instantiate(gameman.Bomb, gameObject.transform.position, Quaternion.identity);
                    Destroy(gameObject.transform.parent.gameObject);
                    soundman.PlaySound("JetBomb");
                    return;
                }

                if (collider.gameObject.transform.parent.gameObject == gameman.wallobject4[y, x])
                {
                    wallbreak(gameman.wallobject4, gameman.wallboard4, x, y);
                    Instantiate(gameman.Bomb, gameObject.transform.position, Quaternion.identity);
                    Destroy(gameObject.transform.parent.gameObject);
                    soundman.PlaySound("JetBomb");
                    return;
                }
            }
            
        }

        

        
        

        
    }

    private void wallbreak(GameObject[,] wallobject, int[,] wallboard, int wallx, int wally)
    { 
        
        
        for (int x = wallx-3; x < wallx+4; x++)
        {
            for (int y = wally; y < wally+6; y++)
            {
                if (x>=0 && x<23 && y >= 0 && y<17)
                {
                    if (wallboard[y,x] == 1 && crush[y-wally,x+3-wallx] == 1)
                    {
                        wallboard[y,x] = 0;
                        Destroy(wallobject[y,x]);
                    }
                }
            }
        }
    }

    
}
