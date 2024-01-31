using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Класс SceneLoader загружает сцену, нужен там, где запущена сцена меню
public class SceneLoader : MonoBehaviour
{
    // Метод Load загружает сцену по позиции.
    // Сцены определяются в настройках Билда (Build Settings).
    public void Load(int number)
    {
        SceneManager.LoadScene(number);
    }
}
