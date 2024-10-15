using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour{

        public static int playerStat1;
        // public GameObject textGameObject;

        // void Start () { UpdateScore (); }

        void Update(){
        //NOTE: delete this quit functionality when a Pause Menu is added!
                if (Input.GetKey("escape")){
                        Application.Quit();
                }

                // Stat tester:
                if (Input.GetKey("p")){
                      Debug.Log("Player Stat = " + playerStat1);
                }
        }

        // void UpdateScore () {
        //        Text scoreTemp = textGameObject.GetComponent<Text>();
        //        scoreTemp.text = "Score: " + score; }

        public void StartGame(){
                SceneManager.LoadScene("1");
        }

        // public void OpenCredits(){
        //         SceneManager.LoadScene("Credits");
        // }

        public void RestartGame(){
                Time.timeScale = 1.0f;
                SceneManager.LoadScene(0);
        }

        public void QuitGame(){
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
        }

        public void ResetGame() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("The button is working");
        }

        public void NextLevel2() {
            SceneManager.LoadScene("Level 2");
        }

        public void NextLevel3() {
            SceneManager.LoadScene("Level 3");
        }

        public void NextLevel4() {
            SceneManager.LoadScene("Level 4");
        }

        public void NextLevel5() {
            SceneManager.LoadScene("Level 5");
        }

        public void NextLevel6() {
            SceneManager.LoadScene("Level 6");
        }

        public void NextLevel7() {
            SceneManager.LoadScene("Level 7");
        }
}
