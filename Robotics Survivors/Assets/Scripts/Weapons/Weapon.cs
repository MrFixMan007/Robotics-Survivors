using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс Weapon - начальный класс, от которого наследуются конкретные классы оружий
public class Weapon : MonoBehaviour
{
    // параметры оружия урон, время перезарядки, название
    public float damage;
    public float recharge;
    public string name;

    // делегат для события, на который подпишутся другие классы
    public delegate void OnObjectDamage(float damage, GameObject target);
    public static OnObjectDamage onObjectDamage;

    // публичный, но скрытый в инспекторы объект цели
    [HideInInspector] public GameObject target;
}
