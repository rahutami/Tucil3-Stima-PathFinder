using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinder
{
    class PathFinder
    {
        public static List<Node> FindShortestPath(string start, string destination, Graph map)
        {
            Node startNode = map.GetNode(start);
            Node destNode = map.GetNode(destination);
            Node currentNode = startNode;
            SortedQueue queue = new SortedQueue();
            currentNode.SetDistanceFromStart(0);
            currentNode.SetEstimatedDistance(map.GetStraightDistance(destNode.GetID(), currentNode.GetID()));
            currentNode.Visit();
            while (!queue.IsEmpty() || Equals(currentNode, startNode))
            {
                foreach (int adjID in currentNode.GetAdjList())
                {
                    Node adjNode = map.GetNode(adjID);

                    if(adjNode.GetStraightDistance(currentNode) + adjNode.GetStraightDistance(destNode) + currentNode.GetDistanceFromStart() < adjNode.GetEstimatedDistance() || adjNode.GetEstimatedDistance() == -1)
                    {
                        adjNode.SetParentID(currentNode.GetID());

                        adjNode.SetDistanceFromStart(adjNode.GetStraightDistance(currentNode) + currentNode.GetDistanceFromStart());

                        adjNode.SetEstimatedDistance(adjNode.GetDistanceFromStart() + adjNode.GetStraightDistance(destNode));
                    }

                    // Kalo node belom divisit dan node bukan destination maka dimasukkin ke queue
                    if (!adjNode.GetVisited() && !Equals(adjNode, destNode))
                    {
                        queue.Enqueue(adjNode);
                    }
                }
                currentNode = queue.Dequeue();
                currentNode.Visit();
;            }
            List<Node> path = GetPath(startNode, destNode, map);
            return path;
        }

        public static List<Node> GetPath(Node start, Node destination, Graph map)
        {
            Node currentNode = destination;
            List<Node> path = new List<Node>();
            do
            {
                path.Add(currentNode);
                currentNode = map.GetNode(currentNode.GetParentID());
            } while (!Equals(start, currentNode));
            path.Add(currentNode);
            path.Reverse();
            return path;
        }

        public static void PrintPath(List<Node> path)
        {
            foreach(Node node in path)
            {
                Console.WriteLine(node.GetName());
            }
        }
    }
}
