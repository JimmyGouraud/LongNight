using UnityEngine;

public class CityHallManager : MonoBehaviour {

	void OnMouseDown() {
		InformationsUI.Instance.UpdatePanel(this.gameObject);
	}
}
