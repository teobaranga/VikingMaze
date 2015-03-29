using UnityEngine;
using System.Collections;

public class MainMenuGUI : BaseGUI {
	private int currentSave;
	private bool actionTaken = false;

	public override void Start() {
		base.Start();

		enableGuiControl();

		currentSave = PlayerPrefs.GetInt("HighestCompletedChapter", -1);
	}

	public override void OnGUI() {
		base.OnGUI();

		GUILayout.BeginArea(AspectUtility.screenRect);

		GUILayout.BeginArea(new Rect(scalePx(20), (AspectUtility.screenHeight - scalePx(150)) , scalePx(200), scalePx(200)));

		numberOfButtonsVisible = 0;

		GUI.SetNextControlName("0");
		if(GUILayout.Button("Start New Game", guiSkin.customStyles[5])) {
			beginGame();
		}

		numberOfButtonsVisible++;

		GUI.enabled = true;
		GUI.SetNextControlName(numberOfButtonsVisible.ToString());
		if(GUILayout.Button("Quit Game", guiSkin.customStyles[5])) {
			Application.Quit();
		}
		
		numberOfButtonsVisible++;

		GUILayout.EndArea();
		GUILayout.EndArea();
	}


	public override void Update() {
		base.Update();

		if(input1IsDown) {
			if(currentButtonSelection == 0) {
				beginGame();
			} else if(currentButtonSelection == 1) {
				Application.Quit();
			} 
		}
	}

	private void beginGame() {
		if(!actionTaken) {
			GameObject.Find("BGM").GetComponent<MusicPlayer>().StopMusic(1.0f);
			Camera.main.GetComponent<CameraFade>().FadeAndNext(Color.black, 2.0f, "Main");
			
			actionTaken = true;
		}
	}
	
}
