﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public interface IShow

{
    int TempWinCoin { get; set; }
    bool AddCoin { get; set; }
    bool CoinShow_Bool { get; set; }
    List<List<bool>> BonusDrawLineOk { get; set; }
    List<bool> DrawLineOK { get; set; }
    Queue<GameObject> DrawLinePool { get; set; }
    List<List<GameObject>> BonusPrepareDrawline { get; set; }
    List<GameObject> PrepareDrawLine { get; set; }
    List<Transform> UiShow { get; set; }
    List<List<Transform>> BonusUiShow { get; set; }
   




    void ObjectPoolInitialization();
    void ObjectPool(int Number, Transform StayOutpool, List<GameObject> StayDrawLine);
    void Recover(GameObject recover, bool Stb);
    void InstObjectInitialization(GameObject Reuse, RectTransform OrangePoint, List<RectTransform> PointPosition, RectTransform EndPoint);
    void RecoverComply(Transform OutPool, List<GameObject> StayDrawLine, List<bool> _SHowOk);
    List<RectTransform> GetRoolWinImg(int SlotSequence);
    IEnumerator StartDrawLine(List<GameObject> StayDrawLine, List<bool> _ShowOk);
    void ListShiny(int Number, List<Transform> ReadyShow);
    IEnumerator ShinyShow(List<Transform> ReadyShow);
    IEnumerator CoinShow(int Coin);


}
