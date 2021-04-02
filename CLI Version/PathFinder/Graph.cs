﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PathFinder
{
    class Graph
    {
        private List<Node> nodes;
        private List<List<bool>> adjMatrix;

        public Graph()
        {
            nodes = new List<Node>();
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
                if (node.GetName() == name) return node;
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
        public static Graph CreateGraph(string path)
        {
            Graph graph = new Graph();
            StreamReader graphFile = new StreamReader(path);
            int n;
            Int32.TryParse(graphFile.ReadLine(), out n);
            for(int i = 1; i <= n; i++)
            {
                int x, y;
                string name;

                // format per line X Y Nama
                string[] identity = graphFile.ReadLine().Split(" ");
                Int32.TryParse(identity[0], out x);
                Int32.TryParse(identity[1], out y);
                name = identity[2];

                Node node = new Node(name, i, x, y);
                graph.InsertNode(node);
            }

            List<List<bool>> adjMatrix = new List<List<bool>>();

            for (int i = 0; i < n; i++)
            {
                string[] adjNode = graphFile.ReadLine().Split(" ");
                List<bool> adjMatrixRow = new List<bool>();

                for (int j = 0; j < n; j++)
                {
                    if(adjNode[j] == "1")
                    {
                        graph.GetNode(i + 1).insertAdjNode(j + 1);
                        adjMatrixRow.Add(true);
                    }
                    else
                    {
                        adjMatrixRow.Add(false);
                    }
                }
                adjMatrix.Add(adjMatrixRow);
            }

            graph.InsertAdjMatrix(adjMatrix);

            return graph;
        }
    }
}