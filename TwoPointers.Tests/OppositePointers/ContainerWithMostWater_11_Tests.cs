using TwoPointers.OppositePointers;

namespace TwoPointers.Tests.OppositePointers;

public class ContainerWithMostWater_11_Tests
{
    [Theory]
    [InlineData(new[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 }, 49)]
    [InlineData(new[] { 1, 1 }, 1)]
    [InlineData(new[] { 4, 3, 2, 1, 4 }, 16)]
    [InlineData(new[] { 1, 2, 1 }, 2)]
    public void Solve_VariousInputs_ReturnsMaxArea(int[] height, int expected)
    {
        Assert.Equal(expected, ContainerWithMostWater.Solve(height));
    }

    [Fact]
    public void Solve_AscendingOnly_PicksOuterEnds()
    {
        Assert.Equal(4, ContainerWithMostWater.Solve([1, 2, 3, 4]));
    }

    [Fact]
    public void Solve_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => ContainerWithMostWater.Solve(null!));
    }

    [Fact]
    public void Solve_TooShortInput_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => ContainerWithMostWater.Solve([1]));
    }
}
