using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayUI : MonoBehaviour {
	// TODO: Find a way to smooth the slider progression (it's currently jerky)

	public Text DayText;
	public Slider DayProgression;
	public Image SlideBackground;
	public Image SliderFillArea;

	private Color dayColor  = new Color(1, 0.63f, 0.4f);
	private Color nightColor = new Color(0.4f, 0.5f, 1);

	void Awake () {
		DayManager.Instance.OnChangeDay += UpdateText;
	}

	void UpdateText() {
		if (DayManager.Instance.Day) {
			DayText.text = "Day";
			DayText.color = dayColor;
			SliderFillArea.color = dayColor;
			SlideBackground.color = dayColor - new Color(0, 0, 0, 0.5f);
		}
		else {
			DayText.text = "Night";
			DayText.color = nightColor;
			SliderFillArea.color = nightColor;
			SlideBackground.color = nightColor - new Color(0, 0, 0, 0.5f);
		}
	}
}
