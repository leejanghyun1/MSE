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
            //매 프레임마다 쿨타임 감소
            _SpawnTime -= Time.deltaTime;
        }
        else
        {
            //5개가 다 스폰되어있는 경우에는
            //디폴트 쿨타임인 NPC스폰 쿨타임과 동일화 시킴
            _SpawnTime = NPCManager.instance.cycle;
        }
        

        //쿨타임 다 돌면 스폰
        if( _SpawnTime < 0 )
        {
            DirtySpawn();
        }
    }

    void DirtySpawn()
    {
        int idx = Random.Range( 0, _SpawnPoints.Length);

        //자식 오브젝트가 없으면 덜티 생성
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
