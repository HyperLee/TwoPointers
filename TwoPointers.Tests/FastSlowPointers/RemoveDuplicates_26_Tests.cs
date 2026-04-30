using TwoPointers.FastSlowPointers;

namespace TwoPointers.Tests.FastSlowPointers;

public class RemoveDuplicates_26_Tests
{
    [Theory]
    [InlineData(new[] { 1, 1, 2 }, 2, new[] { 1, 2 })]
    [InlineData(new[] { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 }, 5, new[] { 0, 1, 2, 3, 4 })]
    [InlineData(new[] { 1, 2, 3 }, 3, new[] { 1, 2, 3 })]
    public void Solve_VariousInputs_ReturnsLengthAndOverwritesPrefix(int[] nums, int expectedK, int[] expectedPrefix)
    {
        int k = RemoveDuplicates.Solve(nums);
        Assert.Equal(expectedK, k);
        Assert.Equal(expectedPrefix, nums.Take(k));
    }

    [Fact]
    public void Solve_AllSameElements_ReturnsOne()
    {
        int[] nums = [7, 7, 7, 7];
        int k = RemoveDuplicates.Solve(nums);
        Assert.Equal(1, k);
        Assert.Equal(7, nums[0]);
    }

    [Fact]
    public void Solve_EmptyArray_ReturnsZero()
    {
        Assert.Equal(0, RemoveDuplicates.Solve([]));
    }

    [Fact]
    public void Solve_SingleElement_ReturnsOne()
    {
        Assert.Equal(1, RemoveDuplicates.Solve([5]));
    }

    [Fact]
    public void Solve_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => RemoveDuplicates.Solve(null!));
    }
}
