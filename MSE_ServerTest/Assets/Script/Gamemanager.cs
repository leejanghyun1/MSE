using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    public float playtime = 123.0f;
    public float _changetime = 75.0f; //���� ���� ����
    public float Alpha_time = -0.1f; //ī��Ʈ�ٿ� ���̵�ƿ� ����� ����
    public int point;

    private bool _esc = false;

    [Header("UI")]
    [SerializeField]
    private Text time_text;
    [SerializeField]
    private Text Role_text;
    [SerializeField]
    private Text CountDown_text;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject setting;
    [SerializeField]
    private Image image;

    public Slider slider; // ����Ʈ ǥ�� �����̴�

    private Color _cookColor; //cook���� ����
    private Color _hallColor; //hall���� ����

    Coroutine coroutine;

    private void Awake()
    {
        instance = this;
        image.gameObject.SetActive(false);
        setting.SetActive(false);
        slider.value = 0.5f; //�����̴� ���� ��ġ�� �̵�
        _cookColor = new Color(0.6352941f, 0.227451f, 0.282353f);
        _hallColor = new Color(0.8705883f, 0.8078432f, 0.3333333f);
        coroutine = StartCoroutine(_DecreaseScale(slider));
    }

    void Update()
    {
        _changetime -= Time.deltaTime;
        playtime -= Time.deltaTime;

        ChangeRole(_changetime);

        if(_changetime <= 5f)
        {
            _FadeOut(_changetime);
        }

        if(playtime < 60)
        {
            time_text.text = "Time: " + string.Format("{0:F2}", playtime);
        }
        else
        {
            time_text.text = $"Time: {(int)(playtime/60)}:" + ((int)playtime - (60 * (int)(playtime / 60))).ToString("D2");
        }

        //���� ����
        /*
        if(playtime < 0)
        {
            time_text.gameObject.SetActive(false);
            point_text.gameObject.SetActive(false);
            menu.SetActive(false);
            image.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        */

        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(_esc)
            {
                Time.timeScale = 1;

                time_text.gameObject.SetActive(true);
                Role_text.gameObject.SetActive(true);
                menu.SetActive(true);
                slider.gameObject.SetActive(true);

                setting.SetActive(false);
                _esc = false;
            }
            else
            {
                Time.timeScale = 0;

                time_text.gameObject.SetActive(false);
                Role_text.gameObject.SetActive(false);
                menu.SetActive(false);
                slider.gameObject.SetActive(false);

                setting.SetActive(true);
                _esc = true;
            }
            
        }

        Role_text.text = $"{Player.Instance._role}";
    }

    //���� ����� ��ư
    public void Resume()
    {
        setting.SetActive(false);

        time_text.gameObject.SetActive(true);
        Role_text.gameObject.SetActive(true);
        menu.SetActive(true);
        slider.gameObject.SetActive(true);

        Time.timeScale = 1;
    }

    //���� �����ư
    public void Exit()
    {
        Application.Quit();
    }

    //point_bar ����
    IEnumerator _IncreaseScale(Slider s)
    {
        while(true)
        {
            s.value += 0.0018f;
            yield return new WaitForSeconds(1);
        }
        
    }

    //point_bar ����
    IEnumerator _DecreaseScale(Slider s)
    {
        while (true)
        {
            s.value -= 0.0018f;
            yield return new WaitForSeconds(1);
        }

    }

    //point_bar ���� ����
    void ChageColor(Player.Role role)
    {
        if(role == Player.Role.Cook)
        {
            //background ����
            slider.transform.GetChild(0).gameObject.GetComponent<Image>().color = _hallColor;
            //fill ����
            slider.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = _cookColor;

            StopCoroutine(coroutine);
            coroutine = StartCoroutine(_DecreaseScale(slider));
        }
        else if(role == Player.Role.Hall)
        {
            //background ����
            slider.transform.GetChild(0).gameObject.GetComponent<Image>().color = _cookColor;
            //fill ����
            slider.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = _hallColor;

            StopCoroutine(coroutine);
            coroutine = StartCoroutine(_IncreaseScale(slider));
        }
    }

    // ���� ����
    void ChangeRole(float time)
    {
        if(time < 0)
        {
            CountDown_text.gameObject.SetActive(false);
            _changetime = 75.0f;
            Player.Instance.Changed();
            ChageColor(Player.Instance._role);
        }
    }

    // ī��Ʈ �ٿ�
    void _FadeOut(float time)
    {
        
        if(time < 5f && time >= 4f)
        {
            CountDown_text.gameObject.SetActive(true);
            if (Alpha_time < 0f)
            {
                Alpha_time = 1f;
            }
            else
            {
                CountDown_text.text = "5";
                Alpha_time -= Time.deltaTime;
                CountDown_text.color = new Color(1, 1, 1, Alpha_time);   
            }
            
        }
        else if(time < 4f && time >= 3f)
        {
            if (Alpha_time < 0f)
            {
                Alpha_time = 1f;
            }
            else
            {
                CountDown_text.text = "4";
                Alpha_time -= Time.deltaTime;
                CountDown_text.color = new Color(1, 1, 1, Alpha_time);
            }
        }
        else if (time < 3f && time >= 2f)
        {
            if (Alpha_time < 0f)
            {
                Alpha_time = 1f;
            }
            else
            {
                CountDown_text.text = "3";
                Alpha_time -= Time.deltaTime;
                CountDown_text.color = new Color(1, 1, 1, Alpha_time);
            }
        }
        else if (time < 2f && time >= 1f)
        {
            if (Alpha_time < 0f)
            {
                Alpha_time = 1f;
            }
            else
            {
                CountDown_text.text = "2";
                Alpha_time -= Time.deltaTime;
                CountDown_text.color = new Color(1, 1, 1, Alpha_time);
            }
        }
        else if (time < 1f)
        {
            if (Alpha_time < 0f)
            {
                Alpha_time = 1f;
            }
            else
            {
                CountDown_text.text = "1";
                Alpha_time -= Time.deltaTime;
                CountDown_text.color = new Color(1, 1, 1, Alpha_time);
            }
        }
        else
        {
            Alpha_time = -0.1f;
            return;
        }
    }
}
