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


    private void RandomPosiyion()
    {
        Bounds bounds = this.SpawnArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

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
