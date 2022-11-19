using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
	[Header("Colors")]
	private Color mainColor = Color.white;
	private Color fillColor = Color.green;
	
	[Header("General")]
	private int numSegments = 10;
	[Range(0, 360)]  private float startAngle = 10;
	[Range(0, 360)]  private float endAngle = 350;
	 private float notchSize = 5;
	[Range(0, 1f)]  private float fillAmount = 0.0f;

	private Image image;
	private List<Image> progressToFill = new List<Image> ();
	private float sizeOfSegment;

	private void Awake() {
		// Get images in Children
		image = GetComponentInChildren<Image>();
		image.color = mainColor;
		image.gameObject.SetActive(false);

		// Calculate notches
		float startNormalAngle = NormalizeAngle(startAngle);
		float endNormalAngle = NormalizeAngle(360 - endAngle);
		float notchesNormalAngle = (numSegments - 1) * NormalizeAngle(notchSize);
		float allSegmentsAngleArea = 1 - startNormalAngle - endNormalAngle - notchesNormalAngle;
		
		// Count size of segments
		sizeOfSegment = allSegmentsAngleArea / numSegments;
		for (int i = 0; i < numSegments; i++) {
			GameObject currentSegment = Instantiate(image.gameObject, transform.position, Quaternion.identity, transform);
			currentSegment.SetActive(true);

			Image segmentImage = currentSegment.GetComponent<Image>();
			segmentImage.fillAmount = sizeOfSegment;

			Image segmentFillImage = segmentImage.transform.GetChild (0).GetComponent<Image> ();
			segmentFillImage.color = fillColor;
			progressToFill.Add (segmentFillImage);

			float zRot = startAngle + i * ConvertCircleFragmentToAngle(sizeOfSegment) + i * notchSize;
			segmentImage.transform.rotation = Quaternion.Euler(0,0, -zRot);
		}
	}

	private void redrawProgressBar() {
		for (int i = 0; i < numSegments; i++) {
			progressToFill [i].fillAmount = (fillAmount * ((endAngle-startAngle)/360)) - sizeOfSegment * i;
		}
	}
	
	public void updateProgressBar() {
		if (fillAmount <= 1.0f) {
			fillAmount += 0.09f;
			this.redrawProgressBar();
		}
	}

	private float NormalizeAngle(float angle) {
		return Mathf.Clamp01(angle / 360f);
	}

	private float ConvertCircleFragmentToAngle(float fragment) {
		return 360 * fragment;
	}
}