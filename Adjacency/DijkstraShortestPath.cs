using System;
namespace Adjacency
{
    public class DijkstraShortestPath
    {
        private string source;
        //private string destination;

        public DijkstraShortestPath()
        {

        }

        int numVertices = 63;

        public WeightedEdge[] DijkstraMethod(string source, string destination, WeightedEdge[] adjacencyList, WeightedEdge[] edges, int[] distTo)
        {
            

            for (int i = 0; i < numVertices; i++)
            {
                distTo[i] = int.MaxValue;
            }

            PriorityQueue priqueue = new PriorityQueue(numVertices);

            priqueue.Enqueue(new Pair(0, source));

            while (!priqueue.IsEmpty())
            {
                Pair current = priqueue.Dequeue();
                string vertex = current.Name;
                int newDist = current.Distance;

                for (int i = 0; i < numVertices; i++)
                {
                    if (adjacencyList[i].Source == vertex)
                    {
                        WeightedEdge currentEdge = adjacencyList[i];

                        while (currentEdge != null)
                        {
                            string adjVertex = currentEdge.Destination;
                            int weight = currentEdge.Weight;
                            bool edgeFound = false;

                            for (int j = 0; j < numVertices; j++)
                            {
                                if (edges[j] != null && edges[j].Destination == adjVertex)
                                {
                                    edgeFound = true;
                                    if (distTo[j] > newDist + weight)
                                    {
                                        distTo[j] = newDist + weight;
                                        edges[j] = currentEdge;
                                        priqueue.Enqueue(new Pair(distTo[j], adjVertex));
                                    }
                                }
                            }

                            if (!edgeFound)
                            {
                                for (int j = 0; j < numVertices; j++)
                                {
                                    if (edges[j] == null)
                                    {
                                        edges[j] = currentEdge;
                                        distTo[j] = newDist + weight;
                                        priqueue.Enqueue(new Pair(distTo[j], adjVertex));
                                        break;
                                    }
                                }
                            }

                            currentEdge = currentEdge.Next;
                        }

                        break;
                    }
                }
            }
            
            //Display shortest path
            WeightedEdge[] pathArray = new WeightedEdge[63];

            WeightedEdge currentPathEdge;
            int totalTime = 0;
            int count = 0;
            bool cont = false;

            while (cont == false)
            {
                //Loop through edges array to find edge with destination
                for (int i = 0; i < numVertices; i++)
                {
                    if (edges[i] != null && edges[i].Destination == destination)
                    {
                        pathArray[count] = edges[i];
                        destination = edges[i].Source;
                        if (edges[i].Source == source)
                        {
                            cont = true;
                        }

                        break;
                    }
                }

                count++;
            }

            int routeCount = 1;

            Console.WriteLine("Start:");
            for (int i = count - 1; i > -1; i--)
            {
                currentPathEdge = pathArray[i];
                Console.WriteLine("(" + routeCount + ")" + " " + "From: " + currentPathEdge.Source + "    to " + currentPathEdge.Destination + "    via " + currentPathEdge.Line + "    Time in minutes: " + currentPathEdge.Weight);
                totalTime = totalTime + currentPathEdge.Weight;
                routeCount++;
            }
            Console.WriteLine("Total time = " + totalTime + " minutes");
            return edges;
        }
    }
}

