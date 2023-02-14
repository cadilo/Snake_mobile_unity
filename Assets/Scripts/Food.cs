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

    private bool IsFoodOnTail(float x, float y)
    {
        for (int i = Snake.Snake_tail.Count - 1; i > 0; i--)
        {
            if ((Snake.Snake_tail[i].position.x == x) && (Snake.Snake_tail[i].position.y == y))
                return true;     
        }
        return false;
    }

    private void RandomPosiyion()
    {
        Bounds bounds = this.SpawnArea.bounds;
        float x, y;

        // Генерируем переменные позиции еды, проверяя не находятся ли они на хвосте
        do
        {
           x = Random.Range(bounds.min.x, bounds.max.x);
           y = Random.Range(bounds.min.y, bounds.max.y);
        }
        while (IsFoodOnTail(x, y));

            this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "snake")
        {
            RandomPosiyion();
        }
    }
}
