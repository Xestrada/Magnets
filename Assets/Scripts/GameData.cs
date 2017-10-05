using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameData : MonoBehaviour {

	public static GameData data;

	//User Data and Options
	public float highScore;

    bool created;

    public bool isAmazon = false;

	//Checks if this is in scene
	void Awake () {
		if (!created) {
			DontDestroyOnLoad (gameObject);
			created = true;
			data = this;
		}else{
			Destroy (this.gameObject);
		}
			
	}

    void Start()
    {
        Load();
    }


    //Saves Progress
    public void Save(){
		BinaryFormatter write = new BinaryFormatter ();
		FileStream save = File.Create (Application.persistentDataPath + "/magnets.vt");

		PlayerData saveData = new PlayerData ();
		saveData.highScore = highScore;
        write.Serialize (save, saveData);
		save.Close ();
	}
		

	//Loads SaveGame
	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/magnets.vt")) {
			//Opens playerData and redas if avialable
			BinaryFormatter read = new BinaryFormatter ();
			FileStream loadData = File.Open (Application.persistentDataPath + "/magnets.vt", FileMode.Open);

			PlayerData loadedData = (PlayerData)read.Deserialize (loadData);
			highScore = loadedData.highScore;
			loadData.Close ();
		}else {
            //Sets up Default Settings and Variables at first run
            highScore = 0;

        }
	}



	//Saves if Application is Quit
	void OnApplicationQuit(){
		GameData.data.Save ();

	}

	//public static IEnumerator TakeScreenshot(){
		
	//	yield return new WaitForEndOfFrame ();
	//	GameData.data.tex.ReadPixels (new Rect (0, 0, Screen.width, Screen.height), 0, 0, false);


	//	GameData.data.tex.Apply (false);
	//	File.WriteAllBytes (Application.persistentDataPath + "/" + "dreamJump_screen.png", GameData.data.tex.EncodeToPNG());
	//}
	
}


//This class holds the Serializable data
[System.Serializable]
class PlayerData {
	public float highScore;
   
}