using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProbabilityText : MonoBehaviour
{
    public Game clicked;
    public TextMeshPro probability;
    public double probabilitycount = 0;
    private bool isPeepClicked = false;

    void Start()
    {
        clicked = FindObjectOfType(typeof(Game)) as Game;
        probabilitycount = 0.021;
        probability.gameObject.SetActive(false); // Initially hide the probability text
    }

    // Update is called once per frame
    void Update()
    {
        if (isPeepClicked)
        {
            CalculateBayesianProbability(clicked.lastcheckedX, clicked.lastcheckedY, clicked.gx, clicked.gy);
            probability.text = probabilitycount.ToString();
        }
    }

    // Method to be called when the "Peep" button is clicked
    public void OnPeepButtonClick()
    {
        isPeepClicked = !isPeepClicked; // Toggle the flag
        probability.gameObject.SetActive(isPeepClicked); // Show or hide the probability text based on the flag
    }

    void CalculateBayesianProbability(int lastcheckedx, int lastcheckedy, int ghostx, int ghosty)
    {
        int Distance = 0, DistanceX = 0, DistanceY = 0;

        probabilitycount = clicked.JointTableProbability("red", 0);
    }
}
