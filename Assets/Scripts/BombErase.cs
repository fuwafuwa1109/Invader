using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombErase : MonoBehaviour
{
    GameManager gameman;
    Renderer objectRenderer;
    private float bombtime = 0;

    private void Awake()
    {
        objectRenderer = GetComponentInChildren<Renderer>();

    }
    private void Start()
    {
        gameman = GameManager.Instance;
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

    private void Update()
    {
        bombtime += Time.deltaTime;
        if (bombtime > 0.3)
        {
            Destroy(gameObject);
            bombtime = 0;
        }
    }
}
