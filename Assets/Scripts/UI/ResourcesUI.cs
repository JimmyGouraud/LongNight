using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour {

	public TextMeshProUGUI StonesValue;
	public TextMeshProUGUI WoodsValue;
	public TextMeshProUGUI GoldsValue;


	void Update() {
        StonesValue.text = ResourcesHandler.Instance.Resources[ResourcesHandler.Resource.stone].ToString();
		WoodsValue.text = ResourcesHandler.Instance.Resources[ResourcesHandler.Resource.wood].ToString();
		GoldsValue.text = ResourcesHandler.Instance.Resources[ResourcesHandler.Resource.gold].ToString();
	}
}
