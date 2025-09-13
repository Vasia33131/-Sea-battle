using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InputMouse : MonoBehaviour
{
    public Tilemap shipTilemap; //���� ��� ��� ��������
    public TileBase shipTile; //���� �������
    

    public Vector2Int leftEdge; //���������� ��� ������ ��� ���, ������� ������ ����� ������ ���� ����� � ��� ����������� ����� ����� ������
    public Vector2Int rightEdge; //����� ����������� ������ ����� ����������, �� ��� ����� ���� �� ������ � ������ ���� ������ ������ ������� ������� � ���� ���� �� �����
                                 //public Transform leftEdje; �������� ���

    private int _selectedShipSize = 1; //������ ���������� ������� (1-5)
    private bool _isHorizontal = true; //���������� ������� (��������������/������������)
    private bool _isPlacingShip = false; 
    

    private Dictionary<int, int> availableShips = new Dictionary<int, int>()//������ ��������� ��� ��������
    {
        {1, 5}, //5 �������� �������� 1
        {2, 4}, //4 ������� �������� 2
        {3, 3}, //3 ������� �������� 3
        {4, 2}, //2 ������� �������� 4
        {5, 1}  //1 ������� �������� 5
    };

    void Update()//����� ������ ������� ������ ��� �� � ��� ������� � ������ (�� ���� ����� ��� ������� ����� � ������ ����� ��������� ����� ������ � ������ �������� ��������) (������ ���� �� ��� ����������� ������ ����)
    {
        Shipsize(); //��������� ������� �������
        Shiporientation(); //��������� ��������� ����������
        Shipplacement(); //��������� ���������� ��������
    }

    // ��������� ����� ��� ������ ������� �������
    private void Shipsize()
    {
        for (int i = 1; i <= 5; i++)
        {
            if (Input.GetKeyDown(i.ToString())) //���� ������ ������� 1-5
            {
                if (availableShips[i] > 0) //���������, ���� �� ������� ������ ����
                {
                    _selectedShipSize = i; //������������� ��������� ������
                    _isPlacingShip = true; //�������� ������� ����������
                    
                }
               
            }
        }
    }

    // ���������  ���������� �������
    private void Shiporientation()
    {
        if (Input.GetKeyDown(KeyCode.R)) //���� ������ ������� R
        {
            _isHorizontal = !_isHorizontal; //������ ����������
            
        }
    }

    // ��������� ���������� ��������
    private void Shipplacement()
    {
        if (!_isPlacingShip) return; //���� �� ������� ���������� - �������

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = shipTilemap.WorldToCell(mouseWorldPos);

        // ��������������� �������� ���������� ������� (����� �������� ������������)

        if (Input.GetMouseButtonDown(0)) //����� ������ ���� - ����������
        {
            if (CanPlaceShip(cellPosition, _selectedShipSize, _isHorizontal))
            {
                PlaceShip(cellPosition, _selectedShipSize, _isHorizontal);
                availableShips[_selectedShipSize]--; // ��������� ���������� ��������� ��������
                _isPlacingShip = false; // ����������� ������� ����������
            }
            
        }

        if (Input.GetMouseButtonDown(1)) //������ ������ ���� - ������
        {
            _isPlacingShip = false;
           
        }
    }

    //�������� ����������� ���������� �������
    private bool CanPlaceShip(Vector3Int startCell, int size, bool isHorizontal)
    {
        for (int i = 0; i < size; i++)
        {
            Vector3Int currentCell = isHorizontal ?
                new Vector3Int(startCell.x + i, startCell.y, startCell.z) :
                new Vector3Int(startCell.x, startCell.y + i, startCell.z);

            //��������� ������� � ���������� ������ ��������
            if (!IsWithinBounds(currentCell) || shipTilemap.GetTile(currentCell) != null)
            {
                return false;
            }
        }
        return true;
    }

    //���������� ������� �� ����
    private void PlaceShip(Vector3Int startCell, int size, bool isHorizontal)
    {
        for (int i = 0; i < size; i++)
        {
            Vector3Int currentCell = isHorizontal ?
                new Vector3Int(startCell.x + i, startCell.y, startCell.z) :
                new Vector3Int(startCell.x, startCell.y + i, startCell.z);

            shipTilemap.SetTile(currentCell, shipTile); //������������� ���� �������
        }

    }
    private bool IsWithinBounds(Vector3Int cellPosition)
    {
        //�������� �� ��������
        return cellPosition.x >= leftEdge.x && //�����
            cellPosition.y <= rightEdge.x && //������
            cellPosition.y >= leftEdge.y && //���
            cellPosition.y <= rightEdge.y;//����
    }
}
