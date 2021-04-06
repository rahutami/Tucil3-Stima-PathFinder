using System;
using System.Collections.Generic;

namespace PathFinder
{
    class Node
    {
        private string name;
        private int id;
        private double latitude;
        private double longitude;
        private List<int> adjList;
        private double estimatedDistance;
        private double distanceFromStart;
        private int parentID;
        private bool visited;

        public Node(string name, int id, double latitude, double longitude)
        {
            this.name = name;
            this.id = id;
            this.latitude = latitude;
            this.longitude = longitude;
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

        // Menghitung jarak antara this dan node dengan menggunakan haversine function
        public double CalculateDistance(Node node)
        {
            double lat1 = latitude * Math.PI / 180;
            double lat2 = node.GetLatitude() * Math.PI / 180;

            double lonDistance = (longitude - node.GetLongitude()) * Math.PI / 180;
            double latDistance = (latitude - node.GetLatitude()) * Math.PI / 180;

            double a = (Math.Sin(latDistance / 2) * Math.Sin(latDistance / 2) +
                        Math.Cos(lat1) * Math.Cos(lat2) *
                        Math.Sin(lonDistance / 2) * Math.Sin(lonDistance / 2));

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return Math.Round(c * 6371 * 1000);
        }

        //Getter
        public string GetName()
        {
            return name;
        }

        public double GetLongitude()
        {
            return longitude;
        }

        public double GetLatitude()
        {
            return latitude;
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