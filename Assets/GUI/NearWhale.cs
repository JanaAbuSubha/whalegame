using UnityEngine;
using UnityEngine.UI;

public class proximityWhale : MonoBehaviour
{
    // swimmer position
    public Transform PlayerCapsule;  

    // whales
    public Transform orcaWhale;
    public Transform pilotWhale;
    public Transform humpackWhale;
    public Transform bowheadWhale;
    public Transform narwhalWhale;

    // whale note + whale image
    public OpenNotes notebookController;
    public WhaleReveal whaleHUD;

    // meter distance
    public float proximityMeters = 21f;

    void Update()
    {
        // calculate volume and make it volume valid number (b/w 0 and 1)
        float distanceOrca = Vector3.Distance(PlayerCapsule.position, orcaWhale.position);
        float distancePilot = Vector3.Distance(PlayerCapsule.position, pilotWhale.position);
        float distanceHumpback = Vector3.Distance(PlayerCapsule.position, humpackWhale.position);
        float distancebowhead = Vector3.Distance(PlayerCapsule.position, bowheadWhale.position);
        float distanceNarwhal = Vector3.Distance(PlayerCapsule.position, narwhalWhale.position);

        // near whale or not
        bool isNearOrca = distanceOrca <= proximityMeters;
        bool isNearPilot = distancePilot <= proximityMeters;
        bool isNearHumpback = distanceHumpback <= proximityMeters;
        bool isNearBowhead= distancebowhead <= proximityMeters;
        bool isNearNarwhal= distanceNarwhal <= proximityMeters;

        // update notebook
        if (isNearOrca){
            notebookController.UnlockNote(0);
            whaleHUD.ShowWhale(0);
        }if (isNearPilot){
            notebookController.UnlockNote(1);
             whaleHUD.ShowWhale(1);
        }if (isNearHumpback){
            notebookController.UnlockNote(2); 
            whaleHUD.ShowWhale(2);
        }if (isNearBowhead){
            notebookController.UnlockNote(3); 
            whaleHUD.ShowWhale(3);
        }if (isNearNarwhal){
            notebookController.UnlockNote(4);
            whaleHUD.ShowWhale(4);
        }
    }
}