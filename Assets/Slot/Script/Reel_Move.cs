using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reel_Move : MonoBehaviour {
	public int[]  XD=new int [3];
	public bool strool;
	public Vector2 originalv2;
	public RectTransform ReelV2;
	public List<GameObject> Reel_images;
    public int tempi;//計算換圖次數
    public bool b;
    public int speed;
    public int Date_Temp;

    //待修改  Reel_images 應該要是 Image的 List
    //然後在 start 這裡 GetComponent 先抓好資料  不要在Update處理  


    // Use this for initialization
    void Start () {
        Date_Temp = 0;
		originalv2 = gameObject.GetComponent<RectTransform>().anchoredPosition;//取得原始座標
		//Debug.Log("originalv2" + originalv2);

		ReelV2= gameObject.GetComponent<RectTransform>();//移動用的座標

		Reel_images = new List<GameObject>();

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Reel_images.Add(gameObject.transform.GetChild(i).gameObject);


        }


    }
	
	// Update is called once per frame
	void Update () {



    }

    [System.Obsolete]
    public void Reel_Move_(float RoolSpeed, Sprite[] ChangeSprite, int Roolcount, List<Reel_Sprite_Date> Date_Sprite_Change,int Temp_i)//Temp_i 用來知道當前輪條是哪一條
    {


        if (tempi < Roolcount && b == true)
        {
            int Date_Chang_Count = Roolcount - gameObject.transform.childCount;//換圖次數-子物件數＝隨機換圖次數 ,（因為最後要留單輪條子物件數來灌入Date的盤面資料）

            ReelV2.anchoredPosition += new Vector2(0, -5) * RoolSpeed * Time.deltaTime;




            if (ReelV2.anchoredPosition.y <= -170)

            {
               

                ReelV2.anchoredPosition = originalv2;//回歸初始座標

                int ri;
                ri = Random.Range(0, ChangeSprite.Length);//換圖用隨機值

                for (int i = 0; i < gameObject.transform.childCount; i++)
                {

                    if (i < gameObject.transform.childCount - 1)//這裡做前四張圖 換成各自的上一個位置的圖片
                    {

                        Reel_images[i].GetComponent<Image>().sprite = Reel_images[i + 1].GetComponent<Image>().sprite;//n的圖片換成n+1的

                    }

                    else//最後一張圖要隨機給圖 如果到需要灌入Date資料的次數時
                    {
                        if (tempi >= Date_Chang_Count)
                        {

                            Reel_images[i].GetComponent<Image>().sprite = Date_Sprite_Change[Temp_i].Sever_Sprites[Date_Temp];
                            tempi++;
                            Date_Temp++;
                           // Debug.Log("tempi:" + tempi + "Roolcount" + Roolcount);


                        }
                        else
                        {
                            Reel_images[i].GetComponent<Image>().sprite = ChangeSprite[ri];
                            tempi++;
                           // Debug.Log("tempi:" + tempi + "Roolcount" + Roolcount);

                        }
                        

                    }

                }

            }



        }

    }


}
