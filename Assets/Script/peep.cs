using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class peep : MonoBehaviour
{
    public ProbabilityText probabilityText; // Reference to ProbabilityText script

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("aaaa");
    }

    // Method to handle the button click event
    public void OnPeepButtonClick()
    {
        Debug.Log("Peep button clicked!"); // Print message when the button is clicked
        probabilityText.OnPeepButtonClick(); // Call the corresponding method in ProbabilityText script
    }
}
