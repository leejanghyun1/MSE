using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Dirty : MonoBehaviour
{
    public GameObject _UICanvas;
    public Slider _sliderPrefab;
    public bool Is_interact = false;
    private int _count = 0;
    Slider slider;
    Camera _camera;

    void OnEnable()
    {
        _UICanvas = GameObject.Find("UICanvas"); // UIĵ���� ������Ʈ�� ã�ƿ�
        _camera = Camera.main;
        slider = Instantiate(_sliderPrefab, this.transform.position, Quaternion.identity);
        slider.transform.SetParent(_UICanvas.transform);
        slider.transform.SetAsFirstSibling();
    }

    void Update()
    {
        //ȭ�鿡 �����̴� ����
        slider.transform.position = _camera.WorldToScreenPoint(this.transform.position 
            + new Vector3(0f, 1.5f, 0f));

        Interact();

        //�������� 0�� �Ǿ��� ��
        if(slider.value == 0)
        {
            DirtySpawner.Instance._SpawnCount--;
            Player.Instance.Is_interact = true;
            Destroy(slider.gameObject);
            Destroy(this.gameObject);
        }
    }

    // Interact �޾��� �� �۵�
    void Interact()
    {
        if(Is_interact)
        {
            _count++;
            slider.value = (float)(1 - (0.2 * _count));
            Is_interact = false;
        }
    }
}
