using System;
using System.Collections.Generic;
using System.IO;

namespace PathFinder
{
    class Graph
    {
        private List<Node> nodes;
        
        // Constructor
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

            for (int i = 0; i < n; i++)
            {
                string[] adjNode = graphFile.ReadLine().Split(" ");
                for (int j = 0; j < n; j++)
                {
                    if (adjNode[j] == "1")
                    {
                        GetNode(i + 1).insertAdjNode(j + 1);
                    }
                }
            }

            graphFile.Close();
        }

        // Mengembalikan list of nama-nama node yang ada di graf
        public List<string> GetNodeNames()
        {
            List<string> names = new List<string>();

            foreach (Node node in nodes)
            {
                names.Add(node.GetName());
            }

            return names;
        }

        // Menambahkan node ke dalam graf
        public void InsertNode(Node node)
        {
            nodes.Add(node);
        }

        // Mengembalikan node yang memiliki ID id
        public Node GetNode(int id)
        {
            foreach(Node node in nodes)
            {
                if (node.GetID() == id) return node;
            }
            return null;
        }

        // Mengembalikan node yang memiliki nama name
        public Node GetNode(string name)
        {
            foreach (Node node in nodes)
            {
                if (node.GetName().ToLower() == name.ToLower()) return node;
            }
            return null;
        }

        // Mengambil semua node yang ada di graf
        public List<Node> GetNodes()
        {
            return nodes;
        }

// Mereset distance dan status visited
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
