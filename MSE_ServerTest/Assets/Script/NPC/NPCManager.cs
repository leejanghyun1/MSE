using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager instance;

    public int selectNPC;
    [SerializeField] Transform spawn;

    public float spawnTimer = 0f; // 스폰 주기를 위한 타이머 => 추후 스폰 주기를 디자인해서 수정한다.
    public float cycle = 5f; // NPC 스폰하는 주기
    public int NPCCount; // NPC가 스폰된 수 확인 

    public int Order_count; // 주문이 들어가 있는 수량
    public GameObject[] Pick_Up;
    private int menu_index;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if(NPCCount >= transform.childCount) 
            spawnTimer = 0f;

        if(spawnTimer >= cycle)
        {
            spawnTimer = 0f;
            Spawn();

            //주문이 4개 이상이면 주문 안함
            if(Order_count < 4)
            {
                Order();
            }

            NPCCount++;
        }
    }

    // 어떤 NPC를 내보낼 지 랜덤으로 고른다.
    void Spawn()
    {
        //NPC번호중 랜덤으로 고르기
        selectNPC = Random.Range(0, transform.childCount);

        // 이미 스폰되어 있는 NPC는 다시 스폰 안되게끔
        if (!transform.GetChild(selectNPC).gameObject.activeSelf) 
        {
            transform.GetChild(selectNPC).position = spawn.position + new Vector3(0,1,0);
            transform.GetChild(selectNPC).gameObject.GetComponent<NPC>().state = NPC.State.Wait;
            transform.GetChild(selectNPC).gameObject.SetActive(true);
        }
        else
        {
            Spawn();
        }
    }

    void Order()
    {
        for (int i = 0; i < Pick_Up.Length; i++)
        {
            //wait중인 Food_Out중 하나 선택
            if (Pick_Up[i].gameObject.GetComponent<Food_Out>().menu == Food_Out.Menu.wait)
            {
                //랜덤으로 메뉴 고르기
                menu_index = Random.Range(0, 4);
                switch (menu_index)
                {
                    case 0:
                        Pick_Up[i].gameObject.GetComponent<Food_Out>().menu = Food_Out.Menu.single;
                        Pick_Up[i].gameObject.GetComponent<Food_Out>().Is_order = true;
                        break;
                    case 1:
                        Pick_Up[i].gameObject.GetComponent<Food_Out>().menu = Food_Out.Menu.cokeset;
                        Pick_Up[i].gameObject.GetComponent<Food_Out>().Is_order = true;
                        break;
                    case 2:
                        Pick_Up[i].gameObject.GetComponent<Food_Out>().menu = Food_Out.Menu.friedset;
                        Pick_Up[i].gameObject.GetComponent<Food_Out>().Is_order = true;
                        break;
                    case 3:
                        Pick_Up[i].gameObject.GetComponent<Food_Out>().menu = Food_Out.Menu.fullset;
                        Pick_Up[i].gameObject.GetComponent<Food_Out>().Is_order = true;
                        break;
                }
                break;
            }  
        }
        Order_count++;
    }
}