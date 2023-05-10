using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DataFlowTest
{
    public class DataflowPipeline
    {
        public static void CreatePipeLine()
        {
            //
            // Create the members of the pipeline.
            // 

            // Downloads the requested resource as a string.
            var downloadString = new TransformBlock<string, string>(async uri =>
            {
                var handler = new HttpClientHandler();
                handler.AutomaticDecompression = DecompressionMethods.GZip;
                var cient = new HttpClient(handler);
                Console.WriteLine("Downloading '{0}'...", uri);
                var text = cient.GetAsync(uri);
                var content = await text.Result.Content.ReadAsStreamAsync();
                var stringReader = new StreamReader(content).ReadToEnd();
                //byte[] buffer = new Byte[] { };
                //var content1 = string.Empty;
                //var offset = 0;
                //var size = 60000;
                //var stream = content.Read(buffer, offset, size);
                //while (buffer.Any())
                //{
                //    offset = size;
                //    content1 = Convert.ToBase64String(buffer);
                //    content.Read(buffer, offset, size);
                //}

                return stringReader.ToString();
            });

            // Separates the specified text into an array of words.
            var createWordList = new TransformBlock<string, string[]>(text =>
            {
                Console.WriteLine("Creating word list...");

                // Remove common punctuation by replacing all non-letter characters 
                // with a space character.
                char[] tokens = text.Select(c => char.IsLetter(c) ? c : ' ').ToArray();
                text = new string(tokens);

                // Separate the text into an array of words.
                return text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            });

            // Removes short words and duplicates.
            var filterWordList = new TransformBlock<string[], string[]>(words =>
            {
                Console.WriteLine("Filtering word list...");

                return words
                    .Where(word => word.Length > 3)
                    .Distinct()
                    .ToArray();
            });

            // Finds all words in the specified collection whose reverse also 
            // exists in the collection.
            var findReversedWords = new TransformManyBlock<string[], string>(words =>
            {
                Console.WriteLine("Finding reversed words...");

                var wordsSet = new HashSet<string>(words);

                return from word in words.AsParallel()
                    let reverse = new string(word.Reverse().ToArray())
                    where word != reverse && wordsSet.Contains(reverse)
                    select word;
            });

            // Prints the provided reversed words to the console.    
            var printReversedWords = new ActionBlock<string>(reversedWord =>
            {
                Console.WriteLine("Found reversed words {0}/{1}",
                    reversedWord, new string(reversedWord.Reverse().ToArray()));
            });

            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };

            downloadString.LinkTo(createWordList, linkOptions);
            createWordList.LinkTo(filterWordList, linkOptions);
            filterWordList.LinkTo(findReversedWords, linkOptions);
            findReversedWords.LinkTo(printReversedWords, linkOptions);
            downloadString.Post("http://www.gutenberg.org/cache/epub/16452/pg16452.txt");
            downloadString.Complete();
            downloadString.Completion.Wait();
        }
    }
}
