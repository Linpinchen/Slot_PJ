using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IMove
{

    bool strool { get; set; }
    Vector2 originalv2 { get; set; }
    RectTransform ReelV2 { get; set; }
    List<Image> Reel_images { get; set; }
    int tempi { get; set; }
    float Speed { get; set; }
    int Date_Temp { get; set; }
    int Roolcount { get; set; }
    Sprite[] Sprites { get; set; }
    List<int> ChangeSprite { get; set; }
    GameObject Self { get; set; }

    void Move();

}
