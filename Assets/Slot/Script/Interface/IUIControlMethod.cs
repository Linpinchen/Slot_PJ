using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public interface IUIControlMethod

{
    bool StAddBonusDate { get; set; }
    GameObject Bet_Menu { get; }
    GameObject Auto_Menu { get; }
    GameObject Options { get; }
    Text Bet_Text { get; }
    Text BetMenu_Text { get; }
    Text Auto_text { get; }
    Sprite[] InFoSprites { get; }
    Image InfoBackSprite { get; }
    Image Img_Introduction { get; }


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

}
