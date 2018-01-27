﻿using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class HealthBar : MonoBehaviour
{
    public GameObject Zero, One;
    public string CurrentHP;
    public Transform Parent;
    
    private GameObject[] currentHPBar;
    private bool[] isActive;

    public void Start()
    {
        currentHPBar = new GameObject[] { };

        SetUpHpBar();
    }

    public void TakeDamage(char fromWhat)
    {
        var i = 0;
        while (!isActive[i] && i < isActive.Length)
        {
            i++;
        }

        if (fromWhat == CurrentHP[i])
        {
            currentHPBar[i].SetActive(false);
            isActive[i] = false;
        }
        else
        {
            for (int j = 0; j < i; j++)
            {
                currentHPBar[j].SetActive(true);
                isActive[j] = true;
            }
        }
    }
    
    private static string GenerateNewHP()
    {
        return Convert.ToString(Random.Range(1, 31), 2).PadLeft(5, '0');
    }

    private void SetUpHpBar()
    {
        CurrentHP = GenerateNewHP();
        
        foreach (var o in currentHPBar)
        {
            Destroy(o);
        }

        currentHPBar = new GameObject[CurrentHP.Length];
        isActive = Enumerable.Repeat(true, CurrentHP.Length).ToArray();

        for (var i = 0; i < CurrentHP.Length; i++)
        {
            currentHPBar[i] = Instantiate(CurrentHP[i] == '0' ? Zero : One, transform);
        }
    }

    private void Update()
    {
        var parentPos = Camera.main.WorldToScreenPoint(Parent.position);
        transform.position = parentPos;
    }
}
