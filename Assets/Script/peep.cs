using UnityEngine;

public class peep : MonoBehaviour
{
    public ProbabilityText probabilityText; // Reference to ProbabilityText script

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("aaaa");
    }

    // Update is called once per frame
    void Update()
    {
        // Check if Fire2 button is pressed
        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Fire2 button pressed!"); // Print message when Fire2 button is pressed
            OnPeepButtonClick(); // Call the OnPeepButtonClick method
        }
    }

    // Method to handle the button click event
    public void OnPeepButtonClick()
    {
        Debug.Log("Peep button clicked!"); // Print message when the button is clicked
        probabilityText.OnPeepButtonClick(); // Call the corresponding method in ProbabilityText script
    }
}
