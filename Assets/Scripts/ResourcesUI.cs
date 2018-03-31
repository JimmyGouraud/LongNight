using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour {

	public Text WoodsText;
	public Text IronsText;

	void Awake() {
		ResourcesManager.Instance.OnChangeResources += UpdateResourcesUI;
	}

	void UpdateResourcesUI() {
		WoodsText.text = "Woods: " + ResourcesManager.Instance.Woods;
		IronsText.text = "Irons: " + ResourcesManager.Instance.Irons;
	}

}
