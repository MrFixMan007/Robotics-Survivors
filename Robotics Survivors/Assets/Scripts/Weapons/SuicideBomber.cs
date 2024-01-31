using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс SuicideBomber наследуется от Weapon и реализует IWeapon.
// Концепция: Враг дотрагивается до игрока, наносит ему урон и самоуничтожается
public class SuicideBomber : Weapon, IWeapon
{    
    public string Name {
        get {return name;}
        set {name = value;}
    }

    public float Damage {
        get {return damage;}
        set {damage = value;}
    }

    public float Recharge {
        get {return recharge;}
        set {recharge = value;}
    }

    // метод Use вызывает событие наненсения урона цели и уничтожает объект, который нёс оружие
    public void Use()
    {
        onObjectDamage?.Invoke(damage, target);
        Destroy(gameObject);
    }

    // стандартный Unity-метод OnCollisionEnter2D вызывается при пересечении Collider обекта с другим
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // если дотронулся до объекта цели, то вызываем метод
        if(collision.gameObject == target)
        {
            Use();
        }
    }
}
