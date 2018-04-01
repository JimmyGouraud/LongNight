using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour {

	public Text WoodsText;
	public Text StonesText;

	void Awake() {
		ResourcesManager.Instance.OnChangeResources += UpdateResourcesUI;
	}

	void UpdateResourcesUI() {
		WoodsText.text = "Woods: " + ResourcesManager.Instance.Woods;
		StonesText.text = "Stones: " + ResourcesManager.Instance.Stones;
	}

}
