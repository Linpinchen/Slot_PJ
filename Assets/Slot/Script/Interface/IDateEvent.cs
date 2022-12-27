using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDateEvent {

    void Initialization_Slot_Sprite();
    void Generate_Date_Sprite(SlotGrid slotGrid);
    void GenerateBonusDate(SlotGrid slotGrid);
    void Add_BonusDate(bool BtnTrue, SlotGrid _SlotGrid);
    void DateTypeChange(SlotGrid _SlotGrid);
    int Win_Sprite(Pool_Images _poolimage, int Line_Count,int WinMoneys);
    void DateSave(SlotGrid CommonGrid, SlotGrid BonusGrid);
    int WInChack(int[] PrizeDate, GridIntS _Gridints,int Win_Money);
}
