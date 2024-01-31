using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс ObjectStats определяет статы Юнита
public class ObjectStats : MonoBehaviour
{
    public float hp;
    [HideInInspector] public float maxHp;

    public float speed;
    public float healPerFrame;

    void Start()
    {
        // Максимальное хп установится равному хп начальному
        maxHp = hp;
    }
}
