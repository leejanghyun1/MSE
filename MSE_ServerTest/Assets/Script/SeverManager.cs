using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static SeverManager;

public class SeverManager : MonoBehaviour
{
    public static SeverManager instance;

    public float dummy_x;
    public float dummy_z;
    public float dummy_r;

    [System.Serializable]
    public class GameData
    {
        public PlayerInfo _playerInfo;
    }

    [System.Serializable]
    public class PlayerInfo
    {
        public float move_x;
        public float move_z;
        public float rotation;
        public int On_handle;
        public int Is_interact;
        public int hand_index;
    }

    public GameData _gameData;
    public GameData _receiveData;

    public WebSocketSharp.WebSocket ws = null;

    void Start()
    {
        instance = this;

        ws = new WebSocketSharp.WebSocket("ws://43.201.219.112:8080/api/socket");

        ws.OnMessage += OnWebSocketMessage;

        ws.Connect();
    }

    void Update()
    {
        PlayerInfo test = new PlayerInfo()
        {
            move_x = Player.Instance._sendData._playerInfo.move_x,
            move_z = Player.Instance._sendData._playerInfo.move_z,
            rotation = Player.Instance._sendData._playerInfo.rotation
        };

        string send_info = JsonUtility.ToJson(test);

        ws.Send(send_info);
        //ws.Send(Encoding.UTF8.GetBytes(_gameData.ToString()));
    }

    public void OnWebSocketMessage(object sender, WebSocketSharp.MessageEventArgs e)
    {
        Debug.Log("Receive Message" + e.Data);
        _receiveData = JsonUtility.FromJson<GameData>(e.Data);
    }
}
