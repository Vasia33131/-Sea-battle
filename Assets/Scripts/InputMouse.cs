using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InputMouse : MonoBehaviour
{
    public Tilemap shipTilemap; //Тайл мап для кораблей
    public TileBase shipTile; //Тайл корабля
    

    public Vector2Int leftEdge; //Используем инт вектор так как, обычный вектор юзает данные типа флоат и при вычислениях могут выйти ошибки
    public Vector2Int rightEdge; //Решил попробовать именно через координаты, но так можно было бы просто в паблик поля сунуть пустые обьекты которые у меня есть на сцене
                                 //public Transform leftEdje; например так

    private int _selectedShipSize = 1; //Размер выбранного корабля (1-5)
    private bool _isHorizontal = true; //Ориентация корабля (горизонтальная/вертикальная)
    private bool _isPlacingShip = false; 
    

    private Dictionary<int, int> availableShips = new Dictionary<int, int>()//просто хранилище для кораблей
    {
        {1, 5}, //5 кораблей размером 1
        {2, 4}, //4 корабля размером 2
        {3, 3}, //3 корабля размером 3
        {4, 2}, //2 корабля размером 4
        {5, 1}  //1 корабль размером 5
    };

    void Update()//когда только начинал писать код то я все помещал в апдейт (но кода стало уже слишком много и потому начал создавать новые методы и просто внолсить название) (апдейт жэто то что происхождит каждый кадр)
    {
        Shipsize(); //Обработка размера корабля
        Shiporientation(); //Обработка изменения ориентации
        Shipplacement(); //Обработка размещения кораблей
    }

    // Обработка ввода для выбора размера корабля
    private void Shipsize()
    {
        for (int i = 1; i <= 5; i++)
        {
            if (Input.GetKeyDown(i.ToString())) //Если нажата клавиша 1-5
            {
                if (availableShips[i] > 0) //Проверяем, есть ли корабли такого типа
                {
                    _selectedShipSize = i; //Устанавливаем выбранный размер
                    _isPlacingShip = true; //Начинаем процесс размещения
                    
                }
               
            }
        }
    }

    // Обработка  ориентации корабля
    private void Shiporientation()
    {
        if (Input.GetKeyDown(KeyCode.R)) //Если нажата клавиша R
        {
            _isHorizontal = !_isHorizontal; //Меняем ориентацию
            
        }
    }

    // Обработка размещения кораблей
    private void Shipplacement()
    {
        if (!_isPlacingShip) return; //Если не выбрано размещение - выходим

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = shipTilemap.WorldToCell(mouseWorldPos);

        // Предварительный просмотр размещения корабля (можно добавить визуализацию)

        if (Input.GetMouseButtonDown(0)) //Левая кнопка мыши - разместить
        {
            if (CanPlaceShip(cellPosition, _selectedShipSize, _isHorizontal))
            {
                PlaceShip(cellPosition, _selectedShipSize, _isHorizontal);
                availableShips[_selectedShipSize]--; // Уменьшаем количество доступных кораблей
                _isPlacingShip = false; // Заканчиваем процесс размещения
            }
            
        }

        if (Input.GetMouseButtonDown(1)) //Правая кнопка мыши - отмена
        {
            _isPlacingShip = false;
           
        }
    }

    //Проверка возможности размещения корабля
    private bool CanPlaceShip(Vector3Int startCell, int size, bool isHorizontal)
    {
        for (int i = 0; i < size; i++)
        {
            Vector3Int currentCell = isHorizontal ?
                new Vector3Int(startCell.x + i, startCell.y, startCell.z) :
                new Vector3Int(startCell.x, startCell.y + i, startCell.z);

            //Проверяем границы и отсутствие других кораблей
            if (!IsWithinBounds(currentCell) || shipTilemap.GetTile(currentCell) != null)
            {
                return false;
            }
        }
        return true;
    }

    //Размещение корабля на поле
    private void PlaceShip(Vector3Int startCell, int size, bool isHorizontal)
    {
        for (int i = 0; i < size; i++)
        {
            Vector3Int currentCell = isHorizontal ?
                new Vector3Int(startCell.x + i, startCell.y, startCell.z) :
                new Vector3Int(startCell.x, startCell.y + i, startCell.z);

            shipTilemap.SetTile(currentCell, shipTile); //Устанавливаем тайл корабля
        }

    }
    private bool IsWithinBounds(Vector3Int cellPosition)
    {
        //проверка по границам
        return cellPosition.x >= leftEdge.x && //левая
            cellPosition.y <= rightEdge.x && //правая
            cellPosition.y >= leftEdge.y && //низ
            cellPosition.y <= rightEdge.y;//верх
    }
}
