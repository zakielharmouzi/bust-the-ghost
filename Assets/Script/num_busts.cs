using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class num_busts : MonoBehaviour
{
    public TextMeshProUGUI bustText; // Reference to the TextMeshProUGUI component

    void Start()
    {
         UpdateBustText(0);
    }

    // Call this method to update the text displayed on the TMP component
    public void UpdateBustText(int numBusts)
    {

        if (bustText != null)
        {
            bustText.text = "Busts: " + numBusts.ToString();
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is not assigned in the Inspector!");
        }
    }
}
