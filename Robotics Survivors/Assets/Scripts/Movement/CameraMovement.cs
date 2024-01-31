using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс CameraMovement сам может найти игрока и перемещает камеру туда, где игрок
public class CameraMovement : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // находим игрока по тегу
    }

    void Update()
    {
        // перемещаем камеру туда, где игрок
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }

    // SetPlayer устанавливает игрока
    public void SetPlayer(GameObject gameObject)
    {
        player = gameObject;
    }
}
