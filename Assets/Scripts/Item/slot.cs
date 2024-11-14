using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slot : MonoBehaviour
{
    [SerializeField] private Image itemSprite;

    private void Awake()
    {
        itemSprite = GetComponentInChildren<Image>();
    }
}
