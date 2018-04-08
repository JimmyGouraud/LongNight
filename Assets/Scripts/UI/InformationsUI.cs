using UnityEngine;

public class InformationsUI : MonoBehaviour {

	public GameObject activedPanel;

	public GameObject panelResources;
	public GameObject panelVillager;
	public GameObject panelCityHall;

	// Static singleton property
	public static InformationsUI Instance { get; private set; }

	void Awake() {
		// First we check if there are any other instances conflicting
		if (Instance != null && Instance != this) {
			// If that is the case, we destroy other instances
			Destroy(gameObject);
		}

		// Here we save our singleton instance
		Instance = this;

		// Furthermore we make sure that we don't destroy between scenes (this is optional)
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		DeactiveAllPanel();

		activedPanel = null;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void UpdatePanel(GameObject go) {
		DeactiveAllPanel();

		if (go.tag == "City Hall") {
			activedPanel = panelCityHall;
			activedPanel.SetActive(true);
		}
		else if (go.tag == "Stone Mine") {
			activedPanel = panelResources;
			activedPanel.SetActive(true);
		}
		else if (go.tag == "Forestry") {
			activedPanel = panelResources;
			activedPanel.SetActive(true);
		}
		else if (go.tag == "Villager") {
			activedPanel = panelVillager;
			activedPanel.SetActive(true);
		}
	}

	void DeactiveAllPanel() {
		panelResources.SetActive(false);
		panelVillager.SetActive(false);
		panelCityHall.SetActive(false);
	}
}
