using System;
using System.Linq;

class Program
{
    // Maximum between two numbers
    static double max(double x, double y)
    {
        return (x > y) ? x : y;
    }

    // Calculates the maximum earning by mining transactions.
    static double calcMaxEarningByTransactions(int blockSize, int[] transactionSize, double[] fees, int noOfTransaction)
    {
        int index, size;
        double[,] maxEarning = new double[noOfTransaction + 1, blockSize + 1];

        // Build maxEarning[][] 
        for (index = 0; index <= noOfTransaction; index++)
        {
            for (size = 0; size <= blockSize; size++)
            {
                if (index == 0 || size == 0)
                {
                    maxEarning[index, size] = 0;
                }
                else if (transactionSize[index - 1] <= size)
                {
                    maxEarning[index, size] = max(fees[index - 1] + maxEarning[index - 1, size - transactionSize[index - 1]], maxEarning[index - 1, size]);
                }
                else
                {
                    maxEarning[index, size] = maxEarning[index - 1, size];
                }
            }
        }

        return maxEarning[noOfTransaction, blockSize];

    }

    public static void Main()
    {
        // Size of Transaction(Bytes)
        int[] transactionSize = new int[] { 57247, 98732, 134928, 77275, 29240, 15440, 70820, 139603, 63718, 143807, 190457, 40572 };

        // Fees corresponding to size of transaction(Bytes)
        double[] fees = new double[] { 0.0887, 0.1856, 0.2307, 0.1522, 0.0532, 0.0250, 0.1409, 0.2541, 0.1147, 0.2660, 0.2933, 0.0686 };

        // Block size(Bytes)
        int blockSize = 1000000;

        // BitCoin Reward for 1 Block.
        double reward = 12.5;

        // Maximum Earning on mining transactions. 
        double maxEarningByTransaction = calcMaxEarningByTransactions(blockSize, transactionSize, fees, fees.Length);

        // Maximum Earning on mining transactions including reward.
        double maxEarning = (transactionSize.Sum() >= blockSize) ? (reward + maxEarningByTransaction) : maxEarningByTransaction;

        Console.WriteLine(maxEarning);
    }
}
