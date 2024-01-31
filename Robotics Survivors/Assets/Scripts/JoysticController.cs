using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс JoysticController показывает игроку направление движения
// и вызывает передвижение 
public class JoysticController : MonoBehaviour
{
    [SerializeField] GameObject touch_marker; // жостик, который передвигаем
    Vector3 target_vector; // вектор между нажатием и центром неподвижного круга жостика
    Vector3 touch_pos; // позиция, куда нажали
    
    Vector3 extremeVector; // самый крайний вектор, куда можно подвинуть жостик
    
    ObjectMovement playerMovement; // объект для вызова передвижения игрока
    private RectTransform rectTransform;
    private float radius; // радиус внешнего круга

    bool isActive = true; // не будет давать передвинуть жостик при паузе игры
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<ObjectMovement>();
        rectTransform = GetComponent<RectTransform>();
        radius = rectTransform.rect.width / 2;

        // при старте выставим внутренный круг на позицию внешнего
        touch_marker.transform.position = transform.position;

        // подписались на события onChangeGameMode игрового менеджера GameManager  
        GameManager.onChangeGameMode += SetActive;
    }

    void Update()
    {
        // если нажали по полю и игра не была на паузе
        //if(Input.touchCount > 0)
        if(Input.GetMouseButton(0) && isActive)
        {
            // получаем координаты нажатия
            touch_pos = Input.mousePosition;
            //touch_pos = Input.GetTouch(0).position; 

            // составляем вектор движения от точки жостика до точки нажатия по экрану
            target_vector = touch_pos - transform.position;

            // если палец внутри неподвижного круга жостика
            if (target_vector.magnitude < radius)
            {
                // то передвигаем внутренний круг на позицию нажатия
                touch_marker.transform.position = touch_pos;
            }   
            else
            {
                // нормаль вектора умножаем на радиус внешнего круга жостика
                extremeVector = target_vector / target_vector.magnitude * radius;  
                // на край внешнего круга ставится внутренний круг
                touch_marker.transform.position = transform.position + extremeVector;
            }        
            // передвигаем игрока в определённом направлении с помощью нормали вектора, 
            // который определяет направление, но не влияет на скорость
            playerMovement.Move(target_vector.normalized);
        }
        else
        {
            // возвращаем на прежнюю позицию, если отпустили кнопку
            touch_marker.transform.position = transform.position;
        }
    }

    // метод SetActive отпределяет, была ли нажата пауза
    private void SetActive(bool isPlaying)
    {
        isActive = isPlaying;
    }
}
