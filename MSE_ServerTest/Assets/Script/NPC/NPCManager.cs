using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager instance;

    public int selectNPC;
    [SerializeField] Transform spawn;

    public float spawnTimer = 0f; // ���� �ֱ⸦ ���� Ÿ�̸� => ���� ���� �ֱ⸦ �������ؼ� �����Ѵ�.
    public float cycle = 5f; // NPC �����ϴ� �ֱ�
    public int NPCCount; // NPC�� ������ �� Ȯ�� 

    public int Order_count; // �ֹ��� �� �ִ� ����
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

            //�ֹ��� 4�� �̻��̸� �ֹ� ����
            if(Order_count < 4)
            {
                Order();
            }

            NPCCount++;
        }
    }

    // � NPC�� ������ �� �������� ����.
    void Spawn()
    {
        //NPC��ȣ�� �������� ����
        selectNPC = Random.Range(0, transform.childCount);

        // �̹� �����Ǿ� �ִ� NPC�� �ٽ� ���� �ȵǰԲ�
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
            //wait���� Food_Out�� �ϳ� ����
            if (Pick_Up[i].gameObject.GetComponent<Food_Out>().menu == Food_Out.Menu.wait)
            {
                //�������� �޴� ����
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