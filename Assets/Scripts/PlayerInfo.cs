using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{    
    [SerializeField] private InputField input;
    [SerializeField] private string NickName;

    public void SetNickName()
    {
      NickName = input.text;
    }
}
