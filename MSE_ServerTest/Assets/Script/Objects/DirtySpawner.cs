using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtySpawner : MonoBehaviour
{
    public static DirtySpawner Instance;

    public float _SpawnTime;
    public int _SpawnCount;

    [SerializeField]
    private Transform[] _SpawnPoints;
    [SerializeField]
    private GameObject _DirtyPrefab;

    GameObject tmp;

    private void Start()
    {
        Instance = this;
        _SpawnTime = NPCManager.instance.cycle;
        _SpawnCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(_SpawnCount < 5)
        {
            //�� �����Ӹ��� ��Ÿ�� ����
            _SpawnTime -= Time.deltaTime;
        }
        else
        {
            //5���� �� �����Ǿ��ִ� ��쿡��
            //����Ʈ ��Ÿ���� NPC���� ��Ÿ�Ӱ� ����ȭ ��Ŵ
            _SpawnTime = NPCManager.instance.cycle;
        }
        

        //��Ÿ�� �� ���� ����
        if( _SpawnTime < 0 )
        {
            DirtySpawn();
        }
    }

    void DirtySpawn()
    {
        int idx = Random.Range( 0, _SpawnPoints.Length);

        //�ڽ� ������Ʈ�� ������ ��Ƽ ����
        if (_SpawnPoints[idx].childCount == 0)
        {
            tmp = Instantiate(_DirtyPrefab, _SpawnPoints[idx].position, _DirtyPrefab.transform.rotation);
            tmp.transform.SetParent(_SpawnPoints[idx]);
            tmp.SetActive(true);
            _SpawnCount++;
            _SpawnTime = Random.Range(5.0f, 10.0f);
        }
    }
}
