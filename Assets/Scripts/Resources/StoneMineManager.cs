using UnityEngine;

public class StoneMineManager : MonoBehaviour {

	void OnMouseDown() {
		PanelUI.Instance.UpdatePanel(this.gameObject);
	}
}
