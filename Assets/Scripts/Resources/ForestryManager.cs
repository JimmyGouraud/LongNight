using UnityEngine;

public class ForestryManager : MonoBehaviour {

	void OnMouseDown() {
		InformationsUI.Instance.UpdatePanel(this.gameObject);
	}
}
