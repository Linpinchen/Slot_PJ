using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public interface IUIControlMethod

{
    bool StAddBonusDate { get; set; }
    

    void AddBonus();

    void BetMenuSwitch();

    void BetPlus();

    void BetReduce();

    void Bet_MaxCoin();

    void AutoMenuSwitch();

    void AutoPlus();

    void AutoReduce();

    void Auto_Clear();

    void Auto_pause();

    void OpenINFO();

    void OutInfo();

    void InfoRight();

    void InfoLeft();

    void GetDateSave();

    void Button_PressAndHold();

    void Button_Reduce_Press();
}
