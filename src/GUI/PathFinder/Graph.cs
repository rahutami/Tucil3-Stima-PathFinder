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
                double latitude, longitude;
                string name = "";

                // format per line latitude longitude Nama
                string[] identity = graphFile.ReadLine().Split(" ");
                latitude = Double.Parse(identity[0], System.Globalization.CultureInfo.InvariantCulture);
                longitude = Double.Parse(identity[1], System.Globalization.CultureInfo.InvariantCulture);

                // Double.TryParse(identity[0], out latitude);
                // Double.TryParse(identity[1], out longitude);
                List<string> list = new List<string>();
                
                
                for (int j = 2; j < identity.Length; j++)
                {
                    name += identity[j];
                    list.Add(name);
                    string[] str = list.ToArray();

                    if (j != identity.Length - 1) name += " ";
                }

                Node node = new Node(name, i, latitude, longitude);
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
            graphFile.Close();
        }

        public List<string> GetNodeNames()
        {
            List<string> names = new List<string>();

            foreach (Node node in nodes)
            {
                names.Add(node.GetName());
            }

            return names;
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

        public List<Node> GetNodes()
        {
            return nodes;
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
