using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс EnemyMovement наследуется от класса ObjectMovement,
// находит игрока и двигается в его сторону
public class EnemyMovement : ObjectMovement
{
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // находим объект игрока по тегу
    }

    void Update()
    {
        // передвигаем врага в сторону игрока
        Move((player.transform.position - transform.position).normalized);
    }
}
