using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Role {Cook, Hall}

    public static Player Instance;

    public bool On_handle = false;
    public bool Is_interact = false;
    public int Send_handle;
    public int Send_interact;
    public int hand_index;
    public GameObject[] Hands;
    public SeverManager.GameData _sendData;

    public Role _role;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Packaging();

        if (Is_interact == true && On_handle == false)
        {
            On_handle = true;
            Hands[hand_index].SetActive(On_handle);
            Is_interact = false;
        }
        else if(Is_interact == true && On_handle == true)
        {
            On_handle = false;
            Hands[hand_index].SetActive(On_handle);
            Is_interact = false;
        }

        DevelopChange();
    }

    //역할 바꾸기
    public void Changed()
    {
        if(this._role == Role.Cook) 
        {
            this._role = Role.Hall;
        }
        else if(this._role == Role.Hall)
        {
            this._role = Role.Cook;
        }
    }

    private void DevelopChange()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Changed();
        }
    }

    private void Packaging()
    {
        if (On_handle)
        {
            Send_handle = 1;
        }
        else
        {
            Send_handle = 0;
        }

        if (Is_interact)
        {
            Send_interact = 1;
        }
        else
        {
            Send_interact = 0;
        }

        _sendData._playerInfo.move_x = this.transform.position.x;
        _sendData._playerInfo.move_z = this.transform.position.z;
        _sendData._playerInfo.rotation = this.transform.rotation.y;
        _sendData._playerInfo.On_handle = Send_handle;
        _sendData._playerInfo.Is_interact = Send_interact;
        _sendData._playerInfo.hand_index = hand_index;

        SeverManager.instance._gameData = _sendData;
    }
}
