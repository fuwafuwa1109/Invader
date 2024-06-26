using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour
{
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private GameObject[] fire;
    [SerializeField] private GameObject Jet;
    [SerializeField] private GameObject triangle;

    private float enemytime;
    private float selecttime;
    private float triangletime;
    private bool selectcheck = false;
    private bool activecheck = true;
    private int enemytype = 0;
    SoundManager soundman;

    private void Awake()
    {
        soundman = SoundManager.Instance;
    }

    private void Start()
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            enemy[i].transform.GetChild(0).gameObject.SetActive(true);
            enemy[i].transform.GetChild(1).gameObject.SetActive(false);
        }

        for (int i = 0; i < fire.Length; i++)
        {
            fire[i].transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        enemytime += Time.deltaTime;

        if (enemytime > 0.5f)
        {
            for (int i = 0; i < enemy.Length; i++)
            {
                enemy[i].transform.GetChild(enemytype).gameObject.SetActive(false);
                enemytype = (enemytype + 1) % 2;
                enemy[i].transform.GetChild(enemytype).gameObject.SetActive(true);
                enemytime = 0;
            }

            if (activecheck)
            {
                activecheck = false;
                for (int i = 0; i < fire.Length; i++)
                {
                    fire[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                Jet.transform.Translate(Vector3.left * 0.2f);
            }
            else
            {
                activecheck = true;
                for (int i = 0; i < fire.Length; i++)
                {
                    fire[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                Jet.transform.Translate(Vector3.right * 0.2f);

            }


        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            selectcheck = true;
            soundman.PlaySound("Decide");
        }

        if (selectcheck)
        {
            selecttime += Time.deltaTime;
            triangletime += Time.deltaTime;

            if (triangletime > 0.1f && triangle.activeSelf)
            {
                triangle.SetActive(false);
                triangletime = 0;
            }
            else if (triangletime > 0.1f)
            {
                triangle.SetActive(true);
                triangletime = 0;
            }

            if (selecttime > 1)
            {
                triangle.SetActive(true);
                triangletime = 0;
                selecttime = 0;
                selectcheck = false;
                SceneManager.LoadScene(0);
            }
        }
    }
}
