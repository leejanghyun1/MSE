using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using System;
using System.Threading;
using System.Text;
using UnityEngine.Networking.PlayerConnection;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;

    public WebSocketSharp.WebSocket ws = null;
    public float x;
    public float z;

    // Start is called before the first frame update
    void Start()
    {       
        Instance = this;
        ws = new WebSocketSharp.WebSocket("ws://43.201.219.112:8080/api/socket");

        ws.OnMessage += OnWebSocketMessage;
        ws.Connect();
    }

    public void OnWebSocketMessage(object sender, WebSocketSharp.MessageEventArgs e)
    {
        Debug.Log("Receive Message");

        Payload payload = JsonUtility.FromJson<Payload>(e.Data);
        x = payload.move_x;
        z = payload.move_z;
    }

    void Update()
    {
        if(ws == null)
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.Space)) {
            Payload payload = new Payload
            {
                move_x = 1f,
                move_z = 2f,
                rotation = 3f,
                On_handle = 1,
                Is_interact = 0,
                hand_index = 0
            };

            string payloadJson = JsonUtility.ToJson(payload);

            ws.Send(payloadJson);
        }
    }

    public void SignUp()
    {
        SignupPayload signupPayload = new SignupPayload
        {
            username = "MSE",
            password = "1234"
        };

        string JsonSingupPayload = JsonUtility.ToJson(signupPayload);

        ws.Send("POST|" + JsonSingupPayload);

        Debug.Log("SignUp");
    }

    [System.Serializable]
    private class SignupPayload
    {
        public string username;
        public string password;
    }

    [System.Serializable]
    private class Payload
    {
        public float move_x;
        public float move_z;
        public float rotation;
        public int On_handle;
        public int Is_interact;
        public int hand_index;
    }
}
