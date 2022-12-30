using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reel_Move : MonoBehaviour {
   
    public bool strool;//是否要滾動
    public Vector2 originalv2;//原始座標
    public RectTransform ReelV2;//移動用的座標
    public List<Image> Reel_images; //掛著這個腳本的物件 的 子物件Image
    public int tempi;//計算換圖次數
    public float Speed;
    public int Date_Temp;
    public int Roolcount;
    public Sprite[] Sprites;
    public List<int> ChangeSprite;


 
    void Start() {
        
        Date_Temp = 0;
        originalv2 = gameObject.GetComponent<RectTransform>().anchoredPosition;//取得原始座標
        //Debug.Log("originalv2" + originalv2);
        ReelV2 = gameObject.GetComponent<RectTransform>();//移動用的座標
        Reel_images = new List<Image>();//取得掛著這個腳本的物件 的 子物件Image

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Reel_images.Add(gameObject.transform.GetChild(i).GetComponent<Image>());

        }

    }

   
    void Update() {

        if (tempi < Roolcount && strool == true)
        {
            int Date_Chang_Count = Roolcount - gameObject.transform.childCount;//換圖次數-子物件數＝隨機換圖次數 ,（因為最後要留單輪條子物件數來灌入Date的盤面資料）
            ReelV2.anchoredPosition += new Vector2(0, -5) * Speed * Time.deltaTime;

            if (ReelV2.anchoredPosition.y <= -170)
            {

                ReelV2.anchoredPosition = originalv2;//回歸初始座標
                int ri;
                ri = Random.Range(0, Sprites.Length);//換圖用隨機值

                for (int i = 0; i < gameObject.transform.childCount; i++)
                {

                    if (i < gameObject.transform.childCount - 1)//這裡做前四張圖 換成各自的上一個位置的圖片
                    {

                        Reel_images[i].sprite = Reel_images[i + 1].sprite;//n的圖片換成n+1的

                    }

                    else//最後一張圖要隨機給圖 如果到需要灌入Date資料的次數時
                    {
                        if (tempi >= Date_Chang_Count)
                        {

                            Reel_images[i].sprite = Sprites[ChangeSprite[Date_Temp]];
                            tempi++;
                            Date_Temp++;
                            //Debug.Log("tempi:" + tempi + "Roolcount" + Roolcount + "Sprites :" + ChangeSprite[i]);

                            if (tempi==Roolcount)//這裡做重置的動作
                            {
                                strool = false;
                                //tempi = 0;
                                Date_Temp = 0;
                            }

                        }
                        else
                        {
                            Reel_images[i].sprite = Sprites[ri];
                            tempi++;
                            // Debug.Log("tempi:" + tempi + "Roolcount" + Roolcount);

                        }


                    }

                }

            }

        }

    }

    public void Init(int Roolcount, Sprite[] sprites, float Speed)
    {

        this.Roolcount = Roolcount;
        this.Sprites = sprites;
        this.Speed = Speed;
        ChangeSprite = new List<int>();

        for (int i=0;i<this.transform.childCount;i++)
        {

            ChangeSprite.Add(0);

        }

    }
 
}
