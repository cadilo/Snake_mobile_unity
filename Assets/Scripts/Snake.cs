using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Snake : MonoBehaviourPunCallbacks
{
    PhotonView view;
    public static Vector2 direction = Vector2.up;
    private bool flagmultiplayer;
    //=================================Список со змейкой
    public static List<Transform> Snake_tail;
    //==================================================

    [SerializeField] Transform snake_tail_prefab;
    //================================ Текст с очками
    [SerializeField] Text scoreText;
    private int score = 0;

    [SerializeField] Text BestScoreText;
    private int BestScore = 0;

    public AudioSource sound;
    public AudioClip gameov;
    //================================================

    //================================= Кнопки
    //[SerializeField] Button btn_UP;
    //[SerializeField] Button btn_DOWN;
    //[SerializeField] Button btn_LEFT;
    //[SerializeField] Button btn_RIGHT;

    [SerializeField] GameObject Start_btn;
    [SerializeField] GameObject Exit_btn;
    [SerializeField] GameObject Swipe_Panel;
    [SerializeField] GameObject bgmusic;
    //==========================================

    private void Start()
    {
        



        if(SceneManager.GetActiveScene().name == "MultiPlayerGame")
        {
            if (view.IsMine)
            {
                bgmusic.SetActive(false);
                Swipe_Panel.SetActive(true);
                flagmultiplayer = true;
                view = GetComponent<PhotonView>();

                Snake_tail = new List<Transform>();
                Snake_tail.Add(this.transform);
            }
        }
        else
        {
            flagmultiplayer = false;
            Time.timeScale = 0;
            Start_btn.SetActive(true);
            Exit_btn.SetActive(true);
            bgmusic.SetActive(false);

            Snake_tail = new List<Transform>();
            Snake_tail.Add(this.transform);
        }


        if (PlayerPrefs.HasKey("snakeScore"))
        {
            BestScore = PlayerPrefs.GetInt("snakeScore");
        }
        else
        {
            BestScore = 0;
        }
        CheckScore();
    }
    private void Update()
    {


        //====================================== Управление клавиатурой
        if (flagmultiplayer)
        {
            if (view.IsMine)
            {

                if (Input.GetKey(KeyCode.W))
                {
                    direction = Vector2.up;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    direction = Vector2.down;
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    direction = Vector2.left;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    direction = Vector2.right;
                }
            }
        }
        else 
        {
            if (Input.GetKey(KeyCode.W))
            {
                direction = Vector2.up;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                direction = Vector2.down;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                direction = Vector2.left;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                direction = Vector2.right;
            }
        }
        //==============================================================

    }

    private void FixedUpdate()
    {
        if (flagmultiplayer)
        {
            if (view.IsMine)
            {
                for (int i = Snake_tail.Count - 1; i > 0; i--)
                {
                    Snake_tail[i].position = Snake_tail[i - 1].position;
                }
                this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + direction.x, Mathf.Round(this.transform.position.y) + direction.y, 0f);
            }
        }
        else
        {
            for (int i = Snake_tail.Count - 1; i > 0; i--)
            {
                Snake_tail[i].position = Snake_tail[i - 1].position;
            }
            this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + direction.x, Mathf.Round(this.transform.position.y) + direction.y, 0f);
        }
    }

    //Кнопка для старта игры
    public void Start_game()
    {
        Debug.Log("start");
        Time.timeScale = 1;
        Start_btn.SetActive(false);
        Exit_btn.SetActive(false);
        bgmusic.SetActive(true);
        Swipe_Panel.SetActive(true);
        CheckScore();
    }
    //Кнопка для выхода их игры
    public void Exit_game()
    {
        Debug.Log("exit");
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    private void CheckScore()
    {
        if(score >= BestScore)
        {
            BestScore = score;
        }

        scoreText.text = "SCORE: " + score.ToString();
        BestScoreText.text = "BEST SCORE: " + BestScore.ToString();
    }

    private void AddTail()
    {
        if (flagmultiplayer)
        {
            if (view.IsMine)
            {
                Transform tail = Instantiate(this.snake_tail_prefab);
                tail.position = Snake_tail[Snake_tail.Count - 1].position;

                Snake_tail.Add(tail);
            }
        }
        else
        {
            Transform tail = Instantiate(this.snake_tail_prefab);
            tail.position = Snake_tail[Snake_tail.Count - 1].position;

            Snake_tail.Add(tail);
        }
    }
    
    private void GameOver()
    {
        //Debug.Log("gameover");

        for(int i = 1; i < Snake_tail.Count; i++)
        {
            Destroy(Snake_tail[i].gameObject);
        }
        Snake_tail.Clear();
        Snake_tail.Add(this.transform);

        this.transform.position = Vector3.zero;

        if(score >= BestScore)
        {
            BestScore = score;
            PlayerPrefs.SetInt("snakeScore", BestScore);
        }
        score = 0;
        CheckScore();

        Time.timeScale = 0;
        Start_btn.SetActive(true);
        Exit_btn.SetActive(true);
        Swipe_Panel.SetActive(false);
        bgmusic.SetActive(false);
        sound.PlayOneShot(gameov);
    }

    //Обработка коллизий
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (flagmultiplayer)
        {
            if (view.IsMine)
            {
                if (other.tag == "food")
                {
                    AddTail();
                    score++;
                    CheckScore();
                }
                else if (other.tag == "gameover")
                {
                    GameOver();
                }
            }
        }

        if (other.tag == "food")
        {
            AddTail();
            score++;
            CheckScore();
        }
        else if (other.tag == "gameover")
        {
            GameOver();
        }
    }
}
