using UnityEngine;
using UnityEngine.InputSystem; // was getting errors for using another version of keydown


public class OpenNotes : MonoBehaviour
{

  public GameObject popupPanel;
  private bool tabOpen = false; // at first the tab is closed

  public GameObject[] whaleNoteSlots;
  private bool[] unlockedNote = new bool[5];


// start without any whale notes - blank notebook
  void Start(){
            foreach (GameObject slot in whaleNoteSlots)
            slot.SetActive(false);
  }

// open notebook when key is pressed
    void Update()
    {
        // when tab key is pressed open the tab
         if (Keyboard.current.tabKey.wasPressedThisFrame)
        {   
            tabOpen = !tabOpen;
            popupPanel.SetActive(tabOpen);
        }
        
    }

// unlocking notes  
    public void UnlockNote(int index)
    {
        if (unlockedNote[index]) return; 

        unlockedNote[index] = true;
        whaleNoteSlots[index].SetActive(true); 

    }
}

