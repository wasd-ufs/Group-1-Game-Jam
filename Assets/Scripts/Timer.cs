using UnityEngine;
using System;
public class Timer : MonoBehaviour
{
    private float _duration;        // Duração total do timer
    private float _timeRemaining;   // Tempo restante
    private bool _isRunning;        // Se está rodando

    public Action OnTimerEnd;       // Evento chamado ao finalizar o tempo

    public Timer(float duration)
    {
        _duration = duration;
        _timeRemaining = duration;
        _isRunning = false;
    }

    public void StartTimer()
    {
        _timeRemaining = _duration;
        _isRunning = true;
    }

    public void StopTimer()
    {
        _isRunning = false;
    }

    public void UpdateTimer()
    {
        if (!_isRunning) return;

        _timeRemaining -= Time.deltaTime;
        if (_timeRemaining <= 0f)
        {
            _timeRemaining = 0f;
            _isRunning = false;
            OnTimerEnd?.Invoke(); // Dispara evento quando termina
        }
    }

    public void SetTimeRemaining(float timeRemaining)
    {
        _timeRemaining = timeRemaining;
        _duration = timeRemaining;
    }
    public float GetTimeRemaining()
    {
        return _timeRemaining;
    }

    public bool IsRunning()
    {
        return _isRunning;
    }
}
