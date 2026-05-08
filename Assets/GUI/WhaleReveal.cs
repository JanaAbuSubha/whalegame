using UnityEngine;

public class WhaleReveal : MonoBehaviour
{
  public GameObject[] whaleImage;

// start without any whale notes - blank notebook
  void Start(){
    foreach (GameObject slot in whaleImage)
        slot.SetActive(false);
  }

// unlocking notes  
    public void ShowWhale(int index) {
        whaleImage[index].SetActive(true); 
    }
}

