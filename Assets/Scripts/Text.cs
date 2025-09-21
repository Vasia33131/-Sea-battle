using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text : MonoBehaviour
{
    public TMP_Text[] textToCollapse; //Массив текста

    void Update()
    {
        //Проверяем нажатие любой кнопки
        if (Input.anyKeyDown)
        {
            //Отключаем все GameObject в массиве
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