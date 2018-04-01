using UnityEngine;
using UnityEngine.UI;

public class PanelUI : MonoBehaviour {

	public Text text;

	// Static singleton property
	public static PanelUI Instance { get; private set; }

	void Awake() {
		// First we check if there are any other instances conflicting
		if (Instance != null && Instance != this) {
			// If that is the case, we destroy other instances
			Destroy(gameObject);
		}

		// Here we save our singleton instance
		Instance = this;
	}

	public void UpdatePanel(GameObject target) {
		text.text = target.tag;
	}
}
