using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DrawLine : MonoBehaviour {
	public LineRenderer LI;
	public List<Transform> Taget_Point;//目標
	public List<Transform> Temp_point;//暫存
	public float DrawSpeed;
	public GameObject Orange_point;//原始位
	public Transform Temp_VV;
	public bool StDrawLine;
	// Use this for initialization
	void Start () {
		DrawSpeed = 50;
		LI = gameObject.GetComponent<LineRenderer>();
		//Temp_point = new List<Transform>();
		//Temp_point.Add(Orange_point.transform);
		//Temp_point.Add(gameObject.transform);
		//gameObject.transform.position = Orange_point.transform.position;

		Temp_VV = Taget_Point[0].transform;


	}
	
	// Update is called once per frame
	void Update () {

		if (StDrawLine)
		{


			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Temp_VV.position, DrawSpeed * Time.deltaTime);
			

			for (int i = 0; i < Temp_point.Count; i++)
			{

				LI.SetPosition(i, Temp_point[i].position);


			}


			for (int j = 0; j < Taget_Point.Count; j++)
			{

				if (gameObject.transform.position == Taget_Point[j].transform.position)//如果移動得座標到達目標點
				{
					if (j + 1 < Taget_Point.Count)
					{
						//Debug.Log("StDrawLine__ Taget_Point.Count :" + Taget_Point.Count);
						//Debug.Log(j);
						//Debug.Log("StDrawLine__ Temp_point.Count" + Temp_point.Count);
						Temp_VV = Taget_Point[j + 1].transform;//更換暫存目標點 （換成下一個目標）
						Temp_point[j + 1] = Taget_Point[j];//加入到達的那個點（設定畫線用（加入中繼點））
						Temp_point.Add(gameObject.transform);//從新加入移動點來移動到目標

						LI.positionCount++;
						for (int i = 0; i < Temp_point.Count; i++)
						{

							LI.SetPosition(i, Temp_point[i].position);


						}




					}



				}


			}

			if (gameObject.transform.position == Taget_Point[Taget_Point.Count - 1].transform.position)
			{
				StDrawLine = false;
				Debug.Log("StDrawLine-Bool :" + StDrawLine);



			}




		}



	}



}
