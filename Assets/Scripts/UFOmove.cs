using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOmove : MonoBehaviour
{
    GameManager gameman;
    Move move;
    SoundManager soundman;

    private float speed = 4.5f;
    void Awake()
    {
        gameman = GameManager.Instance;
        move = Move.Instance;
        soundman = SoundManager.Instance;


    }

    private void OnEnable()
    {
        soundman.SetLoop("UFO", true);
        soundman.PlaySound("UFO");
        soundman.SetVolume("UFO", 0.7f);
    }

    void Update()
    {
        switch (gameman.gamestat)
        {
            case "update":
                if (gameman.ufoLorR == -1)
                {
                    gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime);
                    if (gameObject.transform.position.x < move.xleftlim)
                    {
                        gameman.ufoLorR = 1;
                        Destroy(gameObject);
                    }
                }

                if (gameman.ufoLorR == 1)
                {
                    gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
                    if (gameObject.transform.position.x > move.xrightlim)
                    {
                        gameman.ufoLorR = -1;
                        Destroy(gameObject);
                    }
                }
                break;

            case "gameover":
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
    private void OnDestroy()
    {
        soundman.StopSound("UFO");
    }
}

    
