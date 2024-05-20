using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Pathfinding
{
    public PathNode[,] grid;
    List<PathNode> openList;
    List<PathNode> closedList;
    int width, height;
    public bool withObstacles;

    public Pathfinding(int width, int height, bool withObstacles){
        grid = new PathNode[width,height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x,y]=new PathNode(grid,x,y);
            }
        }
        this.width=width;
        this.height=height;
        this.withObstacles=withObstacles;
    }
    public List<PathNode> FindPath(int startX, int startY, int endX, int endY){
        if (openList!=null&&closedList!=null)
        {
//            Debug.Log("LA");
            openList.Clear();
            closedList.Clear();
        }
        openList= new List<PathNode> {grid[startX,startY]};
        closedList = new List<PathNode>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x,y].gCost = int.MaxValue;
                grid[x,y].CalculateFCost();
            }
        }
        grid[startX,startY].gCost =0;
        grid[startX,startY].hCost =CalculateDistanceCost(grid[startX,startY],grid[endX,endY]);
        grid[startX,startY].CalculateFCost();
        while (openList.Count>0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode==grid[endX,endY])
            {
                return CalculatePath(grid[endX,endY]);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode))continue;
                if(withObstacles&&!neighbourNode.isWalkable){
                    closedList.Add(neighbourNode);
                    continue;
                }
                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode=currentNode;
                    neighbourNode.gCost=tentativeGCost;
                    neighbourNode.hCost=CalculateDistanceCost(neighbourNode,grid[endX,endY]);
                    neighbourNode.CalculateFCost();
                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }
        return null;
    }
    private List<PathNode> GetNeighbourList(PathNode currentNode){
        List<PathNode> neighbourList = new List<PathNode>();
        if (currentNode.x -1 >=0)
        {
            neighbourList.Add(grid[currentNode.x-1,currentNode.y]);
            /*
            if (currentNode.y-1>=0)
            {
                neighbourList.Add(grid[currentNode.x-1,currentNode.y-1]);
            }
            if (currentNode.y+1<height)
            {
                neighbourList.Add(grid[currentNode.x-1,currentNode.y+1]);
            }
            */
        }
        if (currentNode.x + 1 <width)
        {
            neighbourList.Add(grid[currentNode.x+1,currentNode.y]);
            /*
            if (currentNode.y-1>=0)
            {
                neighbourList.Add(grid[currentNode.x+1,currentNode.y-1]);
            }
            if (currentNode.y+1<height)
            {
                neighbourList.Add(grid[currentNode.x+1,currentNode.y+1]);
            }
            */
        }
        if (currentNode.y-1>=0)
        {
            neighbourList.Add(grid[currentNode.x,currentNode.y-1]);
        }
        if (currentNode.y+1<height)
        {
            neighbourList.Add(grid[currentNode.x,currentNode.y+1]);
        }
        return neighbourList;
    }
    private List<PathNode> CalculatePath(PathNode endNode){
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode=currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }
    private int CalculateDistanceCost(PathNode a, PathNode b){
        int yDistance =Mathf.Abs(a.y-b.y);
        int xDistance =Mathf.Abs(a.x-b.x);
        int remaining = Mathf.Abs(xDistance-yDistance);
        return 14 * Mathf.Min(xDistance,yDistance)+10 * remaining;
    }
    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList){
        PathNode LowestFCostNode = pathNodeList[0];
        for (int i = 0; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < LowestFCostNode.fCost)
            {
                LowestFCostNode = pathNodeList[i];                
            }
        }
        return LowestFCostNode;
    }
}