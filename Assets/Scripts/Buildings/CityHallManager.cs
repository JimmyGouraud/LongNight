using UnityEngine;

public class CityHallManager : MonoBehaviour {

	void OnMouseDown() {
		PanelUI.Instance.UpdatePanel(this.gameObject);
	}
}
