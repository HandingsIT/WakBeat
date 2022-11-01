using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObjectTrophy : MonoBehaviour
{
    // �˾�â ȣ�� �� UI ����
    private float _duration = 0.15f;

    // �˾� â ȣ�� �� UI ��� ����
    private void OnEnable()
    {
        // ������ 0���� ����
        this.transform.localScale = Vector3.zero;
        // 1���� Ŀ���鼭 �ð��� 0.2��, ��ȯ �� ť�� ���·� ����
        this.transform.DOScale(Vector3.one, _duration).SetEase(Ease.InCubic);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnClickHide()
    {
        this.transform.parent.gameObject.SetActive(false);
    }
}
