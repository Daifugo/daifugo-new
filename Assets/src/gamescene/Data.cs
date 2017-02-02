using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

public class Data{

	private int _code;
	private JToken _data;

	public Data(int code, JToken data)
	{
		this._code = code;
		this._data = data;
	}

	public int getCode()
	{
		return this._code;
	}

	public JToken getData()
	{
		return this._data;
	}

}