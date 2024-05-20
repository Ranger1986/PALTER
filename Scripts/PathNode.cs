using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    PathNode[,] grid;

    public int x,y;

    public int gCost, hCost, fCost;
    public bool isWalkable;
    public PathNode cameFromNode;

    public PathNode(PathNode[,] grid, int x, int y){
        this.grid=grid;
        this.x=x;
        this.y=y;
        cameFromNode=null;
        isWalkable=true;
    }
    public void CalculateFCost(){
        fCost=gCost+hCost;
    }
    public override string ToString()
    {
        return x+","+y;
    }
}
