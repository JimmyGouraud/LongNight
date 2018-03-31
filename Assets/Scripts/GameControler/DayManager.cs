﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour {

	public Slider DaySlider;

	public delegate void ChangeDay();
	public event ChangeDay OnChangeDay;

	private float dayDuration = 10f;            // In seconds
	private float nightDuration = 5f;           // In seconds
	private float longestNightDuration = 25f;	// In seconds

	private bool day = false;
	public bool Day {

		get { return day; }

		private set {
			day = value;
			if (OnChangeDay != null) {
				OnChangeDay();
			}
		}
	}

	// Static singleton property
	public static DayManager Instance { get; private set; }

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

	void Start () {
		StartCoroutine(RunningDayCycle());
	}

	IEnumerator RunningDayCycle() {
		float stepDuration = 100f;
		float stepSlider = 1 / stepDuration;

		float duration = 0;
		while (nightDuration < longestNightDuration) {
			Day = !Day;
		
			if (Day) {
				Debug.LogFormat("day duration: {0}s | night duration: {1}s", dayDuration, nightDuration);
				duration = dayDuration / stepDuration;
			}
			else {
				duration = nightDuration / stepDuration;
				nightDuration += 1; // Each night lasts 1 second longer.
			}

			DaySlider.value = 0;
			while (DaySlider.value < 1) {
				yield return new WaitForSeconds(duration);
				DaySlider.value += stepSlider;
			}
		}

		Debug.Log("Game Over !");
	}
}