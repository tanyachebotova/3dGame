using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 100f;  // Скорость поворота персонажа
    public float moveSpeed = 5f;        // Скорость движения персонажа

    private Transform player;
    private float currentRotationY = 0f; // Вертикальный угол поворота камеры
    private float horizontalRotation = 0f;  // Горизонтальный угол поворота игрока

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        // Движение персонажа
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;  // Стрелки влево-вправ (A, D)
        float vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;      // Стрелки вверх-вниз (W, S)

        player.Translate(horizontal, 0, vertical);  // Двигаем персонажа

        // Поворот персонажа по горизонтали с использованием стрелок влево/вправ (A, D)
        float rotate = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        horizontalRotation += rotate;  // Обновляем горизонтальный угол поворота

        // Ограничиваем вертикальный поворот (чтобы камера не смотрела вниз или вверх слишком сильно)
        currentRotationY = Mathf.Clamp(currentRotationY, -40f, 80f);

        // Поворачиваем персонажа по оси Y
        player.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);

        // Камера следит за позицию персонажа (глаза персонажа)
        transform.position = player.position + new Vector3(0f, 1.6f, 0f);  // Камера будет чуть выше уровня персонажа

        // Поворот камеры зависит от поворота игрока по оси Y (глаза персонажа)
        transform.rotation = Quaternion.Euler(currentRotationY, player.rotation.eulerAngles.y, 0f);
    }
}
