using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public bool movecheck1, movecheck2, movecheck3, movecheck4;
    public float enemytime1, enemytime2, enemytime3, enemytime4 = 0.0f;
    public float ydistance = 6.0f;
    public float xrightlim = 14f;
    public float xleftlim = -0.5f;
    public bool dontmove = true;
    public int leftorright = 1;
    private bool yminus = false;
    private int typeofenemy = 0;
    private float ytime1, ytime2, ytime3, ytime4 = 0;
    private bool ycheck0, ycheck1, ycheck2, ycheck3, ycheck4 = false;

    public float updatetime = 0.8f; //敵の移動速度
    public float timelag = 0.08f; //敵の移動のラグ
    private float lasertime;




    public float[,] erasetime = new float[5,11];
 


    public bool lasercheck;

    private float[,,] enemyxy = new float[5,11,2];


    float randomevent;
    int laserselect;


    GameManager gameman;
    SoundManager soundman;


    public static Move Instance { get; private set; }

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
        movecheck1 = false;
        movecheck2 = false;
        movecheck3 = false;
        movecheck4 = false;

        lasercheck = true;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                enemyxy[i, j, 0] = j;
                enemyxy[i, j, 1] = 11 - i;
            }
        }

        gameman = GameManager.Instance;
        soundman = SoundManager.Instance;

     
    }
    public void EnemyMove(GameObject[,] GameBoard, int[,] enemysurvive, GameObject Squid,
                          GameObject Crab, GameObject Octopus,
                          ref float MoveTime, ref float movedistance,
                          ref float movedistance_1, ref float movedistance_2,
                          ref float movedistance_3, ref float movedistance_4)
    {



        



        if (MoveTime > updatetime)
        {
            typeofenemy = (typeofenemy + 1) % 2;

            movecheck1 = true;
            movecheck2 = true;
            movecheck3 = true;
            movecheck4 = true;

            enemytime1 = MoveTime - updatetime;
            enemytime2 = MoveTime - updatetime;
            enemytime3 = MoveTime - updatetime;
            enemytime4 = MoveTime - updatetime;

            MoveTime = 0.0f;

            soundman.PlaySound("enemywalk");

            for (int i = 0; i < 11; i++)
            {
                if (enemysurvive[0, i] == 1)
                {
                    GameBoard[0, i].transform.Translate(Vector3.right * 0.15f * leftorright);
                    GameBoard[0, i].transform.GetChild((typeofenemy + 1) % 2).gameObject.SetActive(false);
                    GameBoard[0, i].transform.GetChild(typeofenemy).gameObject.SetActive(true);
                    
                }
            }
        }

        if (movecheck1)
        {
            enemytime1 += Time.deltaTime;
            if (enemytime1 > timelag)
            {
                for (int i = 0; i < 11; i++)
                {
                    if (enemysurvive[1, i] == 1)
                    {
                        GameBoard[1, i].transform.Translate(Vector3.right * 0.15f * leftorright);
                        GameBoard[1, i].transform.GetChild((typeofenemy + 1) % 2).gameObject.SetActive(false);
                        GameBoard[1, i].transform.GetChild(typeofenemy).gameObject.SetActive(true);
                        movecheck1 = false;
                    }
                }

            }
        }

        if (movecheck2)
        {
            enemytime2 += Time.deltaTime;
            if (enemytime2 > timelag * 2)
            {
                for (int i = 0; i < 11; i++)
                {
                    if (enemysurvive[2, i] == 1)
                    {
                        GameBoard[2, i].transform.Translate(Vector3.right * 0.15f * leftorright);
                        GameBoard[2, i].transform.GetChild((typeofenemy + 1) % 2).gameObject.SetActive(false);
                        GameBoard[2, i].transform.GetChild(typeofenemy).gameObject.SetActive(true);
                        movecheck2 = false;
                    }
                }

            }
        }

        if (movecheck3)
        {
            enemytime3 += Time.deltaTime;
            if (enemytime3 > timelag * 3)
            {
                for (int i = 0; i < 11; i++)
                {
                    if (enemysurvive[3, i] == 1)
                    {
                        GameBoard[3, i].transform.Translate(Vector3.right * 0.15f * leftorright);
                        GameBoard[3, i].transform.GetChild((typeofenemy + 1) % 2).gameObject.SetActive(false);
                        GameBoard[3, i].transform.GetChild(typeofenemy).gameObject.SetActive(true);
                        movecheck3 = false;
                    }
                }

            }
        }

        if (movecheck4)
        {
            enemytime4 += Time.deltaTime;
            if (enemytime4 > timelag * 4)
            {
                for (int i = 0; i < 11; i++)
                {
                    if (enemysurvive[4, i] == 1)
                    {
                        GameBoard[4, i].transform.Translate(Vector3.right * 0.15f * leftorright);
                        GameBoard[4, i].transform.GetChild((typeofenemy + 1) % 2).gameObject.SetActive(false);
                        GameBoard[4, i].transform.GetChild(typeofenemy).gameObject.SetActive(true);
                        movecheck4 = false;
                    }
                }
                for (int i = 0; i < 11; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (enemysurvive[j, i] == 1)
                        {
                            if (GameBoard[j, i].transform.position.x > xrightlim && leftorright == 1)
                            {
                                leftorright = leftorright * -1;
                                yminus = true;
                                ycheck0 = true;
                                ycheck1 = true;
                                ycheck2 = true;
                                ycheck3 = true;
                                ycheck4 = true;

                                break;

                            }
                            else if (GameBoard[j, i].transform.position.x < xleftlim && leftorright == -1)
                            {
                                leftorright = leftorright * -1;
                                yminus = true;
                                ycheck0 = true;
                                ycheck1 = true;
                                ycheck2 = true;
                                ycheck3 = true;
                                ycheck4 = true;
                                break;
                            }
                        }
                    }
                    if (yminus)
                    {
                        break;
                    }
                }

            }
        }




        if (ycheck0)
        {
            for (int i = 0; i < 11; i++)
            {
                if (enemysurvive[0, i] == 1)
                {
                    GameBoard[0, i].transform.Translate(Vector3.down * 0.5f);

                }
            }
            ycheck0 = false;
        }

        if (ycheck1)
        {
            ytime1 += Time.deltaTime;
            if (ytime1 > timelag)
            {
                for (int i = 0; i < 11; i++)
                {
                    if (enemysurvive[1, i] == 1)
                    {
                        GameBoard[1, i].transform.Translate(Vector3.down * 0.5f);
                        ytime1 = 0;
                    }
                }
                ycheck1 = false;
            }
        }

        if (ycheck2)
        {
            ytime2 += Time.deltaTime;
            if (ytime2 > timelag * 2)
            {
                for (int i = 0; i < 11; i++)
                {
                    if (enemysurvive[2, i] == 1)
                    {
                        GameBoard[2, i].transform.Translate(Vector3.down * 0.5f);
                        ytime2 = 0;
                    }
                }
                ycheck2 = false;
            }
        }

        if (ycheck3)
        {
            ytime3 += Time.deltaTime;
            if (ytime3 > timelag * 3)
            {
                for (int i = 0; i < 11; i++)
                {
                    if (enemysurvive[3, i] == 1)
                    {
                        GameBoard[3, i].transform.Translate(Vector3.down * 0.5f);
                        ytime3 = 0;
                    }
                }
                ycheck3 = false;
            }
        }

        if (ycheck4)
        {
            ytime4 += Time.deltaTime;
            if (ytime4 > timelag * 4)
            {
                for (int i = 0; i < 11; i++)
                {
                    if (enemysurvive[4, i] == 1)
                    {
                        GameBoard[4, i].transform.Translate(Vector3.down * 0.5f);
                        ytime4 = 0;
                        yminus = false;
                    }
                }
                ycheck4 = false;
                if (timelag > 0.03f)
                {
                    timelag -= 0.02f;
                }
                else
                {
                    timelag = 0.01f;
                }

                if (updatetime > 0.2f)
                {
                    updatetime -= 0.15f;

                }
                else
                {
                    updatetime = 0.1f;
                }

                for (int i = 0; i < 11; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (enemysurvive[j,i] == 1)
                        {
                            if (GameBoard[j,i].transform.position.y < 0)
                            {
                                gameman.gamestat = "wallbreak";
                            }
                        }
                    }
                }
            }
        }

        





    }

    public void JetMove(ref GameObject jetset, GameObject Jet, Vector3 jetsize, GameObject laser)
    {
        float xposmin = jetset.transform.position.x;
        float xposmax = jetset.transform.position.x;



        if (Input.GetButton("Horizontal"))
        {
            if (Input.GetAxis("Horizontal") > 0.01f && xposmax < xrightlim)
            {
                jetset.transform.Translate(Vector3.right * 7f * Time.deltaTime);
            }

            if (Input.GetAxis("Horizontal") < -0.01f && xposmin > xleftlim)
            {
                jetset.transform.Translate(Vector3.left * 7f * Time.deltaTime);
            }

        }
        if (lasercheck)
        {
            lasertime += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space) && lasertime > 0.6f)
            {
                Instantiate(laser, new Vector3(jetset.transform.position.x, jetset.transform.position.y + 0.5f, 0), Quaternion.identity);
                soundman.PlaySound("LaserShoot");
                lasercheck = false;
                lasertime = 0;
            }
        }


    }

    public void enemylaser(int[,] GameBoard, GameObject[,] enemyboard)
    {

        randomevent += Time.deltaTime;
        

        if (randomevent > timelag * gameman.enemyY * 1.5 + 1.5)//敵の初期位置が低いほど、動きが速くなるほどレーザーの頻度が高くなる
        {
            laserselect = Random.Range(0, 3);
            int enemyselect = Random.Range(0, 11);

            if (laserselect == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (GameBoard[i, enemyselect] == 1)
                    {
                        Instantiate(gameman.enemylaserA,
                                            new Vector3(enemyboard[i, enemyselect].transform.position.x,
                                                        enemyboard[i, enemyselect].transform.position.y,
                                                        0),
                                            Quaternion.identity);
                        soundman.PlaySound("EnemyShoot");
                        randomevent = 0;
                        return;
                    }
                    
                }
                randomevent = 0.4f;
                return;
            }

            else if (laserselect == 1)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (GameBoard[i, enemyselect] == 1)
                    {
                        Instantiate(gameman.enemylaserB,
                                            new Vector3(enemyboard[i, enemyselect].transform.position.x,
                                                        enemyboard[i, enemyselect].transform.position.y,
                                                        0),
                                            Quaternion.identity);
                        soundman.PlaySound("EnemyShoot");

                        randomevent = 0;
                        return;
                    }
                    
                }
                randomevent = 0.4f;
                return;

            }

            else
            {
                for (int i = 0; i < 5; i++)
                {
                    if (GameBoard[i, enemyselect] == 1)
                    {
                        Instantiate(gameman.enemylaserC,
                                            new Vector3(enemyboard[i, enemyselect].transform.position.x,
                                                        enemyboard[i, enemyselect].transform.position.y,
                                                        0),
                                            Quaternion.identity);
                        soundman.PlaySound("EnemyShoot");

                        randomevent = 0;
                        return;
                    }
                    
                }
                randomevent = 0.4f;
                return;

            }
        }


        

        
    }

    

    

    

}
