using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateBecauseUnitySucks : MonoBehaviour
{
	public Sprite[] sprites;
	public GameObject danceFloorCenter; // where they meet
	public float moveMod; // which direction they move
    float totTime;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        totTime = 0;
	       image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
    	totTime += Time.deltaTime;
	int frame = (int)(Mathf.Floor(totTime/1.6666666f*4)%4);
       image.sprite = sprites[frame];
       float meterMax = PlayerStats.Meter_Max;
       float meterVal = PlayerStats.Meter_Val;
       gameObject.transform.localPosition = new Vector2(
		danceFloorCenter.transform.position.x + moveMod*(100+(meterMax-meterVal)/meterMax*(1920-100)/2),
		gameObject.transform.localPosition.y);
		/*
		var tempColor = image.color;
		tempColor.a = (meterMax-meterVal)/meterMax;
		image.color = tempColor;
		*/
    }
}
