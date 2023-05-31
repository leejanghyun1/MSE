using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_Out : MonoBehaviour
{
    public enum Menu {single, cokeset, friedset, fullset, done, wait}
    public Menu menu;
    public static Food_Out Instance;

    [SerializeField]
    private GameObject[] foods;

    public GameObject NPCmanager;
    public GameObject Menu_UI;

    public bool Is_interact = false;
    public bool Is_order = false;
    public bool NPC_pick = false;

    void Awake()
    {
        //�ʱ�ȭ
        Instance = this;
        menu = Menu.wait;
        Menu_UI.SetActive(false);
    }

    void Update()
    {
        if (Is_interact)
        {
            //ī���� ���� ���� �÷��α�
            if (Player.Instance.hand_index == 0)
            {
                foods[0].gameObject.SetActive(true);
            }
            else if (Player.Instance.hand_index == 1)
            {
                foods[1].gameObject.SetActive(true);
            }
            else if (Player.Instance.hand_index == 2)
            {
                foods[2].gameObject.SetActive(true);
            }

            Is_interact = false;
        }

        //NPC�� �������� ���� ������Ʈ�� �� ����
        if (NPC_pick)
        {
            foods[0].gameObject.SetActive(false);
            foods[1].gameObject.SetActive(false);
            foods[2].gameObject.SetActive(false);

            NPC_pick = false;
        }

        //�޴��� ����
        if(Is_order)
        {
            //�޴��� ������ ���� UI�� �ٸ�������� ��������
            switch(menu)
            {
                case Menu.single:
                    Menu_UI.SetActive(true);
                    Menu_UI.transform.GetChild(1).gameObject.SetActive(false);
                    Menu_UI.transform.GetChild(2).gameObject.SetActive(false);
                    break;

                case Menu.cokeset:
                    Menu_UI.SetActive(true);
                    Menu_UI.transform.GetChild(2).gameObject.SetActive(false);
                    break;

                case Menu.friedset:
                    Menu_UI.SetActive(true);
                    Menu_UI.transform.GetChild(1).gameObject.SetActive(false);
                    break;

                case Menu.fullset:
                    Menu_UI.SetActive(true);
                    break;

            }
            Is_order = false;
        }

        // �޴��� ���� ����ȹ��
        switch (menu)
        {
            case Menu.single:
                if (foods[2].gameObject.activeSelf)
                {
                    //����ȹ��
                    Gamemanager.instance.slider.value += 0.002f;
                    for(int i = 0; i < NPCmanager.gameObject.transform.childCount; i++)
                    {
                        if (NPCmanager.gameObject.transform.GetChild(i).GetComponent<NPC>().state == NPC.State.Wait
                            && NPCmanager.gameObject.transform.GetChild(i).gameObject.activeSelf)
                        {
                            NPCmanager.gameObject.transform.GetChild(i).GetComponent<NPC>().state = NPC.State.Order;
                            break;
                        }
                    }
                    menu = Menu.done;
                    NPCManager.instance.Order_count--;
                }
                break;

            case Menu.cokeset:
                if (foods[2].gameObject.activeSelf && foods[0].gameObject.activeSelf)
                {
                    //����ȹ��
                    Gamemanager.instance.slider.value += 0.004f;
                    for (int i = 0; i < NPCmanager.gameObject.transform.childCount; i++)
                    {
                        if (NPCmanager.gameObject.transform.GetChild(i).GetComponent<NPC>().state == NPC.State.Wait
                            && NPCmanager.gameObject.transform.GetChild(i).gameObject.activeSelf)
                        {
                            NPCmanager.gameObject.transform.GetChild(i).GetComponent<NPC>().state = NPC.State.Order;
                            break;
                        }
                    }
                    menu = Menu.done;
                    NPCManager.instance.Order_count--;
                }
                break;

            case Menu.friedset:
                if (foods[2].gameObject.activeSelf && foods[1].gameObject.activeSelf)
                {
                    //����ȹ��
                    Gamemanager.instance.slider.value += 0.006f;
                    for (int i = 0; i < NPCmanager.gameObject.transform.childCount; i++)
                    {
                        if (NPCmanager.gameObject.transform.GetChild(i).GetComponent<NPC>().state == NPC.State.Wait
                            && NPCmanager.gameObject.transform.GetChild(i).gameObject.activeSelf)
                        {
                            NPCmanager.gameObject.transform.GetChild(i).GetComponent<NPC>().state = NPC.State.Order;
                            break;
                        }
                    }
                    menu = Menu.done;
                    NPCManager.instance.Order_count--;
                }
                break;

            case Menu.fullset:
                if (foods[2].gameObject.activeSelf && foods[1].gameObject.activeSelf && foods[0].gameObject.activeSelf)
                {
                    //����ȹ��
                    Gamemanager.instance.slider.value += 0.01f;
                    for (int i = 0; i < NPCmanager.gameObject.transform.childCount; i++)
                    {
                        if (NPCmanager.gameObject.transform.GetChild(i).GetComponent<NPC>().state == NPC.State.Wait
                            && NPCmanager.gameObject.transform.GetChild(i).gameObject.activeSelf)
                        {
                            NPCmanager.gameObject.transform.GetChild(i).GetComponent<NPC>().state = NPC.State.Order;
                            break;
                        }
                    }
                    menu = Menu.done;
                    NPCManager.instance.Order_count--;
                }
                break;

            case Menu.done:
                Menu_UI.transform.GetChild(1).gameObject.SetActive(true);
                Menu_UI.transform.GetChild(2).gameObject.SetActive(true);
                Menu_UI.transform.GetChild(3).gameObject.SetActive(true);
                Menu_UI.SetActive(false);
                break;
        }
    }
}
