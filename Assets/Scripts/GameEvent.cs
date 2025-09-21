using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameEvents
{
    // Событие для размещения корабля
    public static event Action<Vector3Int, int, bool> OnShipPlaced;
    public static void InvokeShipPlaced(Vector3Int position, int size, bool isHorizontal) =>
        OnShipPlaced?.Invoke(position, size, isHorizontal);

    // Событие для изменения ориентации
    public static event Action<bool> OnOrientationChanged;
    public static void InvokeOrientationChanged(bool isHorizontal) =>
        OnOrientationChanged?.Invoke(isHorizontal);

    // Событие для отмены размещения
    public static event Action OnPlacementCancelled;
    public static void InvokePlacementCancelled() => OnPlacementCancelled?.Invoke();
}
