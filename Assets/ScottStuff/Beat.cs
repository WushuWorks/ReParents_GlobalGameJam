using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Beat : MonoBehaviour
{
	public GameObject music;
	public int trackNum;
	public string keyId;

	Music musicScript;
	Image buttonImage;
	void Start () {
		Button btn = this.GetComponent<Button>();
		btn.onClick.AddListener(ButtonClick);
		musicScript = music.GetComponent<Music>();
		buttonImage = this.GetComponent<Image>();
	}
	void ButtonClick(){
		musicScript.HitBeatButton(trackNum);
	}
	void Update ()
    {
	if (Input.GetKeyDown(keyId))
		ButtonClick();
        buttonImage.color = new Color32(255,0,0,0);
        if (musicScript.OnBeat()) {
			//buttonImage.color = new Color32(255,0,0,100);
		}else{
			//buttonImage.color = new Color32(255,255,225,100);
		}
	}
}
