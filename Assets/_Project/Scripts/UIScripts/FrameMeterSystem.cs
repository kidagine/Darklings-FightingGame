using Demonics;
using UnityEngine;

public class FrameMeterSystem : MonoBehaviour
{
    [SerializeField] private FrameMeter _framedataMeterOne = default;
    [SerializeField] private FrameMeter _framedataMeterTwo = default;
    private FramedataTypesEnum _framedataOne;
    private FramedataTypesEnum _framedataTwo;
    private int _cycles;
    public int Index { get; private set; }

    public void AddFrame(int playerOne, FramedataTypesEnum framedataEnum)
    {
        if (playerOne == 0)
            _framedataOne = framedataEnum;
        else
            _framedataTwo = framedataEnum;
    }

    public void RunFrame()
    {

        if (_framedataMeterOne.WasPreviousNone && _framedataMeterTwo.WasPreviousNone)
        {
            if (_framedataOne != FramedataTypesEnum.None || _framedataTwo != FramedataTypesEnum.None)
            {
                _framedataMeterOne.ClearFrames();
                _framedataMeterTwo.ClearFrames();
                Index = 0;
                _cycles = 0;
            }
        }
        if (_framedataOne == FramedataTypesEnum.None && _framedataTwo == FramedataTypesEnum.None)
        {
            int actionFrameOne = _framedataMeterOne.ActionFrame;
            int actionFrameTwo = _framedataMeterTwo.ActionFrame;
            _framedataMeterOne.SetRecovery(actionFrameTwo - actionFrameOne);
            _framedataMeterTwo.SetRecovery(actionFrameOne - actionFrameTwo);
        }

        _framedataMeterOne.AddFrame(_framedataOne, Index, _cycles);
        _framedataMeterTwo.AddFrame(_framedataTwo, Index, _cycles);
        if (_framedataOne != FramedataTypesEnum.None || _framedataTwo != FramedataTypesEnum.None)
            Index++;
        if (Index >= 60)
        {
            _cycles++;
            Index = 0;
            _framedataMeterOne.FadeFrames();
            _framedataMeterTwo.FadeFrames();
        }
    }
}
