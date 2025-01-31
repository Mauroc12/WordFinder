using System.Runtime.InteropServices;

namespace WordFinderSolution
{
    public class WordFinder
    {
        private readonly List<ReadOnlyMemory<char>> rows;
        private readonly List<ReadOnlyMemory<char>> columns;

        public WordFinder(IEnumerable<string> matrix)
        {
            rows = matrix.Select(row => row.AsMemory()).ToList();
            columns = GetColuumnsAsRows(rows);
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            var wordStreamHashabled = new HashSet<string>(wordstream);
            var wordsCounter = new Dictionary<string, int>();

            foreach (var word in wordStreamHashabled)
            {
                bool wordFounded = SearchWord(word);

                if (wordFounded)
                {
                    ref int count = ref CollectionsMarshal.GetValueRefOrAddDefault(wordsCounter, word, out bool exists);
                    if (!exists)
                        count = 1;
                }
            }

            return wordsCounter.OrderByDescending(x => x.Value).Take(10).Select(x => x.Key);
        }

        private List<ReadOnlyMemory<char>> GetColuumnsAsRows(List<ReadOnlyMemory<char>> matrix)
        {
            int cols = matrix[0].Length;
            var rows = new List<ReadOnlyMemory<char>>(cols);

            for (int c = 0; c < cols; c++)
            {
                char[] column = new char[matrix.Count];
                for (int r = 0; r < matrix.Count; r++)
                {
                    column[r] = matrix[r].Span[c];
                }
                rows.Add(column.AsMemory());
            }

            return rows;
        }


        private bool SearchWord(string word)
        {
            ReadOnlySpan<char> wordSpan = word.AsSpan();
            bool found = false; 

            foreach (var row in rows)
            {
                if (row.Span.Contains(wordSpan, StringComparison.Ordinal))
                {
                    found = true;
                    break;
                }
            } 

            if (!found)
            {
                foreach (var col in columns)
                {
                    if (col.Span.Contains(wordSpan, StringComparison.Ordinal))
                    {
                        found = true;
                        break;
                    }
                }
            }


            return found;
        }
    }
}
