using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour {

	public delegate void ChangeDay();
	public event ChangeDay OnChangeDay;

	public delegate void RunningDay(float progression);
	public event RunningDay OnRunningDay;

	public bool Day { get; private set; }

	float dayDuration = 10f;            // In seconds
	float nightDuration = 5f;           // In seconds
	float longestNightDuration = 25f;	// In seconds

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
				duration = dayDuration / stepDuration;
			}
			else {
				duration = nightDuration / stepDuration;
				nightDuration += 1; // Each night lasts 1 second longer.
			}

			float daytimeProgress = 0;
			OnRunningDay(0);
			while (daytimeProgress < 1) {
				yield return new WaitForSeconds(duration);
				daytimeProgress += stepSlider;
				OnRunningDay(daytimeProgress);
			}
			OnRunningDay(1);
			OnChangeDay();
		}

		Debug.Log("Game Over !");
	}
}
