using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    private float speed = 12f;
    private int lasernum;
    [SerializeField] private GameObject[] children;
    GameManager gameman;

    
    private void Start()
    {
        gameman = GameManager.Instance;
        // 最初にすべての子オブジェクトを無効化
        foreach (var child in children)
        {
            if (child != null)
            {
                child.SetActive(false);
            }
        }
        // 初期の子オブジェクトを有効化
        if (children.Length > 0 && children[0] != null)
        {
            children[0].SetActive(true);
        }

        lasernum = 0;
        
    }

    private void Update()
    {

        switch (gameman.gamestat)
        {
            case "update":
                gameObject.transform.Translate(Vector3.down * speed * Time.deltaTime);

                

                children[lasernum].SetActive(false);
                lasernum = (lasernum + 1) % children.Length;
                children[lasernum].SetActive(true);

                
                break;

            case "gameover":
            case "gotonext":
                Destroy(gameObject);
                return;

            default:
                break;
        }


        


    }



}
