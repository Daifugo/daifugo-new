using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.IO;

public static class JSONMaker{


	public static string makeJSON(int code, Dictionary<string,object> data = null)
	{

		StringBuilder sb = new StringBuilder ();
		StringWriter sw = new StringWriter (sb);

		JsonWriter writer = new JsonTextWriter (sw);

		writer.Formatting = Formatting.Indented;
		writer.WriteStartObject ();
		writer.WritePropertyName ("code");
		writer.WriteValue (code);

		// Add the data property if hasData is true.

		if (data != null) {
			writer.WritePropertyName ("data");
			writer.WriteStartObject ();

			foreach (KeyValuePair<string, object> d in data) 
			{
				writer.WritePropertyName (d.Key);

				if(d.Value.GetType() == typeof(Dictionary<string,int>[]))
				{
					makeIntArray(writer,d.Value);
				}
				else
				{
					writer.WriteValue (d.Value);
				}
			}

			writer.WriteEndObject ();
		}
			
		writer.WriteEndObject ();

		return sw.ToString ();
	}


	static void makeIntArray(JsonWriter w, object data)
	{
		w.WriteStartArray ();

		var m = (Dictionary<string,int>[])data;

		foreach (Dictionary<string,int> d in m) 
		{
			w.WriteStartObject ();
		
			foreach (KeyValuePair<string, int> it in d) 
			{
				w.WritePropertyName (it.Key);
				w.WriteValue (it.Value);
			}
				
			w.WriteEndObject ();
		}

		w.WriteEndArray ();
	}
}
