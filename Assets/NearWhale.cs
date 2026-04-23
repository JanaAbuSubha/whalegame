using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class proximityWhale : MonoBehaviour
{
    // swimmer position
    public Transform PlayerCapsule;  


    // whales
    public Transform orcaWhale;
    public Transform pilotWhale;
    public Transform humpackWhale;


    // text
    public TMP_Text proximityText;

    void Update()
    {
        // calculate volume and make it volume valid number (b/w 0 and 1)
        float distanceOrca = Vector3.Distance(PlayerCapsule.position, orcaWhale.position);
        float distancePilot = Vector3.Distance(PlayerCapsule.position, pilotWhale.position);
        float distanceHumpback = Vector3.Distance(PlayerCapsule.position, humpackWhale.position);


        // near whale or not
        bool isNearOrca = distanceOrca <= 5f;
        bool isNearPilot = distancePilot <= 5f;
        bool isNearHumpback = distanceHumpback <= 5f;

     // text change
        if (isNearOrca)
        {
            proximityText.text = "Orca Found";
        }
        else
        {
            proximityText.text = "Not Found: Orca";
        }



    }
}