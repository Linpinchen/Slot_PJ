using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDate
{
    List<int> Temp { get; set; }
    int[] PrizeDate { get; }
    int CurrentReel { get; set; }
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
    List<intCount> Date { get; set; }
    Slot_SeveDate Slot_SeverDate { get; set; }


    void Initialization_Slot_Sprite();
    void Generate_Date_Sprite(SlotGrid slotGrid);
    void GenerateBonusDate(SlotGrid slotGrid);
    void Add_BonusDate(bool BtnTrue, SlotGrid _SlotGrid);
    void DateTypeChange(SlotGrid _SlotGrid);
    int Win_Sprite(Pool_Images _poolimage, int Line_Count, int WinMoneys);
    void DateSave(SlotGrid CommonGrid, SlotGrid BonusGrid, int FreeGamecount);
    int WInChack(GridIntS _Gridints);
    void CheckBetCoin();



}
