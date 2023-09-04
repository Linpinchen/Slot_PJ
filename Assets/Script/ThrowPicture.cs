using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPicture 
{
    //控制是否要丟圖片給輪條的腳本

    // 將輪條內 產生隨機圖的方法抽離

    //讓輪條 有資料就滾動 沒資料就停止


    // 思路 ： Manager 呼叫這個腳本的 丟圖片方法 （丟給每個輪條的資料會不相同） 例如：輪條 1 : 10張圖 , 輪條 2 : 20張圖 , 輪條 3 : 30張圖

    // 藉由 各輪條的圖片數量差 來做出依序停輪的感覺

    //-----------------------------------------------------------------------------------

    //輪條腳本 有一個List 輪條在 Update 判斷List的長度 是否為0 ,0 就停止轉動 不為 0 就 持續滾動 換圖


    // 我應該在哪個環節給資料？？ （需要滾輪滾動時就給 例如開使按鈕）  又需要在哪個環節斷斷是否需要持續給資料 ？？（先不用做）（這個先寫 閃邊框的好了 有要閃邊框的輪條 多給滾動資料）
    // 我該如何判斷 滾輪全都停止了 ？？（用下面的方式）

    //滾輪開一個委派 Manager 給各個滾輪一個方法（方法有一個Int參數 , 那個參數是滾輪剩下幾張圖時要去執行判斷的方法）讓滾輪去判斷他需不需要再加滾動資料 如果需要再呼叫“給滾動圖片的腳本”給他們滾動圖片的資料
    

    //委派放 manamger的一個方法 那個方法 要寫 判斷Date Bonus圖 如果有兩個 那要多給圖的輪條是哪個 讓那幾個輪條加圖
    //而 MOveScript 在剩下 輪條數圖片時判斷 並調用委派內容 

    // Start is called before the first frame update







    //阿阿 這個不要給 滾輪的委派  由Manager 呼叫這個方法 輪條滾動圖片資料

    #region 給輪條滾動用的 隨機圖片資料
    /// <summary>
    /// 給輪條滾動用的 隨機圖片資料
    /// </summary>
    /// <param name="ReelLenght"></param>
    /// <param name="SpriteLenght"></param>
    /// <returns></returns>
    public void ThrowSpriteDate(int ReelLenght,int SpriteLenght,List<int> RoolSprite)
    {
        
        for (int i=0;i<ReelLenght+30;i++)
        {

            int Ri= Random.Range(0,SpriteLenght);
            RoolSprite.Add(Ri);


        }


        
    }
    #endregion







}
