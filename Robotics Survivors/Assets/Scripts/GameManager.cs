using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Класс GameManager управляет игрой
public class GameManager : MonoBehaviour
{
    private GameObject player; // объект игрока
    private ObjectMovement playerMovement; // объект передвижения игрока
    private ObjectStats playerStats; // объект статов игрока
    private CameraMovement cameraMovement; // объект передвижения камеры

    [SerializeField] GameObject meleeUnitPrefab; // префаб врага ближнего боя
    [SerializeField] GameObject playerPrefab; // префаб игрока

    [SerializeField] GameObject panel; // панель паузы UI
    [SerializeField] GameObject pauseButton; // кнопка паузы UI

    [SerializeField] GameObject gunPrefab; // префаб пушки
    [SerializeField] GameObject quadrupleGunPrefab; // префаб четверной пушка игроку

    private int numberOfObjects = 20;
    private float radius;

    Vector3 pos;

    // Событие изменения режима игры
    public delegate void OnChangeGameMode(bool isPlaying);
    public static OnChangeGameMode onChangeGameMode;

    // Контейнер оружий игрока
    WeaponContainer playerWeaponContainer;

    // Метод Awake вызывается раньше чем метод Start у других объектов
    void Awake()
    {
        Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity); // создаём игрока из префаба

        player = GameObject.FindGameObjectWithTag("Player"); // находим объект игрока по тегу
        playerMovement = player.GetComponent<ObjectMovement>(); // получаем скрипт передвижения игрока
        playerStats = player.GetComponent<ObjectStats>(); // получаем статы игрока
        playerWeaponContainer = player.GetComponent<WeaponContainer>(); // получаем статы игрока
        
        // Выдаём оружие игроку
        GiveWeapon(player, Instantiate(quadrupleGunPrefab, player.transform.position, Quaternion.identity));

        cameraMovement = Camera.main.GetComponent<CameraMovement>(); // получаем скрипт передвижения камеры
    }

    void Start()
    {
        // radius = Screen.width / 2;
        radius = Screen.height / 16;

        for (int i = 0; i < numberOfObjects; i++)
        {
            pos = new Vector3(Random.Range(player.transform.position.x, radius), Random.Range(player.transform.position.y, radius), 0);
            SuicideBomber suicideBomber = Instantiate(meleeUnitPrefab, pos, Quaternion.identity).GetComponent<SuicideBomber>();
            suicideBomber.target = player;
        }
        Weapon.onObjectDamage += ObjectDamage;
        ProjectileMovement.onObjectDamage += ObjectDamage;
    }

    private void CreateMeleeUnits(){
        
    }

    void FixedUpdate()
    {   
        // Если хп игрока меньше его начального, то хилим его
        if(playerStats.hp < playerStats.maxHp) playerStats.hp += playerStats.healPerFrame;
    }

    // Метод ObjectDamage наносит урон цели
    public void ObjectDamage(float damage, GameObject target)
    {
        ObjectStats stats = target.GetComponent<ObjectStats>();
        // уменьшаем хп
        stats.hp -= damage;
        if(stats.hp <= 0)
        {
            // Уничтожаем объект, если не игрок
            // и грузим сцену, если игрок
            if(target == player) SceneManager.LoadScene(0);
            else Destroy(target);
        }
    }

    // Метод Pause ставит игру на паузу
    public void Pause()
    {
        // Вызов события изменения режима игры
        onChangeGameMode?.Invoke(false);
        // Ставим неактивной и невидимой кнопку паузы
        pauseButton.SetActive(false);
        // Панель ставим активной и видимой
        panel.SetActive(true);
        // Изменение времени на 0, чтобы остановить игрока и врагов
        Time.timeScale = 0f;
    }

    // Метод Continue продолжает игру
    public void Continue()
    {
        onChangeGameMode?.Invoke(true);
        panel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }

    // Метод GiveWeapon выдаёт оружие объекту
    public void GiveWeapon(GameObject master, GameObject weapon)
    {
        // Ставим созданному объекту родителя, чтобы прикрепить и оружие
        // перемещалось вместе с игроком
        weapon.transform.SetParent(master.transform);
        // Если хозяин оружия - игрок, то переменную бул ставим равное true
        if(master == player) weapon.GetComponent<Gun>().fromPlayer = true;
        // Вызываем метод добавления оружия
        master.GetComponent<WeaponContainer>().AddWeapon(weapon);
    }
}
