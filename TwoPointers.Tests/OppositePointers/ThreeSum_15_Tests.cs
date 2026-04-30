using TwoPointers.OppositePointers;

namespace TwoPointers.Tests.OppositePointers;

public class ThreeSum_15_Tests
{
    private static HashSet<string> Normalize(IEnumerable<IList<int>> triplets)
    {
        return [.. triplets.Select(t =>
        {
            int[] arr = [.. t];
            Array.Sort(arr);
            return string.Join(",", arr);
        })];
    }

    [Fact]
    public void Solve_LeetCodeSample_ReturnsExpectedTriplets()
    {
        var result = ThreeSum.Solve([-1, 0, 1, 2, -1, -4]);
        var expected = Normalize([[-1, -1, 2], [-1, 0, 1]]);
        Assert.Equal(expected, Normalize(result));
    }

    [Fact]
    public void Solve_AllZeros_ReturnsSingleTriplet()
    {
        var result = ThreeSum.Solve([0, 0, 0, 0]);
        Assert.Single(result);
        Assert.Equal([0, 0, 0], result[0]);
    }

    [Fact]
    public void Solve_NoTriplet_ReturnsEmpty()
    {
        Assert.Empty(ThreeSum.Solve([1, 2, -2, -1]));
    }

    [Fact]
    public void Solve_HeavyDuplicates_DeduplicatesCorrectly()
    {
        var result = ThreeSum.Solve([-2, 0, 0, 2, 2]);
        Assert.Single(result);
        Assert.Equal([-2, 0, 2], result[0]);
    }

    [Fact]
    public void Solve_LessThanThree_ReturnsEmpty()
    {
        Assert.Empty(ThreeSum.Solve([1, -1]));
        Assert.Empty(ThreeSum.Solve([]));
    }

    [Fact]
    public void Solve_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => ThreeSum.Solve(null!));
    }
}
