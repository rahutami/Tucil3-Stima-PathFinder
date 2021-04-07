using System.Collections.Generic;

namespace PathFinder
{
    class PathFinder
    {
        List<Node> path;
        double distance;
        public PathFinder(string start, string destination, Graph map)
        {
            Node startNode = map.GetNode(start);
            Node destNode = map.GetNode(destination);

            if (startNode == null || destNode == null)
            {
                path = new List<Node>();
                distance = -1;
                return;
            }

            Node currentNode = startNode;
            SortedQueue queue = new SortedQueue();

            currentNode.SetDistanceFromStart(0);
            currentNode.SetEstimatedDistance(currentNode.CalculateDistance(destNode));
            currentNode.Visit();

            while (currentNode != null)
            {
                currentNode.Visit();
                
                foreach (int adjID in currentNode.GetAdjList())
                {
                    Node adjNode = map.GetNode(adjID);

                    if (adjNode.CalculateDistance(currentNode) + adjNode.CalculateDistance(destNode) + currentNode.GetDistanceFromStart() < adjNode.GetEstimatedDistance() || adjNode.GetEstimatedDistance() == -1)
                    {
                        adjNode.SetParentID(currentNode.GetID());

                        adjNode.SetDistanceFromStart(adjNode.CalculateDistance(currentNode) + currentNode.GetDistanceFromStart());

                        adjNode.SetEstimatedDistance(adjNode.GetDistanceFromStart() + adjNode.CalculateDistance(destNode));
                    }

                    // Kalo node belom divisit dan node bukan destination maka dimasukkin ke queue
                    if (!adjNode.GetVisited() && !Equals(adjNode, destNode))
                    {
                        queue.Enqueue(adjNode);
                    }
                }
                currentNode = queue.Dequeue();
            }
            CreatePath(startNode, destNode, map);
            distance = destNode.GetDistanceFromStart();
            map.Clear();
        }

        public void CreatePath(Node start, Node destination, Graph map)
        {
            Node currentNode = destination;
            path = new List<Node>();

            if (destination.GetParentID() != 0)
            {
                do
                {
                    path.Add(currentNode);
                    currentNode = map.GetNode(currentNode.GetParentID());
                } while (!Equals(start, currentNode));
                path.Add(currentNode);
                path.Reverse();
            }
        }

        public List<Node> GetPath()
        {
            return path;
        }

        public double GetDistance()
        {
            return distance;
        }
    }
}