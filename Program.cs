namespace NetworkProgram;

class Program
{
    public static void Main()
    {
        Console.WriteLine("How many nodes exist in the graph?\nPlease use a number below 10:");
        int numberOfNodes = GetValidInt(1, 10);
        int[,] adjacencyMatrix = new int[numberOfNodes, numberOfNodes];
        
        Matrix.DisplayMatrix(adjacencyMatrix);
        
        Console.WriteLine("Because this is a simple program, i'll randomly assign the distances of the edges between the nodes");
        Matrix.AssignRandomDistances(adjacencyMatrix, numberOfNodes);
        
        Console.WriteLine("After assigning these distances, here is the current adjacency matrix:");
        Matrix.DisplayMatrix(adjacencyMatrix);
        
        Dijkstra.ApplyDijkstra(adjacencyMatrix);
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

// Dijkstra-related methods
class Dijkstra
{
    public static void ApplyDijkstra(int[,] adjacencyMatrix)
    {
        Console.WriteLine("We are applying Dijkstra's from a matrix; let us cook");
    }
}

class Prims
{
    public static void ApplyPrims(int[,] adjacencyMatrix)
    {
        Console.WriteLine("We are applying Prim's from a matrix; let us cook");
    }
}

class Kruskal
{
    public static void ApplyKruskal(int[,] adjacencyMatrix)
    {
        Console.WriteLine("We are applying Kruskal's from a matrix; let us cook");
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
                    adjacencyMatrix[i, j] = random.Next(1, 10);
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
