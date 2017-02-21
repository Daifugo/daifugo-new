using UnityEngine;
using System.Collections;

public abstract class CardRenderer : MonoBehaviour {

	protected CardGameSpawner _spawner = null;

	public virtual void render(Card s){}

	public abstract void removeCard(Card s); 

	protected abstract void setImage(GameObject card, int suit, int rank);
	protected abstract void setGeometry(GameObject card, float xCoor = 0.0f);

	protected virtual GameObject getObject(string prefabName)
	{
		GameObject cardPrefab = Resources.Load("prefabs/"+prefabName, typeof(GameObject)) as GameObject;
		return Instantiate (cardPrefab, Vector3.zero, Quaternion.identity, transform) as GameObject;
	}
	
}