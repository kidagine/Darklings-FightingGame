using System.Collections.Generic;
using System.Linq;
using Demonics;
using TMPro;
using UnityEngine;

public class FrameMeter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _framedataText = default;
    List<FrameMeterSquare> _frameMeterSquares = new List<FrameMeterSquare>();
    private bool _wasPreviousNone = true;
    private int _index;
    private int _indexPrevious;
    private int _frame = 1;
    private int _startUp;
    private int _active;
    private int _recovery;
    private FramedataTypesEnum _framedataEnum = FramedataTypesEnum.StartUp;

    void Awake()
    {
        foreach (Transform child in transform)
            _frameMeterSquares.Add(child.GetComponent<FrameMeterSquare>());
    }

    public void AddFrame(FramedataTypesEnum framedataEnum)
    {
        if (framedataEnum == FramedataTypesEnum.None)
        {
            if (!_wasPreviousNone)
                SetFramedataNumber(true);
            _wasPreviousNone = true;
        }
        else
        {
            if (_wasPreviousNone)
                ClearFrames();
            if (_index > _frameMeterSquares.Count - 1)
                FadeFrames();
            _frameMeterSquares[_index].SetFrame(framedataEnum);
            CheckFramedata(framedataEnum);
            _indexPrevious = _index;
            _index++;
        }
    }

    private void ClearFrames()
    {
        _index = 0;
        _frame = 0;
        _wasPreviousNone = false;
        for (int i = 0; i < _frameMeterSquares.Count; i++)
            _frameMeterSquares[i].SetFrame(FramedataTypesEnum.None);
    }

    private void FadeFrames()
    {
        _index = 0;
        for (int i = 0; i < _frameMeterSquares.Count; i++)
            _frameMeterSquares[i].FadeFrame();
    }

    private void CheckFramedata(FramedataTypesEnum framedataEnum)
    {
        if (_framedataEnum != framedataEnum)
        {
            switch (_framedataEnum)
            {
                case FramedataTypesEnum.StartUp:
                    _startUp = _frame;
                    break;
                case FramedataTypesEnum.Active:
                    _active = _frame;
                    break;
                case FramedataTypesEnum.Recovery:
                    _recovery = _frame;
                    break;
            }
            SetFramedataNumber();
            _frame = 1;
            _framedataEnum = framedataEnum;
            _framedataText.text = $"StartUp {_startUp} | Active {_active} | Recovery {_recovery}";
        }
        else
            _frame++;
    }

    private void SetFramedataNumber(bool setFrameForward = false)
    {
        int _lastMeterSquare = _index - 1;
        if (_lastMeterSquare >= 0)
        {
            int[] frameDigits = _frame.ToString().Select(digit => int.Parse(digit.ToString())).ToArray();
            if (frameDigits.Length == 1 || _index > _frameMeterSquares.Count - 1)
                setFrameForward = false;
            for (int i = 0; i < frameDigits.Length; i++)
            {
                int meterSquareIndex = 0;
                if (setFrameForward)
                {
                    meterSquareIndex = _lastMeterSquare - ((frameDigits.Length - 2) - i);
                    _frameMeterSquares[_lastMeterSquare + 1].SetFrame(_framedataEnum, true);
                }
                else
                    meterSquareIndex = _lastMeterSquare - ((frameDigits.Length - 1) - i);
                _frameMeterSquares[meterSquareIndex].DisplayFrameNumber(frameDigits[i]);
            }
        }
    }
}
