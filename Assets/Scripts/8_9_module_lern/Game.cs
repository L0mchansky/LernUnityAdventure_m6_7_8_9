using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    private string _looseMessage = "Вы проиграли! :(";
    private string _winMessage = "Вы победили! :)";

    [SerializeField] private Charter _charter;

    [SerializeField] private Transform _upperBoundary;
    [SerializeField] private Transform _lowerBoundary;
    [SerializeField] private Transform _leftBoundary;
    [SerializeField] private Transform _rightBoundary;

    [SerializeField] private float _upperYLimit;
    [SerializeField] private float _lowerYLimit;
    [SerializeField] private float _leftXLimit;
    [SerializeField] private float _rightXLimit;

    [SerializeField] private TMP_Text _pointText;

    [SerializeField] private GameObject _defeatPopup;

    [SerializeField] private int _pointsToWin;

    private string _shakeKey = "Die";
    [SerializeField] private Animator _cameraAnimator;

    private bool _isRunning;

    private void Awake()
    {
        StartGame();
        UpdatePointsText();
    }

    private void Update()
    {
        UpdatePointsText();

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

    private void UpdatePointsText()
    {
        _pointText.text = _charter.Points.ToString();
    }

    private bool IsOufOfBoundary()
        => _charter.transform.position.y > _upperYLimit
        || _charter.transform.position.y < _lowerYLimit
        || _charter.transform.position.x > _rightXLimit
        || _charter.transform.position.x < _leftXLimit;

    private bool IsGoalCompleted()
        => _charter.Points >= _pointsToWin;

    public void StartGame()
    {
        _defeatPopup.SetActive(false);
        _charter.gameObject.SetActive(true);
        _charter.transform.position = Vector3.zero;
        _charter.GetComponent<Rigidbody>().isKinematic = true;
        _charter.GetComponent<Rigidbody>().isKinematic = false;
        _charter.ResetPoints();

        _upperBoundary.position = new Vector3(0, _upperYLimit, 0);
        _lowerBoundary.position = new Vector3(0, _lowerYLimit, 0);
        _rightBoundary.position = new Vector3(_rightXLimit, 0, 0);
        _leftBoundary.position = new Vector3(_leftXLimit, 0, 0);

        _isRunning = true;
    }

    private void LooseGame()
    {
        _charter.Die();
        _cameraAnimator.SetTrigger(_shakeKey);
        _charter.GetComponent<Rigidbody>().isKinematic = true;
        Debug.Log(_looseMessage);
        Debug.Log($"Ваш счет: {_charter.Points}");
        _isRunning = false;
        _defeatPopup.SetActive(true);
    }

    private void WonGame()
    {
        _charter.GetComponent<Rigidbody>().isKinematic = true;
        Debug.Log(_winMessage);
        Debug.Log($"Накопили очков: {_pointsToWin}");
        _isRunning = false;
    }

}
