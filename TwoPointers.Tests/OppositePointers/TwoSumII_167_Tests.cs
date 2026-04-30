using TwoPointers.OppositePointers;

namespace TwoPointers.Tests.OppositePointers;

public class TwoSumII_167_Tests
{
    [Theory]
    [InlineData(new[] { 2, 7, 11, 15 }, 9, new[] { 1, 2 })]
    [InlineData(new[] { 2, 3, 4 }, 6, new[] { 1, 3 })]
    [InlineData(new[] { -1, 0 }, -1, new[] { 1, 2 })]
    public void Solve_TargetExists_ReturnsOneBasedIndices(int[] numbers, int target, int[] expected)
    {
        Assert.Equal(expected, TwoSumII.Solve(numbers, target));
    }

    [Fact]
    public void Solve_NegativeAndPositiveBoundary_ReturnsExpected()
    {
        Assert.Equal(new[] { 1, 5 }, TwoSumII.Solve([-3, -1, 0, 2, 5], 2));
    }

    [Fact]
    public void Solve_NoValidPair_Throws()
    {
        Assert.Throws<InvalidOperationException>(() => TwoSumII.Solve([1, 2, 3], 100));
    }

    [Fact]
    public void Solve_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => TwoSumII.Solve(null!, 0));
    }

    [Fact]
    public void Solve_TooShortInput_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => TwoSumII.Solve([1], 1));
    }
}
