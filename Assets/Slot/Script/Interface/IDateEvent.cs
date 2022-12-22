using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDateEvent {

    void Initialization_Slot_Sprite(Reel_Move[] reel_Moves);
    void Generate_Date_Sprite(SlotGrid slotGrid);
    void GenerateBonusDate(SlotGrid slotGrid);
    void Add_BonusDate(bool BtnTrue, SlotGrid _SlotGrid);
    void DateTypeChange(SlotGrid _SlotGrid);
    void Win_Sprite(Pool_Images _poolimage, int Line_Count, ref int WinMoneys);
    void Win_Line(GridIntS _Gridints, List<Transform> ReadyShow, ref int Win_Money, Transform _StayOutPool, List<GameObject> _StayDrawLine);
    void DateSave(SlotGrid CommonGrid, SlotGrid BonusGrid);
}
