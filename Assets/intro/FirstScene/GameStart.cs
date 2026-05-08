// Scrift and start button inspired by https://www.youtube.com/watch?v=8kVeDbuqokU
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStart : MonoBehaviour
{
    public void OnPlayButton (){
        SceneManager.LoadScene(1); // looks at the second scene
    }

}
