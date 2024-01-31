using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс ProjectileMovement наследуется от класса ObjectMovement и реализует интерфейс IProjectile.
// оперделяет передвижение прожектайла, например пули.
public class ProjectileMovement : ObjectMovement, IProjectile
{
    // Запоминаем прожектайл вражеский или нет
    [HideInInspector] public bool fromPlayer;
    // Из оружия возьмём данные урона
    [HideInInspector] public IWeapon iWeapon;
    // Нормаль направления
    [HideInInspector] public Vector3 targetVector;
    // Время жизни прожектайла
    public float lifeTime;

    // События нанесения урона
    public delegate void OnObjectDamage(float damage, GameObject target);
    public static OnObjectDamage onObjectDamage;

    void Update()
    {
        // Каждый кадр двигаем прожектайл
        Move(targetVector);
    }

    void FixedUpdate()
    {
        // Каждый фиксированный кадр уменьшаем время
        lifeTime -= Time.fixedDeltaTime;
        // Уничтожаем прожектайл, если время вышло
        if(lifeTime <= 0) Destroy(gameObject);
    }

    // Метод Use вызывет событие нанесения урона и уничтожит прожектайл
    public void Use(GameObject target)
    {
        onObjectDamage?.Invoke(iWeapon.Damage, target); //TODO урон по-другому
        Destroy(gameObject);
    }

    // OnCollisionEnter2D вызывается при пересечении Collider обекта с другим
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // если дотронулся дружеский снаряд до врага
        // или если вражеский снаряд дотронулся до игрока, то вызываем метод
        if((fromPlayer && !collision.gameObject.CompareTag("Player"))
        || (!fromPlayer && collision.gameObject.CompareTag("Player")))
        {
            Use(collision.gameObject);
        }
    }
}
