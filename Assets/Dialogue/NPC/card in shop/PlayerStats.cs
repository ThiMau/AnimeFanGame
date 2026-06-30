using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public int attack = 10;

    public int defense = 0;

    public int maxHP = 100;

    void Awake()
    {
        Instance = this;
    }
}