using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DetectSwipe : MonoBehaviour, IDragHandler, IBeginDragHandler
{

    public void OnBeginDrag(PointerEventData eventData)
    {
        if((Mathf.Abs(eventData.delta.x)) > (Mathf.Abs(eventData.delta.y)))
        {
            if(eventData.delta.x > 0)
            {
                Debug.Log("right");
                Snake.direction = Vector2.right;
            }
            else
            {
                Debug.Log("left");
                Snake.direction = Vector2.left;
            }
        }
        else
        {
            if (eventData.delta.y > 0)
            {
                Debug.Log("up");
                Snake.direction = Vector2.up;
            }
            else
            {
                Debug.Log("down");
                Snake.direction = Vector2.down;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
    
    }
}
