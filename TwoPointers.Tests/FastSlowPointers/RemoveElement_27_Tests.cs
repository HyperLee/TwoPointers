using TwoPointers.FastSlowPointers;

namespace TwoPointers.Tests.FastSlowPointers;

public class RemoveElement_27_Tests
{
    [Theory]
    [InlineData(new[] { 3, 2, 2, 3 }, 3, 2, new[] { 2, 2 })]
    [InlineData(new[] { 0, 1, 2, 2, 3, 0, 4, 2 }, 2, 5, new[] { 0, 1, 3, 0, 4 })]
    [InlineData(new[] { 1, 1, 1 }, 1, 0, new int[] { })]
    public void Solve_VariousInputs_ReturnsCountAndOverwritesPrefix(int[] nums, int val, int expectedK, int[] expectedPrefix)
    {
        int k = RemoveElement.Solve(nums, val);
        Assert.Equal(expectedK, k);
        Assert.Equal(expectedPrefix, nums.Take(k));
    }

    [Fact]
    public void Solve_NoMatch_KeepsAllElements()
    {
        int[] nums = [1, 2, 3];
        int k = RemoveElement.Solve(nums, 99);
        Assert.Equal(3, k);
        Assert.Equal(new[] { 1, 2, 3 }, nums);
    }

    [Fact]
    public void Solve_EmptyArray_ReturnsZero()
    {
        Assert.Equal(0, RemoveElement.Solve([], 0));
    }

    [Fact]
    public void Solve_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => RemoveElement.Solve(null!, 0));
    }
}
