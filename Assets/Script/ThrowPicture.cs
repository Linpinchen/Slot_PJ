using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPicture : MonoBehaviour
{
    //控制是否要丟圖片給輪條的腳本

    // 將輪條內 產生隨機圖的方法抽離

    //讓輪條 有資料就滾動 沒資料就停止


    // 思路 ： Manager 呼叫這個腳本的 丟圖片方法 （丟給每個輪條的資料會不相同） 例如：輪條 1 : 10張圖 , 輪條 2 : 20張圖 , 輪條 3 : 30張圖

    // 藉由 各輪條的圖片數量差 來做出依序停輪的感覺

    //-----------------------------------------------------------------------------------

    //輪條腳本 有一個List 輪條在 Update 判斷List的長度 是否為0 ,0 就停止轉動 不為 0 就 持續滾動 換圖


    // 我應該在哪個環節給資料？？   又需要在哪個環節斷斷是否需要持續給資料 ？？
    // 我該如何判斷 滾輪全都停止了 ？？



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
