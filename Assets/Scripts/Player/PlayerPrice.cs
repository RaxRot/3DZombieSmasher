using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrice : MonoBehaviour
{
    [SerializeField] private int price;

    public int GetPrice()
    {
        return price;
    }
}
