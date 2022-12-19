using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XD : MonoBehaviour {
	public Fun FA;
	public Fun Fb;
	public int oi;
	public List<int> El;
	public List<object> OL;
	public List<Enm> EG;
	// Use this for initialization
	void Start () {
		EG = new List<Enm>();
		OL = new List<object>();
		
		foreach (Enm e in (Enm[])System.Enum.GetValues(typeof(Enm)))
		{
			El.Add((int)e);
		}
		El.Add((int)Enm2.E3);






		//Debug.Log((int)Enm2.E2);
		//Debug.Log("Enm.lengh :"+System.Enum.GetNames(typeof(Enm)).Length);

		
		FA = new Fun(Enm.One,A);
		Fb = new Fun(Enm.Two,B);
		//Debug.Log(FA.SS);
		
		FA._XD = A;

		//FA._XD(FA.SS);
		//System.Array Es = System.Enum.GetValues(typeof(Enm));
		Enm[] ESx = (Enm[])System.Enum.GetValues(typeof(Enm));


		List<int> EE = new List<int>();
		

		foreach (Enm e in System.Enum.GetValues(typeof(Enm)))
		{
			EE.Add((int)e);
		}
		EE.RemoveAt(1);
		for (int i=0;i<EE.Count;i++)
		{

            Debug.Log("EE:" + EE[i]);
            Debug.Log("ESx:" + (int)ESx[i]);
		}
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void C(int i)
	{

		Debug.Log("C");


	}

	public void A<T>(T _Enm)
	{


		Debug.Log("A"+_Enm);


	}


    public void B(Enm _Enm)
    {

        Debug.Log("B"+_Enm);


    }



}

public class Fun
{
	public delegate void WXD<T>(T _T);
	WXD<Enm> OWXD;

	Enm S;
	public Fun(Enm _S, WXD<Enm> _WXD)
	{
		this.S = _S;
		this.OWXD = _WXD;
	}
	public Enm SS
	{
		get { return S; }


	}
	public WXD<Enm> _WWXD
	{
		get { return OWXD; }

	}



	public delegate void XD<T>(T _Enum);

	public XD<Enm> _XD;

	public XD<int> _IXD;
}

public enum Enm
{
One,
Two,
two2,
three


}

public enum Enm2
{
E2=Enm.three+1,
E3,





}



