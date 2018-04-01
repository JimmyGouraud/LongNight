using UnityEngine;

public class ForestryManager : MonoBehaviour {

	void OnMouseDown() {
		PanelUI.Instance.UpdatePanel(this.gameObject);
	}
}
