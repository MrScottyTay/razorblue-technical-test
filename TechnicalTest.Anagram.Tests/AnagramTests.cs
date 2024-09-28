namespace TechnicalTest.Anagram.Tests
{
    [TestClass]
    public class AnagramTests
    {
        [DataTestMethod]
        [DataRow("Clint Eastwood", "Old West Action", true)]
        [DataRow("Funeral", "Real fun", true)]
        [DataRow("The Morse Code", "Here come dots", true)]
        [DataRow("Rhythm", "Rhymer", false)]
        [DataRow("Glistening", "Negligible", false)]
        [DataRow("Word", "Phrase", false)]
        public void TestIsAnagram(string first, string second, bool expected)
        {
            bool actual = Anagram.IsAnagram(first, second);

            Assert.AreEqual(expected, actual);
        }
    }
}