using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SocialPlatforms.Impl;

public class MultiPllayerController : MonoBehaviour
{
    public static Vector2 direction = Vector2.up;
    PhotonView view;
    Food food;
    [SerializeField] Transform snake_tail_prefab;
    public static List<Transform> Snake_tail;
    [SerializeField] GameObject Swipe_Panel;


    private void Start()
    {

        Snake_tail = new List<Transform>();
        view = GetComponent<PhotonView>();
        Snake_tail.Add(this.transform);
    }

    private void Update()
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

    private void FixedUpdate()
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


    private bool IsFoodOnTail(int x, int y)
    {
        for (int i = Snake.Snake_tail.Count - 1; i > 0; i--)
        {

            if ((Snake.Snake_tail[i].position.x == x) && (Snake.Snake_tail[i].position.y == y))
            {
                return true;
            }


        }
        return false;
    }

    private void RandomPosiyion()
    {
        Bounds bounds = food.SpawnArea.bounds;
        int x, y;


        do
        {
            x = (int)Random.Range(bounds.min.x, bounds.max.x);
            y = (int)Random.Range(bounds.min.y, bounds.max.y);
        }
        while (IsFoodOnTail(x, y));
        food.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "food")
        {
            AddTail();
            //score++;
            //CheckScore();
        }

        if (other.tag == "snake")
        {
            Debug.Log("Food");
            RandomPosiyion();
        }
    }
    private void AddTail()
    {
        if (view.IsMine)
        {
            Transform tail = Instantiate(this.snake_tail_prefab);
            tail.position = Snake_tail[Snake_tail.Count - 1].position;
            Snake_tail.Add(tail);
        }
    }
}
