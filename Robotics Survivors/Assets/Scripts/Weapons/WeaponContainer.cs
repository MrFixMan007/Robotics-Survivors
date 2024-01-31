using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс WeaponContainer для хранения и использования нескольких видов оружия
public class WeaponContainer : MonoBehaviour
{
    // Публичный, но спрятанный в инспекторе объект, который носит оружия
    [HideInInspector] public GameObject master;
    //Список оружий
    private List<IWeapon> weapons = new List<IWeapon>();

    void Start()
    {
        // Привязываем хозяина
        master = gameObject;
    }

    void FixedUpdate()
    {
        // В фиксированные моменты времени проходимся по списку оружий
        // и используем их
        foreach (IWeapon weapon in weapons)
        {
            weapon.Use();
        }
    }

    // Метод AddWeapon добавляет в список оружий новое, которое передали.
    // Если удачно добавил, то вернём true, иначе false
    public bool AddWeapon(GameObject objectWeapon)
    {
        // Получаем объект интерфейса IWeapon у объекта, который передали, для удобства 
        IWeapon weapon = objectWeapon.GetComponent<IWeapon>();

        //Если мы не пробоуем добавить уже имеющееся оружие
        if(!weapons.Contains(weapon))
        {
            weapons.Add(weapon);
            return true;
        }
        return false;
    }
}
