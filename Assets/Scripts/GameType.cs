using UnityEngine;
using System.Collections;

public class GameType{
	public string typevar=null;
	public string namevar=null;
	public Vector3 positionvar=Vector3.zero;
	public Vector3 scalevar=Vector3.zero;

	public bool boolean1var=false;
	public bool boolean2var=false;
	public bool boolean3var=false;
	public bool boolean4var=false;

	public float number1var=0;
	public float number2var=0;
	public float number3var=0;

	public GameType()
	{}

	public GameType(string type, string name, Vector3 position, Vector3 scale)
	{
		typevar=type;
		namevar=name;
		positionvar=position;
		scalevar=scale;
	}

	public GameType(string type, string name, Vector3 position, Vector3 scale, bool boolean1, bool boolean2, float float1)
	{
		typevar=type;
		namevar=name;
		positionvar=position;
		scalevar=scale;
		boolean1var=boolean1;
		boolean2var=boolean2;
		number1var=float1;
	}

	public GameType(string type, string name, Vector3 position, Vector3 scale, float float1)
	{
		typevar=type;
		namevar=name;
		positionvar=position;
		scalevar=scale;
		number1var=float1;
	}

	public GameType(string type, string name, Vector3 position, Vector3 scale, float float1, float float2, float float3, bool boolean1, bool boolean2, bool boolean3, bool boolean4)
	{
		typevar=type;
		namevar=name;
		positionvar=position;
		scalevar=scale;
		number1var=float1;
		number2var=float2;
		number3var=float3;
		boolean1var=boolean1;
		boolean2var=boolean2;
		boolean3var=boolean3;
		boolean4var=boolean4;
	}

	public GameType(string type, string name, Vector3 position, Vector3 scale, float float1, float float2, bool boolean1, bool boolean2, bool boolean3, bool boolean4)
	{
		typevar=type;
		namevar=name;
		positionvar=position;
		scalevar=scale;
		number1var=float1;
		number2var=float2;
		boolean1var=boolean1;
		boolean2var=boolean2;
		boolean3var=boolean3;
		boolean4var=boolean4;
	}
}
