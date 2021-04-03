using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinder
{
    class Node
    {
        private string name;
        private int id;
        private int x;
        private int y;
        private List<int> adjList;
        private double estimatedDistance;
        private double distanceFromStart;
        private int parentID;
        private bool visited;

        public Node(string name, int id, int x, int y)
        {
            this.name = name;
            this.id = id;
            this.x = x;
            this.y = y;
            adjList = new List<int>();
            estimatedDistance = -1;
            distanceFromStart = -1;
            parentID = 0;
            visited = false;
        }

        public void insertAdjNode(int adjNodeID)
        {
            adjList.Add(adjNodeID);
        }

        public double GetStraightDistance(Node node)
        {
            double yDistance = this.GetY() - node.GetY();
            double xDistance = this.GetX() - node.GetX();

            return Math.Sqrt(Math.Pow(yDistance, 2) + Math.Pow(xDistance, 2));
        }

        //Getter
        public string GetName()
        {
            return name;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

        public int GetID()
        {
            return id;
        }

        public List<int> GetAdjList()
        {
            return adjList;
        }

        //Setter
        public void SetEstimatedDistance(double est)
        {
            estimatedDistance = est;
        }

        public void SetDistanceFromStart(double dist)
        {
            distanceFromStart = dist;
        }

        public void SetParentID(int Pid)
        {
            parentID = Pid;
        }

        public double GetEstimatedDistance()
        {
            return estimatedDistance;
        }

        public double GetDistanceFromStart()
        {
            return distanceFromStart;
        }

        public int GetParentID()
        {
            return parentID;
        }

        public bool GetVisited()
        {
            return visited;
        }

        public void Visit()
        {
            visited = true;
        }

        public void UnVisit()
        {
            visited = false;
        }
    }
}
