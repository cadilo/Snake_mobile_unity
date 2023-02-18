using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Snake2 : MonoBehaviour
{
    public float speed;
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (view.IsMine)
        {
            //Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), (Input.GetAxisRaw("Vertical")));
            Vector2 moveAmount = Snake.direction * speed * Time.deltaTime;
            transform.position += (Vector3)moveAmount;
        }
    }
}