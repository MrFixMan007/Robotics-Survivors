using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс Gun наследуется от Weapon и реализует IWeapon.
// Концепция: Нестандартное ружьё, которому можно добавить несколько стволов.
// Выстреливает из каждого ствола, перезаряжается. 
public class Gun : Weapon, IWeapon
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

    float tic; // для перезарядки
    public bool fromPlayer = false; // разные реализации, если (или нет) от игрока
    Vector3[] vectors; // массив нормализированных векторов для указания направления движения пули
    Transform[] tubes; // массив позиций концов ствола, откуда пули вылетают 
    [SerializeField] GameObject projectilePrefab; // префаб пули

    void Start()
    {
        tic = 0;
        // создадим 2 массива длиной, равной кол-ву вложенных элементов
        // являющиеся стволами
        vectors = new Vector3[transform.childCount];
        tubes = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            // i-й элемент массива векторов равна нормали
            // (позиция конца ствола (является подПодчинённому элементу)
            // минус позиция ствола (подчинённому элементу))
            vectors[i] = (transform.GetChild(i).GetChild(0).position - transform.GetChild(i).position).normalized;

            // i-й элемент массива позиций равна позиции подПодчинённого элемента
            tubes[i] = transform.GetChild(i).GetChild(0);
        }
    }

    void Update()
    {
        // Если был выстрел, то вычитает Тик из переменной
        if(tic > 0)
        {
            tic -= Time.deltaTime;
        }
    }

    // метод Use создаём пули по префабу и настраивает их
    public void Use()
    {
        // Если перезарядились
        if(tic <= 0)
        {
            // Проходимся по всем вложенным пушкам
            for (int i = 0; i < transform.childCount; i++)
            {
                // Создаём пулю на позиции i-ого конца ствола и берем его скрипт ProjectileMovement для настройки 
                ProjectileMovement projectile = Instantiate(projectilePrefab, tubes[i].position, Quaternion.identity).GetComponent<ProjectileMovement>();
                
                // Ставим значение отИгрока
                projectile.fromPlayer = fromPlayer;
                // Указываем направление движения пули по i-ому значению массива, равному нормали
                projectile.targetVector = vectors[i];
                // Привязываем это оружие, из которого была выпущена пуля, к пуле
                projectile.iWeapon = this;
            }
            // Ставим время перезарядки
            tic = recharge;
        }
    }
}
