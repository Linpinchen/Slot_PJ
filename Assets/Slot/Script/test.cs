using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	public Pool_Images[] Poops;
	public int ii = 0;
	void Start () {

		for (int i=0;i<Poops.Length;i++)
		{

			
			ii += XD(Poops[i]);
		}


	}
	

	void Update () {
		
	}




	public int XD(Pool_Images PP)
	{

		if (PP==Pool_Images.Fish)
		{
			int i = (int)Pool_Images.Fish;

			Debug.Log((int)Pool_Images.Fish);
			return i;

		}


		if (PP == Pool_Images.Apple)
		{
			int i = (int)Pool_Images.Apple;

			Debug.Log((int)Pool_Images.Apple);
			return i;

		}

		else { return 1000; }

	}
	
}

