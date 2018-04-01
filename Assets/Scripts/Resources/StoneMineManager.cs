using UnityEngine;

public class StoneMineManager : MonoBehaviour {

	void OnMouseDown() {
		InformationsUI.Instance.UpdatePanel(this.gameObject);
	}
}
