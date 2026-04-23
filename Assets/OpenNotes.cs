using UnityEngine;
using UnityEngine.InputSystem; // was getting errors for using another version of keydown


public class OpenNotes : MonoBehaviour
{
  public GameObject popupPanel;
  private bool tabOpen = false; // at first the tab is closed

    // Update is called once per frame
    void Update()
    {
        // when tab key is pressed open the tab
         if (Keyboard.current.tabKey.wasPressedThisFrame)
        {   
            tabOpen = !tabOpen;
            popupPanel.SetActive(tabOpen);
        }
        
    }
}

