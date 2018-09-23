using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public GameObject menu;
	public GameObject credits;
	public GameObject instructions;

	public void PlayGame(){
		SceneManager.LoadScene("rodrigo");
	}

	public void BackToMenu(){
		SceneManager.LoadScene("Menu");	
	}

	public void Quit(){
		Application.Quit();
	}

	public void ToggleCredits(){
		menu.SetActive(!menu.activeSelf);
		credits.SetActive(!credits.activeSelf);
	}

	public void ToggleInstructions(){
		menu.SetActive(!menu.activeSelf);
		instructions.SetActive(!instructions.activeSelf);
	}
}
