using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text : MonoBehaviour
{
    public TMP_Text[] textToCollapse; //������ ������

    void Update()
    {
        //��������� ������� ����� ������
        if (Input.anyKeyDown)
        {
            //��������� ��� GameObject � �������
            foreach (TMP_Text text in textToCollapse)
            {
                if (text != null)
                {
                    text.gameObject.SetActive(false);
                }
            }
        }
    }
}