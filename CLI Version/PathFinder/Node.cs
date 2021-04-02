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

        public Node(string name, int id, int x, int y)
        {
            this.name = name;
            this.id = id;
            this.x = x;
            this.y = y;
            adjList = new List<int>();
        }

        public void insertAdjNode(int adjNodeID)
        {
            adjList.Add(adjNodeID);
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
    }
}
