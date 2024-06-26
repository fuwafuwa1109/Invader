using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHitCheck : MonoBehaviour
{
    GameManager gameman;
    Move move;
    SoundManager soundman;
    private Renderer objectRenderer;


    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
    }
    private void Start()
    {
        gameman = GameManager.Instance;
        move = Move.Instance;
        soundman = SoundManager.Instance;

        

    }
    public void OnTriggerEnter2D(Collider2D laser)
    {
        if (laser.transform.parent.gameObject.name == "Mylaser(Clone)")
        {
            Destroy(laser.transform.parent.gameObject);
            move.lasercheck = true;
        }
        
    }

    private void OnEnable()
    {
        if (transform.position.y > 9)
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
        else
        {
            objectRenderer.material.color = Color.red;
        }
    }

    private void Update()
    {
        switch (gameman.gamestat)
        {
            case "gameover":
                Destroy(gameObject.transform.parent.gameObject);
                return;
            default:
                break;
        }
    }
}
