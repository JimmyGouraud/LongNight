using UnityEngine;

public class ResourcesManager : MonoBehaviour {

	public delegate void ChangeResources();
	public event ChangeResources OnChangeResources;

	private int woods = 0;
	public int Woods {
		get { return woods; }

		private set {
			woods = value;
			if (OnChangeResources != null) {
				OnChangeResources();
			}
		}
	}

	private int stones = 0;
	public int Stones {
		get { return stones; }

		private set {
			stones = value;
			if (OnChangeResources != null) {
				OnChangeResources();
			}
		}
	}

	// public int Villagers { get; private set; }
	// public int Warriors  { get; private set; }


	// Static singleton property
	public static ResourcesManager Instance { get; private set; }

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

	void Start() {
		this.Woods = 0;
		this.Stones = 0;
	}

	public void AddWoods(int woods) {
		this.Woods += woods;
	}

	public void AddStones(int stones) {
		this.Stones += stones;
	}
}
