using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Интерфейс IWeapon требует реализации геттеров, сеттеров параметров
// и метода Use.
// Вместо объекта определённого класса подставляется объект интерфейса.
// очень удобно.
public interface IWeapon
{
    string Name {
        get;
        set;
    }

    float Damage {
        get;
        set;
    }

    float Recharge {
        get;
        set;
    }

    void Use();
}
