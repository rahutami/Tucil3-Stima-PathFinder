using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PathFinder
{
    class Graph
    {
        private List<Node> nodes;
        private List<List<bool>> adjMatrix;

        public Graph(string path)
        {
            nodes = new List<Node>();

            StreamReader graphFile = new StreamReader(path);
            int n;

            Int32.TryParse(graphFile.ReadLine(), out n);

            for (int i = 1; i <= n; i++)
            {
                double x, y;
                string name = "";

                // format per line X Y Nama
                string[] identity = graphFile.ReadLine().Split(" ");
                Double.TryParse(identity[0], out x);
                Double.TryParse(identity[1], out y);
                for(int j = 2; j < identity.Length; j++)
                {
                    name += identity[j];
                    if (j != identity.Length - 1) name += " ";
                }

                Node node = new Node(name, i, x, y);
                InsertNode(node);
            }

            List<List<bool>> adjMatrix = new List<List<bool>>();

            for (int i = 0; i < n; i++)
            {
                string[] adjNode = graphFile.ReadLine().Split(" ");
                List<bool> adjMatrixRow = new List<bool>();

                for (int j = 0; j < n; j++)
                {
                    if (adjNode[j] == "1")
                    {
                        GetNode(i + 1).insertAdjNode(j + 1);
                        adjMatrixRow.Add(true);
                    }
                    else
                    {
                        adjMatrixRow.Add(false);
                    }
                }
                adjMatrix.Add(adjMatrixRow);
            }

            InsertAdjMatrix(adjMatrix);
        }

        public void InsertAdjMatrix(List<List<bool>> adjMatrix)
        {
            this.adjMatrix = new List<List<bool>>(adjMatrix);
        }

        public void InsertNode(Node node)
        {
            nodes.Add(node);
        }

        public Node GetNode(int id)
        {
            foreach(Node node in nodes)
            {
                if (node.GetID() == id) return node;
            }
            return null;
        }

        public Node GetNode(string name)
        {
            foreach (Node node in nodes)
            {
                if (node.GetName().ToLower() == name.ToLower()) return node;
            }
            return null;
        }

        public void PrintNodes()
        {
            foreach(Node node in nodes)
            {
                Console.Write(node.GetName() + " ");
                foreach(int adjID in node.GetAdjList())
                {
                    Console.Write(GetNode(adjID).GetName() + " ");
                }
                Console.WriteLine();
            }
        }

        public double GetStraightDistance(int id1, int id2)
        {
            Node node1 = GetNode(id1);
            Node node2 = GetNode(id2);

            double yDistance = node1.GetY() - node2.GetY();
            double xDistance = node1.GetX() - node2.GetX();

            return Math.Sqrt(Math.Pow(yDistance, 2) + Math.Pow(xDistance, 2));
        }

        public double GetStraightDistance(string name1, string name2)
        {
            Node node1 = GetNode(name1);
            Node node2 = GetNode(name2);

            double yDistance = node1.GetY() - node2.GetY();
            double xDistance = node1.GetX() - node2.GetX();

            return Math.Sqrt(Math.Pow(yDistance, 2) + Math.Pow(xDistance, 2));
        }

        public void Clear()
        {
            foreach(Node node in nodes)
            {
                node.SetDistanceFromStart(-1);
                node.SetEstimatedDistance(-1);
                node.SetParentID(0);
                node.UnVisit();
            }
        }
    }
}
