using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erase : MonoBehaviour
{
    GameManager gameman;
    private Renderer objectRenderer;

    private float erasetime;
    void Start()
    {
        gameman = GameManager.Instance;
        objectRenderer = GetComponentInChildren<Renderer>();
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

    void Update()
    {
        erasetime += Time.deltaTime;

        if (erasetime > 0.4f)
        {
            Destroy(gameObject);
        }
    }
}
