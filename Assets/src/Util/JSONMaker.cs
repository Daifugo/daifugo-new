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

			foreach (KeyValuePair<string, object> d in data) {
				writer.WritePropertyName (d.Key);
				writer.WriteValue (d.Value);
			}

			writer.WriteEndObject ();
		}
			
		writer.WriteEndObject ();

		return sw.ToString ();
	}
}
