using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DataFlowTest
{
    public class TestTrnsformMany
    {
        public static void TestTransformany()
        {
            var batchBlock = new BatchBlock<int>(10);

            // Post several values to the block.
            for (int i = 0; i < 13; i++)
            {
                batchBlock.Post(i);
            }
            // Set the block to the completed state. This causes
            // the block to propagate out any any remaining
            // values as a final batch.
            //batchBlock.Complete();

            // Print the sum of both batches.

            Console.WriteLine("The sum of the elements in batch 1 is {0}.",
                batchBlock.Receive().Sum());

            Console.WriteLine("The sum of the elements in batch 2 is {0}.",
                batchBlock.Receive().Sum());
        }
    }
}
