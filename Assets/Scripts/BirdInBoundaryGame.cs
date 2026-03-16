using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdInBoundaryGame : MonoBehaviour
{
    private string _looseMessage = "Вы проиграли! :(";
    private string _winMessage = "Вы победили! :)";

    [SerializeField] private Bird _bird;

    [SerializeField] private Transform _upperBoundary;
    [SerializeField] private Transform _lowerBoundary;
    [SerializeField] private Transform _leftBoundary;
    [SerializeField] private Transform _rightBoundary;

    [SerializeField] private float _upperYLimit;
    [SerializeField] private float _lowerYLimit;
    [SerializeField] private float _leftXLimit;
    [SerializeField] private float _rightXLimit;

    [SerializeField] private int _pointsToWin;

    private bool _isRunning;

    private void Awake()
    {
        StartGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            StartGame();

        if (_isRunning == false)
            return;

        if (IsOufOfBoundary())
        {
            LooseGame();
        }

        if (IsGoalCompleted())
        {
            WonGame();
        }
    }

    private bool IsOufOfBoundary()
        => _bird.transform.position.y > _upperYLimit 
        || _bird.transform.position.y < _lowerYLimit
        || _bird.transform.position.x > _rightXLimit
        || _bird.transform.position.x < _leftXLimit;

    private bool IsGoalCompleted()
        => _bird.Points >= _pointsToWin;

    private void StartGame()
    {
        _bird.gameObject.SetActive(true);
        _bird.transform.position = Vector3.zero;
        _bird.GetComponent<Rigidbody>().isKinematic = false;
        _bird.ResetPoints();

        _upperBoundary.position = new Vector3(0, _upperYLimit, 0);
        _lowerBoundary.position = new Vector3(0, _lowerYLimit, 0);
        _rightBoundary.position = new Vector3(_rightXLimit, 0, 0);
        _leftBoundary.position = new Vector3(_leftXLimit, 0, 0);

        _isRunning = true;
    }

    private void LooseGame()
    {
        _bird.gameObject.SetActive(false); // выключаем объект птички
        Debug.Log(_looseMessage);
        Debug.Log($"Ваш счет: {_bird.Points}");
        _isRunning = false;
    }

    private void WonGame()
    {
        _bird.GetComponent<Rigidbody>().isKinematic = true;
        Debug.Log(_winMessage);
        Debug.Log($"Накопили очков: {_pointsToWin}");
        _isRunning = false;
    }
}