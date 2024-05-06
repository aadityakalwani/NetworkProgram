namespace NetworkProgram;

class Program
{

    public static void Main()
    {
        int[,] adjacencyMatrix = new int[3, 3];
        // 3x3 matrix for now, can be changed later
        // take an input for dimensions of the matrix / number of nodes etc. and then set up a 2d array as required later
        
        Console.WriteLine("Here is the current adjacency matrix:");
        DisplayMatrix(adjacencyMatrix);
        
        Console.WriteLine("Enter your choice:\n1 - apply Dijkstra\n2 - apply Prim's\n3 - apply Kruskal's\n");
        int userMenuOption = GetValidInt(1, 9999999);

        switch (userMenuOption) 
        {
            case 1:
                ApplyDijkstra(adjacencyMatrix);
            
                break;
            case 2:
                ApplyPrims(adjacencyMatrix);
            
                break;
            case 3:
                ApplyKruskal(adjacencyMatrix);
            
                break;
        }
    }

    public static void ApplyDijkstra(int[,] adjacencyMatrix)
    {
        Console.WriteLine("We are applying Dijkstra; let us cook");
    }

    public static void ApplyPrims(int[,] adjacencyMatrix)
    {
        Console.WriteLine("doing Prim's");
    }

    public static void ApplyKruskal(int[,] adjacencyMatrix)
    {
        Console.WriteLine("doing Kruskal's");
        }
    
    public static void DisplayMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
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
