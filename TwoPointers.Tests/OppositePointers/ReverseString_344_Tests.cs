using TwoPointers.OppositePointers;

namespace TwoPointers.Tests.OppositePointers;

public class ReverseString_344_Tests
{
    [Theory]
    [InlineData(new[] { 'h', 'e', 'l', 'l', 'o' }, new[] { 'o', 'l', 'l', 'e', 'h' })]
    [InlineData(new[] { 'H', 'a', 'n', 'n', 'a', 'h' }, new[] { 'h', 'a', 'n', 'n', 'a', 'H' })]
    [InlineData(new[] { 'a' }, new[] { 'a' })]
    public void Solve_VariousInputs_ReversesInPlace(char[] input, char[] expected)
    {
        ReverseString.Solve(input);
        Assert.Equal(expected, input);
    }

    [Fact]
    public void Solve_EmptyArray_NoChange()
    {
        char[] s = [];
        ReverseString.Solve(s);
        Assert.Empty(s);
    }

    [Fact]
    public void Solve_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => ReverseString.Solve(null!));
    }
}
