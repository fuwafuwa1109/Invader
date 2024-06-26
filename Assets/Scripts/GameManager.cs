using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Squid;
    [SerializeField] private GameObject Crab;
    [SerializeField] public GameObject Octopus;
    [SerializeField] public GameObject UFO;
    [SerializeField] private GameObject Jet;
    [SerializeField] public GameObject mylaser;
    [SerializeField] public GameObject erase;
    [SerializeField] private GameObject WallBlock;
    [SerializeField] public GameObject enemylaserA;
    [SerializeField] public GameObject enemylaserB;
    [SerializeField] public GameObject enemylaserC;
    [SerializeField] public GameObject Bomb;
    [SerializeField] public GameObject Bar;
    [SerializeField] private TMP_Text gameover;
    [SerializeField] public TMP_Text scoretext;
    [SerializeField] private TMP_Text best;
    [SerializeField] private TMP_Text newrecord;
    [SerializeField] private TMP_Text lifetext;
    [SerializeField] private TMP_Text gamestart;
    [SerializeField] private TMP_Text BackToTitle;
    [SerializeField] private GameObject triangle;


    public GameObject[,] EnemyBoard = new GameObject[5, 11];
    public int[,] enemysurvive = new int[5, 11];
    public GameObject jetset;
    public GameObject ufoset;
    float GameTime;
    public float MoveTime;
    public GameObject bombset;
    public int score = 0;




    int typeofenemy;
    float movedistance, movedistance_1, movedistance_2, movedistance_3, movedistance_4;
    Move move;
    SoundManager soundman;
    public Vector3 jetpos;
    public Vector3 jetsize;

    public int lasercount = 0;

    //*************壁についての変数***************
    private int[,] wallboard = new int[17, 23] {
         {0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
         {0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
         {0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
         {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
         {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1},
         {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1},
         {1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1},
         {1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1},
         {1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1}};


    public int[,] wallboard1 = new int[17,23];
    public int[,] wallboard2 = new int[17, 23];
    public int[,] wallboard3 = new int[17, 23];
    public int[,] wallboard4 = new int[17, 23];


    private float wally = 1.544f;
    private float wallx = 0.264f;
    private float wallcell = 0.064f;

    public GameObject[,] wallobject1= new GameObject[17, 23];
    public GameObject[,] wallobject2 = new GameObject[17, 23];
    public GameObject[,] wallobject3 = new GameObject[17, 23];
    public GameObject[,] wallobject4 = new GameObject[17, 23];


    public int ufoLorR = 1;
    private float ufotime = 0;
    private int[] ufospawn = new int[3] {13, 0, 0};


    //*********ライフ***************
    public int life = 3;
    public GameObject[] playerlife;

    public string gamestat = "start";
    private int enemycountrow = 0;
    private int enemycountcolumn = 0;
    private float enemysettime;
    public float enemyY = 7;
    public int enemycount = 55;

    //*********ダメージ変数*********
    public bool damage = false;
    private float damagetime = 0;
    private float flashtime = 0;


    //*******ゲームオーバー************

    private float scoretime;
    private bool judgegameover = true;

    //********ハイスコア変数**************

    private int highscore;
    private bool record = false;

    private float selecttime;




    public static GameManager Instance { get; private set; }


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // インスタンスを設定
        }
        else
        {
            Destroy(gameObject); // 既にインスタンスが存在する場合は新しいオブジェクトを破棄
        }
    }

    private void Start()
    {
        movedistance   = 0.0f;
        movedistance_1 = 0.0f;
        movedistance_2 = 0.0f;
        movedistance_3 = 0.0f;
        movedistance_4 = 0.0f;

        move = gameObject.AddComponent<Move>();
        soundman = SoundManager.Instance;

        playerlife = new GameObject[5];



        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                enemysurvive[i, j] = 1;
            }
        }

        jetsize = Jet.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size;



        gameover.gameObject.SetActive(false);


        highscore = PlayerPrefs.GetInt("HighScore", 0);
        best.text = highscore.ToString("D6");
        newrecord.gameObject.SetActive(false);
        lifetext.gameObject.SetActive(false);
        gamestart.gameObject.SetActive(true);

        BackToTitle.gameObject.SetActive(false);
        triangle.SetActive(false);

        gamestat = "gamestart";

        //ハイスコアリセット用
        //PlayerPrefs.DeleteKey("HighScore");
        //PlayerPrefs.Save();


    }

    private void Update()
    {
        switch (gamestat)
        {

            case "gamestart":
                enemysettime += Time.deltaTime;
                if (enemysettime > 1)
                {
                    gamestart.gameObject.SetActive(false);
                    lifetext.gameObject.SetActive(true);
                    Instantiate(Bar, new Vector3(6.87f, -2.5f, 0), Quaternion.identity); //バーの配置


                    //****************壁の配置********************
                    for (int x = 0; x < 23; x++)
                    {
                        for (int y = 0; y < 17; y++)
                        {
                            wallboard1[y, x] = wallboard[y, x];
                            wallboard2[y, x] = wallboard[y, x];
                            wallboard3[y, x] = wallboard[y, x];
                            wallboard4[y, x] = wallboard[y, x];
                            if (wallboard[y, x] == 1)
                            {
                                wallobject1[y, x] = Instantiate(WallBlock, new Vector3(wallx + x * wallcell + 1.2224f, wally - wallcell * y, 0), Quaternion.identity);
                                wallobject2[y, x] = Instantiate(WallBlock, new Vector3(wallx + x * wallcell + 4.4168f, wally - wallcell * y, 0), Quaternion.identity);
                                wallobject3[y, x] = Instantiate(WallBlock, new Vector3(wallx + x * wallcell + 7.6112f, wally - wallcell * y, 0), Quaternion.identity);
                                wallobject4[y, x] = Instantiate(WallBlock, new Vector3(wallx + x * wallcell + 10.8056f, wally - wallcell * y, 0), Quaternion.identity);


                            }
                        }
                    }



                    for (int i = 0; i < life; i++)
                    {
                        playerlife[i] = Instantiate(Jet, new Vector3(i + 1, -3, 0), Quaternion.identity);
                    }
                    gamestat = "start";
                    enemysettime = 0;
                    return;
                }
                break;


//********************敵とジェットの設置*****************
            case "start":
                enemysettime += Time.deltaTime;
                if (enemysettime > 0.01f)
                {
                    enemysettime = 0;
                    if (enemycountrow == 0 || enemycountrow == 1)
                    {
                        EnemyBoard[enemycountrow, enemycountcolumn] =
                            Instantiate(Octopus, new Vector3(enemycountcolumn-0.5f, enemyY + enemycountrow, 0),
                            Quaternion.identity);
                        enemycountcolumn++;
                        if (enemycountcolumn == 11)
                        {
                            enemycountrow++;
                            enemycountcolumn = 0;
                        }
                        return;
                    }

                    if (enemycountrow == 2 || enemycountrow == 3)
                    {
                        EnemyBoard[enemycountrow, enemycountcolumn] =
                            Instantiate(Crab, new Vector3(enemycountcolumn-0.5f, enemyY + enemycountrow, 0),
                            Quaternion.identity);
                        enemycountcolumn++;
                        if (enemycountcolumn == 11)
                        {
                            enemycountrow++;
                            enemycountcolumn = 0;
                        }
                        return;
                    }

                    if (enemycountrow == 4)
                    {
                        EnemyBoard[enemycountrow, enemycountcolumn] =
                            Instantiate(Squid, new Vector3(enemycountcolumn-0.5f, enemyY + enemycountrow, 0),
                            Quaternion.identity);
                        enemycountcolumn++;
                        if (enemycountcolumn == 11)
                        {
                            enemycountrow = 0;
                            enemycountcolumn = 0;
                            jetset = Instantiate(Jet, new Vector3(7, -1, 0), Quaternion.identity);
                            gamestat = "update";
                            goto case "update";

                        }
                        return;
                    }
                }
                break;


 //****************ゲームの進行********************
            case "update":
                GameTime += Time.deltaTime;
                MoveTime += Time.deltaTime;
                ufotime += Time.deltaTime;
                move.EnemyMove(EnemyBoard, enemysurvive, Squid, Crab, Octopus, ref MoveTime, ref movedistance,
                               ref movedistance_1, ref movedistance_2, ref movedistance_3, ref movedistance_4);

                move.JetMove(ref jetset, Jet, jetsize, mylaser);
                move.enemylaser(enemysurvive, EnemyBoard);

                if (ufotime > 18)
                {
                    ufoset = Instantiate(UFO, new Vector3(ufospawn[ufoLorR + 1], 14, 0), Quaternion.identity);
                    ufotime = 0;
                }

                if (damage)
                {
                    gamestat = "damage";
                }
                break;
            
 //***************ダメージを受けた時の処理***************
            case "damage":
                damagetime += Time.deltaTime;
                flashtime += Time.deltaTime;
                if (flashtime > 0.1f)
                {
                    if (jetset.activeSelf)
                    {
                        jetset.SetActive(false);
                        flashtime = 0;
                    }
                    else
                    {
                        jetset.SetActive(true);
                        flashtime = 0;
                    }
                }
                if (damagetime > 1f)
                {
                    Destroy(playerlife[life]);
                    jetset.SetActive(true);
                    if (life == 0)
                    {
                        gamestat = "wallbreak";
                        damagetime = 0;
                        flashtime = 0;
                        damage = false;
                        return;
                    }
                    else
                    {
                        jetset.SetActive(true);
                        gamestat = "update";
                        damagetime = 0;
                        flashtime = 0;
                        damage = false;
                        return;
                    }
                }
                break;
//***************次のステージに進む処理*************
            case "gotonext":
                damagetime += Time.deltaTime;
                if (damagetime > 2)
                {
                    if (life < 5)
                    {
                        life++;
                        playerlife[life - 1] = Instantiate(Jet, new Vector3(life, -3, 0), Quaternion.identity);
                        
                    }
                    if (enemyY > 2)
                    {
                        enemyY -= 1;
                    }
                    damagetime = 0;

                    for (int i = 0; i < 23; i++)
                    {
                        for (int j = 0; j < 17; j++)
                        {
                            if (wallboard1[j,i] == 1)
                            {
                                Destroy(wallobject1[j,i]);
                            }
                            if (wallboard2[j, i] == 1)
                            {
                                Destroy(wallobject2[j, i]);
                            }
                            if (wallboard3[j, i] == 1)
                            {
                                Destroy(wallobject3[j, i]);
                            }
                            if (wallboard4[j, i] == 1)
                            {
                                Destroy(wallobject4[j, i]);
                            }

                            

                        }
                    }
                    for (int x = 0; x < 23; x++)
                    {
                        for (int y = 0; y < 17; y++)
                        {
                            wallboard1[y, x] = wallboard[y, x];
                            wallboard2[y, x] = wallboard[y, x];
                            wallboard3[y, x] = wallboard[y, x];
                            wallboard4[y, x] = wallboard[y, x];
                            if (wallboard[y, x] == 1)
                            {
                                wallobject1[y, x] = Instantiate(WallBlock, new Vector3(wallx + x * wallcell + 1.2224f, wally - wallcell * y, 0), Quaternion.identity);
                                wallobject2[y, x] = Instantiate(WallBlock, new Vector3(wallx + x * wallcell + 4.4168f, wally - wallcell * y, 0), Quaternion.identity);
                                wallobject3[y, x] = Instantiate(WallBlock, new Vector3(wallx + x * wallcell + 7.6112f, wally - wallcell * y, 0), Quaternion.identity);
                                wallobject4[y, x] = Instantiate(WallBlock, new Vector3(wallx + x * wallcell + 10.8056f, wally - wallcell * y, 0), Quaternion.identity);
                            }
                        }
                    }

                    for (int i = 0; i < 11; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            enemysurvive[j, i] = 1;
                        }
                    }
                    Destroy(jetset);
                    Destroy(ufoset);
                    GameTime = 0;
                    MoveTime = 0;
                    ufotime = 0;
                    enemycount = 55;
                    move.updatetime = 0.8f;
                    move.timelag = 0.08f;
                    move.enemytime1 = 0;
                    move.enemytime2 = 0;
                    move.enemytime3 = 0;
                    move.enemytime4 = 0;
                    move.leftorright = 1;

                    move.movecheck1 = false;
                    move.movecheck2 = false;
                    move.movecheck3 = false;
                    move.movecheck4 = false;

                    
                    gamestat = "start";
                    return;

                }
                break;

            //************ゲームオーバーの処理***************

            case ("wallbreak"):
                for (int i = 0; i < 23; i++)
                {
                    for (int j = 0; j < 17; j++)
                    {
                        if (wallboard1[j, i] == 1)
                        {
                            Destroy(wallobject1[j, i]);
                        }
                        if (wallboard2[j, i] == 1)
                        {
                            Destroy(wallobject2[j, i]);
                        }
                        if (wallboard3[j, i] == 1)
                        {
                            Destroy(wallobject3[j, i]);
                        }
                        if (wallboard4[j, i] == 1)
                        {
                            Destroy(wallobject4[j, i]);
                        }



                    }
                }
                gamestat = "gameover";
                break;




            case "gameover":
                damagetime += Time.deltaTime;
                scoretime += Time.deltaTime;
                selecttime += Time.deltaTime;
                if (damagetime > 0.5f)
                {
                    gameover.gameObject.SetActive(true);
                    damagetime = 0;
                }

                if (scoretime > 0.4f && scoretext.gameObject.activeSelf)
                {
                    scoretime = 0;
                    scoretext.gameObject.SetActive(false);
                    if (record)
                    {
                        newrecord.gameObject.SetActive(false);
                        best.gameObject.SetActive(false);
                    }

                }
                else if (scoretime > 0.4f)
                {
                    scoretime = 0;
                    scoretext.gameObject.SetActive(true);
                    if (record)
                    {
                        newrecord.gameObject.SetActive(true);
                        best.gameObject.SetActive(true);
                    }
                }
                if (score > highscore && !record &&judgegameover)
                {
                    PlayerPrefs.SetInt("HighScore", score);
                    PlayerPrefs.Save();
                    best.text = score.ToString("D6");
                    soundman.PlaySound("Win");
                    judgegameover = false;
                    record = true;
                    return;
                }
                else if (judgegameover)
                {
                    soundman.PlaySound("GameOver");
                    judgegameover = false;
                    return;
                }


                if (selecttime > 2)
                {
                    BackToTitle.gameObject.SetActive(true);
                    triangle.SetActive(true);
                    selecttime = 0;
                    
                }

                if (Input.GetKeyDown(KeyCode.Space) && triangle.activeSelf)
                {
                    soundman.PlaySound("Decide");
                    gamestat = "selectnext";
                    damagetime = 0;
                    scoretime = 0;
                    selecttime = 0;
                    return;
                }


                break;

            case ("selectnext"):
                selecttime += Time.deltaTime;
                scoretime += Time.deltaTime;

                if (scoretime > 0.1f && triangle.activeSelf)
                {
                    triangle.SetActive(false);
                    scoretime = 0;
                }
                else if (scoretime > 0.1f)
                {
                    triangle.SetActive(true);
                    scoretime = 0;
                }

                if (selecttime > 1)
                {
                    selecttime = 0;
                    scoretime = 0;
                    SceneManager.LoadScene(0);
                    return;
                }
                break;
                


        }

    }

}
