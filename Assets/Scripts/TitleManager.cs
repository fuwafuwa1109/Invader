using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private TMP_Text[] Title;
    [SerializeField] private TMP_Text playtext;
    [SerializeField] private TMP_Text howtoplay;
    [SerializeField] private GameObject[] triangle;
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private TMP_Text[] best;
    [SerializeField] private GameObject Ufo;

    private float titletime;
    private float playtime;
    private int activecount = 0;
    private bool check = false;
    private int cursol = 0;
    private int enemytype = 0;
    private float loadtime = 0;
    
    private string stat = "start";

    SoundManager soundman;

    private void Start()
    {
        soundman = SoundManager.Instance;
        for (int i = 0; i < Title.Length; i++)
        {
            Title[i].transform.gameObject.SetActive(false);
        }
        playtext.gameObject.SetActive(false);
        howtoplay.gameObject.SetActive(false);
        for (int i = 0; i < triangle.Length; i++)
        {
            triangle[i].SetActive(false);
            best[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < enemy.Length; i++)
        {
            enemy[i].SetActive(false);
        }

        Ufo.SetActive(false);


    }

    private void Update()
    {
        switch (stat)
        {
            case ("start"):
                titletime += Time.deltaTime;
                playtime += Time.deltaTime;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    for (int i = activecount; i < Title.Length; i++)
                    {
                        Title[i].gameObject.SetActive(true);
                    }
                    stat = "update";
                    playtext.gameObject.SetActive(true);
                    howtoplay.gameObject.SetActive(true);
                    triangle[0].gameObject.SetActive(true);
                    best[0].gameObject.SetActive(true);
                    best[1].gameObject.SetActive(true);
                    best[1].text = PlayerPrefs.GetInt("HighScore", 0).ToString("D6");
                    for (int i = 0; i < enemy.Length; i++)
                    {
                        enemy[i].gameObject.SetActive(true);
                    }
                    Ufo.SetActive(true);

                    return;
                }
                if (titletime > 0.2f && !check)
                {
                    Title[activecount].transform.gameObject.SetActive(true);
                    activecount++;
                    titletime = 0;
                    if (activecount == Title.Length)
                    {
                        stat = "update";
                        playtext.gameObject.SetActive(true);
                        howtoplay.gameObject.SetActive(true);
                        triangle[0].gameObject.SetActive(true);
                        best[0].gameObject.SetActive(true);
                        best[1].gameObject.SetActive(true);
                        best[1].text = PlayerPrefs.GetInt("HighScore", 0).ToString("D6");
                        Ufo.gameObject.SetActive(true);
                        for (int i = 0; i < enemy.Length; i++)
                        {
                            enemy[i].gameObject.SetActive(true);
                        }
                        Ufo.SetActive(true);

                        return;
                    }

                    
                }
                break;

            case ("update"):
                playtime += Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    soundman.PlaySound("Select");
                    triangle[cursol].SetActive(false);
                    cursol = (cursol + 1) % 2;
                    triangle[cursol].SetActive(true);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    stat = "load";
                    soundman.PlaySound("Decide");
                    playtime = 0;
                    return;
                }

                if (playtime > 0.5f)
                {
                    for (int i = 0; i < enemy.Length; i++)
                    {
                        enemy[i].transform.GetChild(enemytype).gameObject.SetActive(false);
                        enemytype = (enemytype + 1) % 2;
                        enemy[i].transform.GetChild(enemytype).gameObject.SetActive(true);
                        playtime = 0;
                    }

                    if (enemytype == 0)
                    {
                        Ufo.transform.Translate(Vector3.left * 0.2f);
                    }
                    else
                    {
                        Ufo.transform.Translate(Vector3.right * 0.2f);
                    }


                }
                break;

            case ("load"):
                loadtime += Time.deltaTime;
                playtime += Time.deltaTime;

                if (playtime > 0.1f && triangle[cursol].activeSelf)
                {
                    triangle[cursol].SetActive(false);
                    playtime = 0;
                }
                else if (playtime > 0.1f)
                {
                    triangle[cursol].SetActive(true);
                    playtime = 0;
                }

                if (loadtime > 1)
                {
                    triangle[cursol].SetActive(true);
                    loadtime = 0;
                    playtime = 0;
                    if (cursol == 0)
                    {
                        SceneManager.LoadScene(1);
                    }

                    if (cursol == 1)
                    {
                        SceneManager.LoadScene(2);

                    }
                }
                break;




        }
        

        
    }
}
