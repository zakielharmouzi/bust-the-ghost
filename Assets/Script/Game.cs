using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System; 
public class Game : MonoBehaviour
{
    
    public Tile[,] grid = new Tile[9, 12];
     
    
    public double probabilitycount = 2; 
    public int gx;  int NC=0;  double GHOSTPROBABILITY =0;
    public  int gy;
    public  int lastcheckedX;
    public  int lastcheckedY;
    public int NumberOfOranges=0;
    public int NumberOfReds=2;
    public int NumberOfYellow=0;
    public int NumberOfGreens=0;
    public int redDistance=0;
    public int num_of_busts = 0;
    private bool fire2Clicked = false;

    public double JointTableProbability(string color, int DistanceFromGhost) 
     { 
        //Table 1
         if(color.Equals("yellow") && (DistanceFromGhost==3 || DistanceFromGhost==4) ) return 0.5;
         if(color.Equals("red") && (DistanceFromGhost==3 || DistanceFromGhost==4)) return 0.05;
         if(color.Equals("green") && (DistanceFromGhost==3 || DistanceFromGhost==4)) return 0.3;
         if(color.Equals("orange") && (DistanceFromGhost==3 || DistanceFromGhost==4)) return 0.15;

        //Table2
         if(color.Equals("yellow") && (DistanceFromGhost==1 || DistanceFromGhost==2) ) return 0.15;
         if(color.Equals("red") && (DistanceFromGhost==1 || DistanceFromGhost==2)) return 0.3;
         if(color.Equals("green") && (DistanceFromGhost==1 || DistanceFromGhost==2)) return 0.05;
         if(color.Equals("orange") && (DistanceFromGhost==1 || DistanceFromGhost==2)) return 0.5;
         
        //Table3
         if(color.Equals("yellow") && DistanceFromGhost>=5 ) return 0.3;
         if(color.Equals("red") && DistanceFromGhost>=5) return 0.05;
         if(color.Equals("green") && DistanceFromGhost>=5) return 0.5;
         if(color.Equals("orange") && DistanceFromGhost>=5) return 0.15;

        //Table4
         if(color.Equals("red") && DistanceFromGhost==0) return 0.7;
         if(color.Equals("yellow") && DistanceFromGhost==0) return 0.05;
         if(color.Equals("green") && DistanceFromGhost==0) return 0.05;
         if(color.Equals("orange") && DistanceFromGhost==0) return 0.2;

         return 0;
    }
    
     
     
    
    void Start() { 
        PlaceGhost();
        

    }
    void Update()
    {
        // Check if Fire2 button is pressed
        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Fire2 button pressed!");
            fire2Clicked = true;
        }

        CheckInputGrid();
    }



    public void CheckInputGrid()
    {
        int Distance = 0, DistanceX = 0, DistanceY = 0;
       
        if (Input.GetButtonDown("Fire1"))
        {
            num_of_busts++;

            if (num_of_busts > 58)
            {
                Debug.Log("Number of busts exceeded 20!");
                return;
            }

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int a = Mathf.RoundToInt(mousePosition.x);
            int b = Mathf.RoundToInt(mousePosition.y);
            if (a > 9 || b > 12 || a < 0 || b < 0)
            {
                return;
            }
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.RoundToInt(mousePosition.x);
            int y = Mathf.RoundToInt(mousePosition.y);
            lastcheckedX = x; lastcheckedY = y;

            if (lastcheckedX >= gx) DistanceX = lastcheckedX - gx;
            else DistanceX = gx - lastcheckedX;
            if (lastcheckedY >= gy) DistanceY = lastcheckedY - gy;
            else DistanceY = gy - lastcheckedY;
            Distance = DistanceX + DistanceY;
            Tile tile = grid[x, y];
            tile.SetIsCovered(false);

            if (fire2Clicked)
            {
                if (JointTableProbability("green", Distance) >= 0.5)
                {
                    grid[x, y].probability.text = Math.Round(((JointTableProbability("green", Distance) * 0.05)) / 160, 4).ToString();
                    int G = NumberOfGreens;
                    redDistance = Distance;
                    for (int yy = 0; yy < 12; yy++)
                    {
                        for (int xx = 0; xx < 9; xx++)
                        {
                            if (xx != lastcheckedX && yy != lastcheckedY)
                            {
                                if (xx >= gx) DistanceX = xx - gx;
                                else DistanceX = gx - xx;
                                if (yy >= gy) DistanceY = yy - gy;
                                else DistanceY = gy - yy;
                                Distance = DistanceX + DistanceY;
                                if (JointTableProbability("green", Distance) >= 0.5)
                                    grid[xx, yy].probability.text = Math.Round(((JointTableProbability("green", Distance) * 0.05)) / 160, 4).ToString();
                                if (JointTableProbability("yellow", Distance) >= 0.5) grid[xx, yy].probability.text = Math.Round((JointTableProbability("yellow", Distance) * 0.15) / (160 - NumberOfYellow), 4).ToString();
                                if (JointTableProbability("orange", Distance) >= 0.5) grid[xx, yy].probability.text = Math.Round((JointTableProbability("orange", Distance) * 0.3) / (160 - NumberOfOranges), 4).ToString();
                                if (JointTableProbability("red", Distance) >= 0.5) grid[xx, yy].probability.text = Math.Round((JointTableProbability("red", Distance) * 0.5), 4).ToString();
                            }


                        }
                    }
                }
                else if (JointTableProbability("yellow", Distance) >= 0.5)
                {
                    grid[x, y].probability.text = Math.Round(((JointTableProbability("yellow", Distance) * 0.15)) / NumberOfYellow, 4).ToString();
                    redDistance = Distance;
                    for (int yy = 0; yy < 12; yy++)
                    {
                        for (int xx = 0; xx < 9; xx++)
                        {
                            if (xx != lastcheckedX && yy != lastcheckedY)
                            {
                                if (xx >= gx) DistanceX = xx - gx;
                                else DistanceX = gx - xx;
                                if (yy >= gy) DistanceY = yy - gy;
                                else DistanceY = gy - yy;
                                Distance = DistanceX + DistanceY;
                                if (JointTableProbability("yellow", Distance) >= 0.5) grid[xx, yy].probability.text = Math.Round((JointTableProbability("yellow", Distance) * Distance / (Distance * NumberOfGreens)), 4).ToString();
                                if (JointTableProbability("green", Distance) >= 0.5) grid[xx, yy].probability.text = Math.Round((JointTableProbability("green", Distance) * 1 / (Distance * NumberOfGreens)), 4).ToString();
                                if (JointTableProbability("orange", Distance) >= 0.5) grid[xx, yy].probability.text = Math.Round((JointTableProbability("orange", Distance) * 1 / (Distance * NumberOfOranges)), 4).ToString();
                                if (JointTableProbability("red", Distance) >= 0.5) grid[xx, yy].probability.text = Math.Round((JointTableProbability("red", Distance) * 0.6), 3).ToString();

                            }
                        }
                    }

                }
                else if (JointTableProbability("orange", Distance) >= 0.5)
                {
                    grid[x, y].probability.text = Math.Round((JointTableProbability("orange", Distance) * Distance / (Distance * NumberOfGreens)), 3).ToString();
                    redDistance = Distance;
                    if (NC < 7) NC++;
                    for (int yy = 0; yy < 12; yy++)
                    {
                        for (int xx = 0; xx < 9; xx++)
                        {
                            if (xx != lastcheckedX && yy != lastcheckedY)
                            {
                                if (xx >= gx) DistanceX = xx - gx;
                                else DistanceX = gx - xx;
                                if (yy >= gy) DistanceY = yy - gy;
                                else DistanceY = gy - yy;
                                Distance = DistanceX + DistanceY;
                                if (JointTableProbability("yellow", Distance) >= 0.5) grid[xx, yy].probability.text = Math.Round((JointTableProbability("yellow", Distance) * 1 / (Distance + NumberOfYellow)), 3).ToString();
                                if (JointTableProbability("green", Distance) >= 0.5) grid[xx, yy].probability.text = Math.Round((JointTableProbability("green", Distance) * 1 / (Distance * NumberOfGreens)) / NumberOfOranges, 4).ToString();
                                if (JointTableProbability("red", Distance) >= 0.5)
                                {
                                    if (GHOSTPROBABILITY < 1)
                                    {

                                        grid[xx, yy].probability.text = Math.Round(NumberOfGreens / 160 + (NC - 0.05) / 7, 3).ToString();
                                        GHOSTPROBABILITY = NumberOfGreens / 160 + (NC - 0.05) / 7;
                                    }

                                }







                                if (JointTableProbability("orange", Distance) >= 0.5)
                                {
                                    grid[xx, yy].probability.text = Math.Round((JointTableProbability("orange", Distance) * Distance / (Distance * NumberOfOranges * NumberOfOranges)), 3).ToString();

                                }

                            }
                        }
                    }

                }
                else
                {
                    grid[x, y].probability.text = Math.Round((JointTableProbability("red", Distance) + 0.3), 3).ToString();

                    for (int yy = 0; yy < 12; yy++)
                    {
                        for (int xx = 0; xx < 9; xx++)
                        {
                            if (xx != lastcheckedX && yy != lastcheckedY)
                            {
                                if (xx >= gx) DistanceX = xx - gx;
                                else DistanceX = gx - xx;
                                if (yy >= gy) DistanceY = yy - gy;
                                else DistanceX = gy - yy;
                                Distance = DistanceX + DistanceY;
                                if (JointTableProbability("yellow", Distance) >= 0.5) grid[xx, yy].probability.text = Math.Round(((JointTableProbability("yellow", Distance) * 1 / (Distance)) / 160), 4).ToString();
                                if (JointTableProbability("orange", Distance) >= 0.5) grid[xx, yy].probability.text = Math.Round(((JointTableProbability("orange", Distance) * 1 / (Distance)) / 160), 4).ToString();
                                if (JointTableProbability("green", Distance) >= 0.5) grid[xx, yy].probability.text = Math.Round(((JointTableProbability("green", Distance) * 1 / (Distance)) / 160), 4).ToString();

                            }
                        }
                    }

                }


            }
        }


    }




    public void PlaceGhost () 
    { 
                 int x = UnityEngine.Random.Range(0, 9);
                 int y = UnityEngine.Random.Range(0, 12);
            if( grid[x, y]==null)
             { 
                Tile ghostTile =  Instantiate(Resources.Load("Prefabs/red", typeof(Tile)), new Vector3(x, y, 0), Quaternion.identity) as Tile;
                grid[x, y]=ghostTile;
               
                gx=x; gy=y;
                Debug.Log("("+gx+", "+ gy+ ")");
                PlaceNoisyPrint();
                PlaceColor(x, y);
                 
                
            } else { 
                PlaceGhost();
            }

    }
    public void PlaceNoisyPrint()  
    {
        int x = UnityEngine.Random.Range(0, 9);
        int y = UnityEngine.Random.Range(0, 12);
            if( grid[x, y]==null )
             { 
                Tile noisyPrint =  Instantiate(Resources.Load("Prefabs/red", typeof(Tile)), new Vector3(x, y, 0), Quaternion.identity) as Tile;
                grid[x, y]=noisyPrint;
            } else { 
                PlaceNoisyPrint();
            }
    }
    
    public void PlaceColor(int X, int Y)
    {   int DistanceX=0, DistanceY=0, Distance=0;
        for (int y =0 ; y< 12; y++) { 
            for (int x = 0; x < 9; x++)
            {
                
                    if(x>=X) DistanceX = x-X;
                             else DistanceX = X-x;
                    if(y>=Y) DistanceY = y-Y;
                             else DistanceY = Y-y;
                    Distance=DistanceX+DistanceY;

                    
                    
                    if(JointTableProbability("green", Distance)>=0.5 && JointTableProbability("yellow", Distance)<0.5 && JointTableProbability("orange", Distance)<0.5 && grid[x,y]==null)
                     { 
                        Tile color =  Instantiate(Resources.Load("Prefabs/green", typeof(Tile)), new Vector3(x, y, 0), Quaternion.identity) as Tile;
                        grid[x, y]=color;
                        NumberOfGreens++;

                    } 
                         else if (JointTableProbability("yellow", Distance)>=0.5 && grid[x,y]==null) {
                        Tile color =  Instantiate(Resources.Load("Prefabs/yellow", typeof(Tile)), new Vector3(x, y, 0), Quaternion.identity) as Tile;
                        grid[x, y]=color;
                        NumberOfYellow++;
                        }
                              else if (JointTableProbability("orange", Distance)>=0.5  && grid[x,y]==null) {
                        Tile color =  Instantiate(Resources.Load("Prefabs/orange", typeof(Tile)), new Vector3(x, y, 0), Quaternion.identity) as Tile;
                        grid[x, y]=color;
                        NumberOfOranges++;
                        }
            }
        }
    }

   
}
