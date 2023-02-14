using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] BoxCollider2D SpawnArea;


    private void Start()
    {
        RandomPosiyion();
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
        Bounds bounds = this.SpawnArea.bounds;
        int x, y;

        // Ãåíåðèðóåì ïåðåìåííûå ïîçèöèè åäû, ïðîâåðÿÿ íå íàõîäÿòñÿ ëè îíè íà õâîñòå
        do
        {
            x = (int)Random.Range(bounds.min.x, bounds.max.x);
            y = (int)Random.Range(bounds.min.y, bounds.max.y);
        }
        while (IsFoodOnTail(x, y));
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);

        //x = (int)Random.Range(bounds.min.x, bounds.max.x);
        //y = (int)Random.Range(bounds.min.y, bounds.max.y);

        //if (IsFoodOnTail(x, y))
        //{

        //    x = (int)Random.Range(bounds.min.x, bounds.max.x);
        //    y = (int)Random.Range(bounds.min.y, bounds.max.y);
        //    this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);
        //}
        //else
        //{

        //    this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);
        //}


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "snake")
        {
            RandomPosiyion();
        }
    }
}
