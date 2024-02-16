using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterSet
{
    void SetCharacterImage(PlayerStatsSO playerStats, bool isRandomizer);
    void SelectCharacterImage();
}
