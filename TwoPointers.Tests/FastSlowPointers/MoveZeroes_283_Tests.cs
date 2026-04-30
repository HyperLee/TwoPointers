using TwoPointers.FastSlowPointers;

namespace TwoPointers.Tests.FastSlowPointers;

public class MoveZeroes_283_Tests
{
    [Theory]
    [InlineData(new[] { 0, 1, 0, 3, 12 }, new[] { 1, 3, 12, 0, 0 })]
    [InlineData(new[] { 0 }, new[] { 0 })]
    [InlineData(new[] { 1, 2, 3 }, new[] { 1, 2, 3 })]
    [InlineData(new[] { 0, 0, 0, 1 }, new[] { 1, 0, 0, 0 })]
    [InlineData(new[] { 1, 0, 0, 0 }, new[] { 1, 0, 0, 0 })]
    public void Solve_VariousInputs_MovesZeroesToEnd(int[] input, int[] expected)
    {
        MoveZeroes.Solve(input);
        Assert.Equal(expected, input);
    }

    [Fact]
    public void Solve_AllZeros_RemainsAllZeros()
    {
        int[] nums = [0, 0, 0];
        MoveZeroes.Solve(nums);
        Assert.Equal(new[] { 0, 0, 0 }, nums);
    }

    [Fact]
    public void Solve_EmptyArray_NoException()
    {
        int[] nums = [];
        MoveZeroes.Solve(nums);
        Assert.Empty(nums);
    }

    [Fact]
    public void Solve_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => MoveZeroes.Solve(null!));
    }
}
