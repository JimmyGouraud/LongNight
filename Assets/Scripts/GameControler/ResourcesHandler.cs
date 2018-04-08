using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesHandler : MonoBehaviour {

	public enum Resource { wood, stone, gold }

    public Dictionary<Resource, int> Resources;

	// Static singleton property
	public static ResourcesHandler Instance { get; private set; }

	void Awake()
	{
		// First we check if there are any other instances conflicting
		if (Instance != null && Instance != this) {
			// If that is the case, we destroy other instances
			Destroy(gameObject);
		}

		// Here we save our singleton instance
		Instance = this;

		// Furthermore we make sure that we don't destroy between scenes
		DontDestroyOnLoad(gameObject);

		InitResources();
	}

	void InitResources()
	{
		Resources = new Dictionary<Resource, int>();
		foreach (Resource resource in Enum.GetValues(typeof(Resource))) {
			Resources.Add(resource, 0);
		}
	}
}
