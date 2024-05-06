using System.Diagnostics;

namespace NetworkProgram;

class Program
{
    public static void Main()
    {
        Console.WriteLine("How many nodes exist in the graph?\nPlease use a number 2 - 26 otherwise idk what to call the vertices:");
        int numberOfNodes = GetValidInt(2, 26);
        int[,] adjacencyMatrix = new int[numberOfNodes, numberOfNodes];
        
        Console.WriteLine("Because this is a simple program, i'll randomly assign the distances of the edges between the nodes");
        Matrix.AssignRandomDistances(adjacencyMatrix, numberOfNodes);
        
        Console.WriteLine("After assigning these distances, here is the current adjacency matrix:\n");
        Matrix.DisplayMatrix(adjacencyMatrix);
        
        int userMenuChoice = Menu();
        switch (userMenuChoice)
        {
            case 1:
                Algorithms.ApplyDijkstra(adjacencyMatrix, numberOfNodes);
                break;
            case 2:
                Algorithms.ApplyPrims(adjacencyMatrix);
                break;
            case 3:
                Algorithms.ApplyKruskal(adjacencyMatrix);
                break;
        }
        
    }

    public static int Menu()
    {
        
        Console.WriteLine("\nChoose an algorithm to apply:\n1 - Dijkstra's\n2 - Prim's\n3 - Kruskal's\nEnter a number:");
        
        return GetValidInt(1, 3);
    }
    
    public static int GetValidInt(int min, int max)
    {
        if (int.TryParse(Console.ReadLine(), out int validInt))
        {
            if (validInt >= min && validInt <= max)
            {
                return validInt;
            }
            Console.WriteLine($"No bro enter a number between {min} and {max}");
            return GetValidInt(min, max);
        }
        Console.WriteLine("Please enter a valid integer");
        return GetValidInt(min, max);
    }
}

// SP and MST related methods
class Algorithms
{
    public static void ApplyDijkstra(int[,] adjacencyMatrix, int numberOfNodes)
    {
        Console.WriteLine("\nWe are applying Dijkstra's from a matrix; let us cook");
        
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        
        Thread.Sleep(69);
        
        /*
         * algorithm steps:
         * 
         * create an empty list of permanent vertices
         * create a list of non-permanent vertices
         * starting at vertex 0 (A) --> tk must convert between letters 0123 and ABCD for vertices
         * 
         * current vertex = starting vertex
         * while the list of non-permanent vertices is not empty / permanent vertices list is empty:
         *     for every vertex in the network
         *         if it is connected to the current vertex
         *            set the temporary distance label of the vertex to the distance between it and the current vertex
         *     then find the vertex with the smallest temporary distance label
         *     make it permanent and store it in the permanent vertices list, remove it from the non-permanent list
         *     set the current vertex to the newly added permanent vertex
         *     [repeat]
         */
        List<Vertex> PermanentVerticesList = new List<Vertex>();
        
        List<Vertex> NonPermanentVerticesList = new List<Vertex>();
        for (int i = 0; i < numberOfNodes; i++)
        {
            NonPermanentVerticesList.Add(new Vertex(i, i,int.MaxValue, int.MaxValue));
        }
        
        /* to start at any vertex
         * Console.WriteLine("Which vertex do you want to start at? 0 to {numberOfNodes - 1}:\n");
           int startVertex = Program.GetValidInt(0, numberOfNodes - 1);
         */
        
        Console.WriteLine("Starting at vertex 0\nThis is vertex A in the adjacency matrix");
        int startVertex = 0;
        
        NonPermanentVerticesList[startVertex].StopNumber = 0;
        NonPermanentVerticesList[startVertex].PermanentDistanceLabel = 0;
        NonPermanentVerticesList[startVertex].TemporaryDistanceLabel = 0;
        NonPermanentVerticesList[startVertex].PermanentlyAdded = true;

        
        for (int i = 0; i <= NonPermanentVerticesList.Count - 1; i++)
            // for some reason the condition cannot be NonPermanentVerticesList.Count + 1
        {
            if (i == startVertex)
            {
                Console.WriteLine($"Vertex {i} is the start vertex");
            }
            else if (adjacencyMatrix[startVertex, i] != 0)
            {
                NonPermanentVerticesList[i].TemporaryDistanceLabel = adjacencyMatrix[startVertex, i];
                Console.WriteLine($"Vertex {i} is connected to vertex {startVertex} with a distance of {adjacencyMatrix[startVertex, i]}");
            }
            else
            {
                Console.WriteLine($"Vertex {i} is not connected to vertex {startVertex}");
            }
        }
    
        // find the vertex with the smallest temporary distance label, and make it permanent
    
        int smallestValue = int.MaxValue;
        int smallestVertex = 0;
    
        foreach (Vertex vertex in NonPermanentVerticesList)
        {
            if (vertex.PermanentlyAdded == false && vertex.TemporaryDistanceLabel < smallestValue)
            {
                smallestValue = vertex.TemporaryDistanceLabel;
                smallestVertex = vertex.VertexNumber;
            }
        }
    
        NonPermanentVerticesList[smallestVertex].PermanentDistanceLabel = NonPermanentVerticesList[smallestVertex].TemporaryDistanceLabel;
        NonPermanentVerticesList[smallestVertex].PermanentlyAdded = true;
        PermanentVerticesList.Add(NonPermanentVerticesList[smallestVertex]);
        NonPermanentVerticesList.RemoveAt(smallestVertex);
        
        
        stopwatch.Stop();
        Console.WriteLine($"Applying Dijkstra's took {stopwatch.ElapsedMilliseconds}ms for this matrix of {numberOfNodes} nodes.");
        
    }
    
    public static void ApplyPrims(int[,] adjacencyMatrix)
    {
        Console.Clear();
        Console.WriteLine("\nWe are applying Prim's from a matrix; let us cook");
        
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        
        int numberOfNodes = adjacencyMatrix.GetLength(0);
        
        stopwatch.Stop();
        Console.WriteLine($"Applying Prim's took {stopwatch.ElapsedMilliseconds}ms for this matrix of {numberOfNodes} nodes.");
    }
    
    public static void ApplyKruskal(int[,] adjacencyMatrix)
    {
        Console.Clear();
        Console.WriteLine("\nWe are applying Kruskal's from a matrix; let us cook");
        
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        
        int numberOfNodes = adjacencyMatrix.GetLength(0);
        
        stopwatch.Stop();
        Console.WriteLine($"Applying Kruskal's took {stopwatch.ElapsedMilliseconds}ms for this matrix of {numberOfNodes} nodes.");
    }
}

// Vertex-related methods
class Vertex
{
    public int VertexNumber;
    public int StopNumber;
    public int PermanentDistanceLabel;
    public int TemporaryDistanceLabel;
    public bool PermanentlyAdded;
    
    public Vertex(int stopNumber, int vertexNumber, int permanentDistanceLabel, int temporaryDistanceLabel)
    {
        VertexNumber = vertexNumber;
        StopNumber = stopNumber;
        PermanentDistanceLabel = permanentDistanceLabel;
        TemporaryDistanceLabel = temporaryDistanceLabel;
        PermanentlyAdded = false;
    }
}

// Matrix-related methods
class Matrix
{
    public static void AssignRandomDistances(int[,] adjacencyMatrix, int numberOfNodes)
    {
        Random random = new Random();
        
        for (int i = 0; i < numberOfNodes; i++)
        {
            for (int j = 0; j < numberOfNodes; j++)
            {
                if (i == j) // leading diagonal of 0s
                {
                    adjacencyMatrix[i, j] = 0;
                }
                else if (i < j) // above the diagonal
                {
                    adjacencyMatrix[i, j] = random.Next(0, 5);
                }
                else // below the diagonal is the mirror of the above
                {
                    adjacencyMatrix[i, j] = adjacencyMatrix[j, i];
                }
            }
        }
    }
    
    public static void DisplayMatrix(int[,] matrix)
    {
        string vertices = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        Console.Write("  ");
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            Console.Write(" " + vertices[i] + "");
        }
        Console.WriteLine();

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            Console.Write(vertices[i] + "| ");
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
