using System.Collections;
using System.Collections.Generic;
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

	private int irons = 0;
	public int Irons {
		get { return irons; }

		private set {
			irons = value;
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
		Woods = 0;
		Irons = 0;
	}

	public void AddWoods(int woodPieces) {
		Woods += woodPieces;
	}

	public void AddIrons(int ironOres) {
		Irons += ironOres;
	}
}
