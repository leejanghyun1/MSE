using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WebSocketSharp;
using static SeverManager;

public class DumyPlayer : MonoBehaviour
{
    public WebSocketSharp.WebSocket ws = null;
    public enum Role {Cook, Hall}

    public static DumyPlayer Instance;

    public bool On_handle = false;
    public bool Is_interact = false;
    public int _handle;
    public int _interact;
    public int hand_index;
    public GameObject[] Hands;

    public float x;
    public float z;

    public Role _role;

    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if (On_handle)
        {
            _handle = 1;
        }
        else
        {
            _handle = 0;
        }

        if (Is_interact)
        {
            _interact = 1;
        }
        else
        {
            _interact = 0;
        }

        //Receive();

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
        if (Input.GetKeyDown(KeyCode.A))
        {
            Changed();
        }
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(SeverManager.instance._receiveData._playerInfo.move_x + 1,
            transform.position.y,SeverManager.instance._receiveData._playerInfo.move_z + 1);
        transform.rotation = Quaternion.Euler(0, SeverManager.instance.dummy_r, 0);
    }

    void Receive()
    {
        PlayerInfo receive_info = SeverManager.instance._receiveData._playerInfo;
        x = receive_info.move_x;
        z = receive_info.move_z;
        _handle = receive_info.On_handle;
        _interact = receive_info.Is_interact;
        hand_index = receive_info.hand_index;
    }
}
