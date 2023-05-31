using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using System;
using System.Threading;
using System.Text;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbySceneManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private string url;
    [SerializeField] private InputField inputUsername;
    [SerializeField] private InputField inputPassword;
    [SerializeField] private Text stateTxt;
    [Header("Server")]
    [SerializeField] private string token;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void BtnSignUp()
    {
        url = "http://43.201.219.112:8080/api/users";
        Payload signUpForm = new Payload
        {
            username = inputUsername.text,
            password = inputPassword.text,
        };
        string json = JsonUtility.ToJson(signUpForm);
        StartCoroutine(SendSignUp(url, json));
    }

    IEnumerator SendSignUp(string url, string json)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(url, json))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            www.uploadHandler.Dispose();
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.error == null)
            {
                Debug.Log("Sign Up Success");
                stateTxt.color = Color.black;
                stateTxt.text = "Sign Up Success";
                www.Dispose();
            }
            else
            {
                stateTxt.color = Color.red;
                stateTxt.text = "Fail Sign Up";
                Debug.Log("error");
                www.Dispose();
            }
            www.Dispose();
        }
    }

    public void BtnLogIn()
    {
        url = "http://43.201.219.112:8080/api/users/login";
        Payload logInForm = new Payload
        {
            username = inputUsername.text,
            password = inputPassword.text,
        };
        string json = JsonUtility.ToJson(logInForm);
        StartCoroutine(SendLogIn(url, json));
    }

    IEnumerator SendLogIn(string url, string json)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(url, json))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            www.uploadHandler.Dispose();
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();
            if (www.error == null)
            {
                string responseTxt = www.downloadHandler.text;
                string[] jsonResponese = responseTxt.Split('"');
                token = jsonResponese[3];
                Debug.Log(token);
                TokenManager.instance.token = this.token;
                stateTxt.color = Color.black;
                stateTxt.text = "Log In !";
                StartCoroutine(LoadInGame());
            }
            else
            {
                stateTxt.color = Color.red;
                stateTxt.text = "Please check your username or password";
            }
            www.Dispose();
        }
    }

    IEnumerator LoadInGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("IngameScene");
    }

    [System.Serializable]
    private class Payload
    {
        public string username;
        public string password;
    }
}
