using Demonics;
using UnityEngine;

public class FrameMeterSystem : MonoBehaviour
{
    [SerializeField] private FrameMeter _framedataMeterOne = default;
    [SerializeField] private FrameMeter _framedataMeterTwo = default;
    FramedataTypesEnum _framedataOne;
    FramedataTypesEnum _framedataTwo;
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
        if (_framedataOne != FramedataTypesEnum.None || _framedataTwo != FramedataTypesEnum.None)
            if (_framedataMeterOne.WasPreviousNone && _framedataMeterTwo.WasPreviousNone)
            {
                _framedataMeterOne.ClearFrames();
                _framedataMeterTwo.ClearFrames();
                Index = 0;
            }

        _framedataMeterOne.AddFrame(_framedataOne, Index);
        _framedataMeterTwo.AddFrame(_framedataTwo, Index);
        if (_framedataOne != FramedataTypesEnum.None || _framedataTwo != FramedataTypesEnum.None)
            Index++;
        if (Index >= 60)
        {
            Index = 0;
            _framedataMeterOne.FadeFrames();
            _framedataMeterTwo.FadeFrames();
        }
    }
}
