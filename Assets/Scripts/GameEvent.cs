using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameEvents
{
    // ������� ��� ���������� �������
    public static event Action<Vector3Int, int, bool> OnShipPlaced;
    public static void InvokeShipPlaced(Vector3Int position, int size, bool isHorizontal) =>
        OnShipPlaced?.Invoke(position, size, isHorizontal);

    // ������� ��� ��������� ����������
    public static event Action<bool> OnOrientationChanged;
    public static void InvokeOrientationChanged(bool isHorizontal) =>
        OnOrientationChanged?.Invoke(isHorizontal);

    // ������� ��� ������ ����������
    public static event Action OnPlacementCancelled;
    public static void InvokePlacementCancelled() => OnPlacementCancelled?.Invoke();
}
