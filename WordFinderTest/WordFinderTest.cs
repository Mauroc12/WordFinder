using WordFinderSolution;

namespace WordFinderTest
{
    public class WordFinderTest
    {
        [Fact]
        public void should_return_matching_words()
        {
            // Arrange
         var matrix = new List<string>
        {
            "chicatbc",
            "coolefgh",
            "aoldidog",
            "tnopwind",
            "qrsbuvwo",
            "yzcatdeg",
            "ghitklmn",
            "batrsbat"
        };

            var wordFinder = new WordFinder(matrix);

            var wordStream = new List<string> { "cat", "dog", "bat", "pet" };

            // Act
            var result = wordFinder.Find(wordStream).ToList();

            // Assert
            Assert.Contains("cat", result);
            Assert.Contains("dog", result);
            Assert.Contains("bat", result);
            Assert.DoesNotContain("pet", result);
        }

    }
}