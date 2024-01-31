using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс ObjectMovement начальный класс передвижения, от которго наследуются другие.
public class ObjectMovement : MonoBehaviour
{
    // нужен объект со статами, откуда берется скорость
    [SerializeField] ObjectStats stats;

    // Move метод передвигает объект в направлении умноженной на скорость
    public void Move(Vector3 target_move)
    {
        transform.Translate(target_move * stats.speed * Time.deltaTime);
    }
}
