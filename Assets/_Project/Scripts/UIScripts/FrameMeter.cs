using System.Collections.Generic;
using System.Linq;
using Demonics;
using TMPro;
using UnityEngine;

public class FrameMeter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _startUpText = default;
    [SerializeField] private TextMeshProUGUI _totalText = default;
    [SerializeField] private TextMeshProUGUI _recoveryText = default;
    List<FrameMeterSquare> _frameMeterSquares = new List<FrameMeterSquare>();
    public bool WasPreviousNone { get; set; }
    private int _indexPrevious;
    private int _frame;
    private int _total;
    private FramedataTypesEnum _framedataEnum = FramedataTypesEnum.StartUp;

    void Awake()
    {
        foreach (Transform child in transform)
            _frameMeterSquares.Add(child.GetComponent<FrameMeterSquare>());
    }

    public void AddFrame(FramedataTypesEnum framedataEnum, int index)
    {
        if (framedataEnum == FramedataTypesEnum.None)
        {
            if (!WasPreviousNone)
            {
                _totalText.text = $"Total {_total}F";
                if (_frame > 1)
                {
                    SetFramedataNumber(index, true);
                    _frame = 1;
                }
            }
            WasPreviousNone = true;
            _frameMeterSquares[index].SetFrame(framedataEnum);
        }
        else
        {
            WasPreviousNone = false;
            _frameMeterSquares[index].SetFrame(framedataEnum);
            CheckFramedata(framedataEnum, index);
            _indexPrevious = index;
            _total++;
        }
    }

    public void ClearFrames()
    {
        _total = 0;
        _frame = 1;
        WasPreviousNone = false;
        _startUpText.text = $"StartUp --";
        _totalText.text = $"Total --";
        _recoveryText.text = $"Recovery --";
        for (int i = 0; i < _frameMeterSquares.Count; i++)
            _frameMeterSquares[i].SetFrame(FramedataTypesEnum.Empty);
    }

    public void FadeFrames()
    {
        for (int i = 0; i < _frameMeterSquares.Count; i++)
            _frameMeterSquares[i].FadeFrame();
    }

    private void CheckFramedata(FramedataTypesEnum framedataEnum, int index)
    {
        if (_framedataEnum != framedataEnum)
        {
            switch (_framedataEnum)
            {
                case FramedataTypesEnum.StartUp:
                    _startUpText.text = $"StartUp {_frame}F";
                    break;
                case FramedataTypesEnum.Active:
                    _recoveryText.text = $"Recovery {_frame}F";
                    break;
            }
            SetFramedataNumber(index);
            _frame = 1;
            _framedataEnum = framedataEnum;
        }
        else
            _frame++;
    }

    private void SetFramedataNumber(int index, bool setFrameForward = false)
    {

        int _lastMeterSquare = index - 1;
        if (_lastMeterSquare >= 0)
        {
            int[] frameDigits = _frame.ToString().Select(digit => int.Parse(digit.ToString())).ToArray();
            if (frameDigits.Length == 1 || index > _frameMeterSquares.Count - 1)
                setFrameForward = false;

            for (int i = 0; i < frameDigits.Length; i++)
            {
                int meterSquareIndex = 0;
                meterSquareIndex = _lastMeterSquare - ((frameDigits.Length - 1) - i);
                if (setFrameForward)
                    _frameMeterSquares[_lastMeterSquare].SetFrame(_framedataEnum, true);
                if (_frame > 3 & meterSquareIndex > 1)
                    _frameMeterSquares[meterSquareIndex].DisplayFrameNumber(frameDigits[i]);
            }
        }
    }
}
