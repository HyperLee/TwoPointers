using TwoPointers.SlidingWindow;

namespace TwoPointers.Tests.SlidingWindow;

public class LongestSubstringWithoutRepeating_3_Tests
{
    [Theory]
    [InlineData("abcabcbb", 3)]
    [InlineData("bbbbb", 1)]
    [InlineData("pwwkew", 3)]
    [InlineData("", 0)]
    [InlineData(" ", 1)]
    [InlineData("dvdf", 3)]
    [InlineData("abba", 2)]
    public void Solve_VariousInputs_ReturnsLongestLength(string s, int expected)
    {
        Assert.Equal(expected, LongestSubstringWithoutRepeating.Solve(s));
    }

    [Fact]
    public void Solve_AllUnique_ReturnsFullLength()
    {
        Assert.Equal(6, LongestSubstringWithoutRepeating.Solve("abcdef"));
    }

    [Fact]
    public void Solve_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => LongestSubstringWithoutRepeating.Solve(null!));
    }
}
