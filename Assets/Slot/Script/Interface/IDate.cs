﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDate
{
    int PlayerCoin { get; set; }
    int Bet_Coin { get; set; }
    int Win_Coin { get; set; }
    int LeastBetCount { get; set; }
    List<int> BonusWinCoin { get; set; }
    int Total_BonusWinCoin { get; set; }
    int AutoCount { get; set; }
    int CycleCount { get; set; }
    int AutoSurplus { get; set; }
    int BonusCount { get; set; }
    Sprite[] SpritePool { get; set; }

}