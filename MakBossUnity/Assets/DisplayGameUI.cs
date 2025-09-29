using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGameUI : MonoBehaviour
{
    private Mushroom mushroom; //���� ��ü�� �����´�. �������� �Ծ���! �Լ��� ����

    [SerializeField] Image Bosshealthbar; // � ü�¹ٸ� �������� ������
    [SerializeField] Image HUDhealthbar;
    [SerializeField] TextMeshProUGUI ragedText;

    private void Awake()
    {
        mushroom = GetComponent<Mushroom>();
    }

    private void Start()
    {
        ragedText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        mushroom.OnHealthBarUpdate += OnUpdateHealthBar; // int, int �Լ��� ������ �� �ִ� Ÿ���� �����غ�����. ->Action< > �� Ÿ�� ���� OnHealthbarUpdate
        mushroom.OnPatternStart += OnRaged;
    }

    private void OnDisable()
    {
        mushroom.OnHealthBarUpdate -= OnUpdateHealthBar;
        mushroom.OnPatternStart -= OnRaged;

    }

    private void OnRaged(bool enable)
    {
        if(ragedText.gameObject.activeSelf == false)
        ragedText.gameObject.SetActive(enable);
    }

    private void OnUpdateHealthBar(int current, int max) // ���� ü��, �ִ� ü�� -> 0 ~ 1 �Ҽ��� => fillamount
    {
        if(Bosshealthbar != null)
            Bosshealthbar.fillAmount = (float)current / max;
        if (HUDhealthbar != null)
            HUDhealthbar.fillAmount = (float)current / max;
    }
}
