using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_EventTrigger : EventTrigger {

	//在這裏 長按按鈕 會＋＝100 將這裡得到的值傳給 Manager 讓Manager當中間人 把這裡傳出去的值跟ＤＡＴＥ 做溝通  （注意！ 這裡一直長按會一直傳值 但是Manager 那裡加值最大只會加到 玩家總金額的上限 遭過就算一直傳值也不加）

	public Animator Ami;
	public bool Button_Down;//判斷按鈕是否按下
	public float Down_Time;//紀錄按下按鈕時持續的時間
	public int Return_Valu;//用來記Return_BetCoin() 傳出的值
	public bool b_Start;//確保只有一個Coroutine的_Wait_Bet在運做 完成時才會再加新的
	public IEnumerator _Wait_Bet;
	public bool b;
	// Use this for initialization
	void Start () {
		
		b_Start = false;
		Button_Down = false;
		Return_Valu = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Button_Down)//判斷按鈕是否按下
        {
			if (gameObject.tag == "Bet")
			{

				Down_Time += Time.deltaTime;
				Debug.Log("Down_Time:" + Down_Time);

				if (Down_Time >= 1f)//Down_Time為長按的時間
				{
					if (!b_Start)//確保只有一個Coroutine的_Wait_Bet在運做 完成時才會再加新的
					{
						b_Start = true;
						StartCoroutine(_Wait_Bet);

					}


				}


			}



		}


		


	}

    public override void OnPointerEnter(PointerEventData eventData)
    {
		if (gameObject.tag=="UiEnlarge")
		{
			Ami = gameObject.GetComponent<Animator>();
			Ami.SetBool("StEnlarge",true);


		}

    }


    public override void OnPointerExit(PointerEventData eventData)
    {
		if (gameObject.tag=="UiEnlarge")
		{

			Ami = gameObject.GetComponent<Animator>();
			Ami.SetBool("StEnlarge",false);


		}
    }


    public override void OnPointerDown(PointerEventData eventData)
    {

		


		if (gameObject.tag == "Bet")
		{
			Down_Time = 0;
			Return_Valu = 0;
			_Wait_Bet = Wait_Bet();
			Button_Down = true;
			Debug.Log("Down-True");
		}


    }
    public override void OnPointerUp(PointerEventData eventData)
    {
		if (gameObject.tag == "Bet")
		{
			Debug.Log("Down-false");
			Rest();
		}
	}


	

	public IEnumerator Wait_Bet()
	{
		
		yield return new WaitForSeconds(0.5f);//等待1秒
        if (b_Start)
        {
            Return_BetCoin();//Return值為100
			b = true;
            Return_Valu = Return_BetCoin(); //累積加值

            Debug.Log("Return_Valu:" + Return_Valu);
        }
		
		
		b_Start = false;//從新開啟條件
		_Wait_Bet = Wait_Bet();//重新賦予任務


	}


	public int Return_BetCoin()
	{

		
		return 1;//回傳1的值
		
	}

	public void Rest()
	{
		Button_Down = false;
		
		b_Start = false;
		

	}


}
