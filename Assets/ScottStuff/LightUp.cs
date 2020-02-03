using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightUp : MonoBehaviour
{
	Image image;
	// Start is called before the first frame update
	void Start()
	{
		image = gameObject.GetComponent<Image>();
	}

	public void DoLightUp()
	{
		var tempColor = image.color;
		tempColor.a = 1;
		image.color = tempColor;
	}

	// Update is called once per frame
	void Update()
	{
		var tempColor = image.color;
		tempColor.a = tempColor.a - Time.deltaTime*2;
		image.color = tempColor;
	}
}
