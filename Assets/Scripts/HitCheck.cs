using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitCheck : MonoBehaviour
{
    private float speed = 23f;
    GameManager gameman;
    Move move;
    SoundManager soundman;

    private int[,] bombarray = new int[5, 5]
    {
        {1,0,1,0,1},
        {0,1,0,1,0},
        {1,0,1,0,1},
        {0,1,0,1,0},
        {0,0,1,0,0}};

    private void Start()
    {
        gameman = GameManager.Instance;
        move =　Move.Instance;
        soundman = SoundManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                if (gameman.enemysurvive[i, j] == 1)
                {
                    if (collider.gameObject == gameman.EnemyBoard[i, j].transform.GetChild(0).gameObject
                        || collider.gameObject == gameman.EnemyBoard[i, j].transform.GetChild(1).gameObject)
                    {
                        gameman.enemysurvive[i, j] = 0;
                        Instantiate(gameman.erase, collider.transform.position, Quaternion.identity);
                        Destroy(collider.transform.parent.gameObject);
                        move.lasercheck = true;
                        soundman.PlaySound("EnemyDamage");
                        if (i == 1 || i == 0)
                        {
                            gameman.score += 10;
                            gameman.scoretext.text = gameman.score.ToString("D6");
                        }
                        else if (i == 2 || i == 3)
                        {
                            gameman.score += 20;
                            gameman.scoretext.text = gameman.score.ToString("D6");
                        }
                        else
                        {
                            gameman.score += 30;
                            gameman.scoretext.text = gameman.score.ToString("D6");
                        }

                        gameman.enemycount--;
                        if (gameman.enemycount == 0)
                        {
                            soundman.PlaySound("stagewin");
                            gameman.gamestat = "gotonext";
                        }
                        return;

                        

                    }
                }
            }
    

        }

        for (int x = 0; x < 23; x++)
        {
            for (int y = 0; y < 17; y++)
            {
                if (collider.gameObject.transform.parent.gameObject == gameman.wallobject1[y,x])
                {
                    wallbreak(gameman.wallobject1, gameman.wallboard1, x, y);
                    gameman.bombset = Instantiate(gameman.Bomb, gameObject.transform.position, Quaternion.identity);
                    Destroy(gameObject.transform.parent.gameObject);
                    soundman.PlaySound("JetBomb");
                    move.lasercheck = true;
                    return;
                }
                else if (collider.gameObject.transform.parent.gameObject == gameman.wallobject2[y, x])
                {
                    wallbreak(gameman.wallobject2, gameman.wallboard2, x, y);
                    gameman.bombset = Instantiate(gameman.Bomb, gameObject.transform.position, Quaternion.identity);
                    Destroy(gameObject.transform.parent.gameObject);
                    soundman.PlaySound("JetBomb");
                    move.lasercheck = true;
                    return;
                }
                else if (collider.gameObject.transform.parent.gameObject == gameman.wallobject3[y, x])
                {
                    wallbreak(gameman.wallobject3, gameman.wallboard3, x, y);
                    gameman.bombset = Instantiate(gameman.Bomb, gameObject.transform.position, Quaternion.identity);
                    Destroy(gameObject.transform.parent.gameObject);
                    soundman.PlaySound("JetBomb");
                    move.lasercheck = true;
                    return;
                }
                else if(collider.gameObject.transform.parent.gameObject == gameman.wallobject4[y, x])
                {
                    wallbreak(gameman.wallobject4, gameman.wallboard4, x, y);
                    gameman.bombset = Instantiate(gameman.Bomb, gameObject.transform.position, Quaternion.identity);
                    Destroy(gameObject.transform.parent.gameObject);
                    soundman.PlaySound("JetBomb");
                    move.lasercheck = true;
                    return;
                }

            }
        }

        if (collider.gameObject.transform.parent.gameObject == gameman.ufoset)
        {
            Instantiate(gameman.erase, collider.transform.position, Quaternion.identity);
            Destroy(collider.transform.parent.gameObject);
            Destroy(gameObject.transform.parent.gameObject);
            move.lasercheck = true;
            gameman.ufoLorR = gameman.ufoLorR * -1;
            gameman.score += 200;
            gameman.scoretext.text = gameman.score.ToString("D6");
            soundman.PlaySound("EnemyDamage");
        }
    }
    private void Update()
    {
        switch (gameman.gamestat)
        {
            case "update":
                gameObject.transform.Translate(Vector3.up * speed * Time.deltaTime);
                if (gameObject.transform.position.y > 14)
                {
                    gameman.bombset = Instantiate(gameman.Bomb, gameObject.transform.position, Quaternion.identity);
                    Destroy(gameObject.transform.parent.gameObject);
                    move.lasercheck = true;

                }
                break;

            
                


            case "gameover":
            case "gotonext":
                Destroy(gameObject.transform.parent.gameObject);
                return;

            default:
                break;
        }
    }


    private void wallbreak(GameObject[,] wallobject, int[,] wallboard, int wallx, int wally)
    {


        for (int x = wallx - 2; x < wallx + 3; x++)
        {
            for (int y = wally-4; y < wally+1; y++)
            {
                if (x >= 0 && x < 23 && y >= 0 && y < 17)
                {
                    if (wallboard[y, x] == 1 && bombarray[y - wally + 4, x + 2 - wallx] == 1)
                    {
                        wallboard[y, x] = 0;
                        Destroy(wallobject[y, x]);
                    }
                }
            }
        }
    }


}
