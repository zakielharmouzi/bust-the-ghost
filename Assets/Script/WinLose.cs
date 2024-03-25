using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WinLose : MonoBehaviour 
{
    private Sprite gameoverimgl;
    public UnityEvent buttonClick;
    public UnityEvent button_2Click;
    public num_busts num_Busts;
    public int numBusts_2 = 0;

    public Sprite btn;
    public Game var;

    void Awake()
    { 
        if (buttonClick == null)
        {
            buttonClick = new UnityEvent();  
        }
    }

    void Start()
    {
        num_Busts = FindObjectOfType<num_busts>();
        if (num_Busts == null)
        {
            Debug.LogError("num_busts script not found in the scene.");
        }
        var = FindObjectOfType(typeof(Game)) as Game;
    }

    void Update()
    {
        // Check regular click (bust action)
        if (Input.GetButtonDown("Fire1"))
        {
            CheckInput();
            var.CheckInputGrid();
        }
    }

    private void CheckInput() 
    { 
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int a = Mathf.RoundToInt(mousePosition.x);
        int b = Mathf.RoundToInt(mousePosition.y);
        if (a > 9 || b > 12 || a < 0 || b < 0)
        {
            if (var.gx == var.lastcheckedX && var.gy == var.lastcheckedY) 
            { 
                var.grid[var.gx, var.gy].probability.text = "WIN !";
                Debug.Log("You Busted the ghost ! CONGRATS !");
                Tile winimage = Instantiate(Resources.Load("Prefabs/WON", typeof(Tile)), new Vector3(0, 0, 0), Quaternion.identity) as Tile;
                var.grid[0, 0] = winimage;
                Tile ghostcell = Instantiate(Resources.Load("Prefabs/ghostpic", typeof(Tile)), new Vector3(var.gx, var.gy, 0), Quaternion.identity) as Tile;
                Tile T = var.grid[var.gx, var.gy];
                T.SetIsCovered(false);
                var.grid[var.gx, var.gy] = ghostcell;
            } 
            else 
            { 
                Debug.Log("You busted the wrong cell ! GAME OVER !");
                Tile gameoverimage = Instantiate(Resources.Load("Prefabs/GameOver", typeof(Tile)), new Vector3(0, 0, 0), Quaternion.identity) as Tile;
                var.grid[0, 0] = gameoverimage;
                var.grid[var.gx, var.gy].probability.text = "LOSS !";
            }
        }
        else
        {
            // Increase bust count for regular click action
            numBusts_2++;
            num_Busts.UpdateBustText(numBusts_2);
        }
    }
}
