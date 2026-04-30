using TwoPointers.OppositePointers;

namespace TwoPointers.Tests.OppositePointers;

public class TrappingRainWater_42_Tests
{
    [Theory]
    [InlineData(new[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 }, 6)]
    [InlineData(new[] { 4, 2, 0, 3, 2, 5 }, 9)]
    [InlineData(new[] { 2, 0, 2 }, 2)]
    [InlineData(new[] { 3, 0, 0, 2, 0, 4 }, 10)]
    public void Solve_VariousInputs_ReturnsTotalWater(int[] height, int expected)
    {
        Assert.Equal(expected, TrappingRainWater.Solve(height));
    }

    [Theory]
    [InlineData(new int[] { })]
    [InlineData(new[] { 5 })]
    [InlineData(new[] { 5, 5 })]
    [InlineData(new[] { 1, 2, 3, 4, 5 })]
    public void Solve_NoBasin_ReturnsZero(int[] height)
    {
        Assert.Equal(0, TrappingRainWater.Solve(height));
    }

    [Fact]
    public void Solve_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => TrappingRainWater.Solve(null!));
    }
}
