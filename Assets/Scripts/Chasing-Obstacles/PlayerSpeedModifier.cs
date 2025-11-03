using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerSpeedModifier : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private float _baseSpeed;
    private Dictionary<int, float> _activeMultipliers = new Dictionary<int, float>();

    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        if (_playerMovement == null)
        {
            Debug.LogError("PlayerSpeedModifier requires a PlayerMovement on the same GameObject.");
            enabled = false;
            return;
        }

        _baseSpeed = _playerMovement.speed;
    }

    public void AddOrUpdateMultiplier(int obstacleInstanceId, float multiplier)
    {
        if (_playerMovement == null) return;

        _activeMultipliers[obstacleInstanceId] = Mathf.Clamp(multiplier, 0.01f, 10f);
        ApplyBestMultiplier();
    }

    public void RemoveMultiplier(int obstacleInstanceId)
    {
        if (_playerMovement == null) return;

        if (_activeMultipliers.Remove(obstacleInstanceId))
            ApplyBestMultiplier();
    }

    private void ApplyBestMultiplier()
    {
        if (_activeMultipliers.Count == 0)
        {
            _playerMovement.speed = _baseSpeed;
            return;
        }

        float best = float.MaxValue;
        foreach (var kv in _activeMultipliers)
            if (kv.Value < best) best = kv.Value;

        best = Mathf.Clamp(best, 0.01f, 1f);
        _playerMovement.speed = _baseSpeed * best;
    }

    public void UpdateBaseSpeed(float newBase)
    {
        _baseSpeed = newBase;
        ApplyBestMultiplier();
    }
}
