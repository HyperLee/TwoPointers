using TwoPointers.Common;
using TwoPointers.FastSlowPointers;

namespace TwoPointers.Tests.FastSlowPointers;

public class LinkedListCycle_141_Tests
{
    private static ListNode? BuildList(int[] values, int cycleIndex = -1)
    {
        if (values.Length == 0)
        {
            return null;
        }

        var nodes = new ListNode[values.Length];
        for (int i = 0; i < values.Length; i++)
        {
            nodes[i] = new ListNode(values[i]);
        }
        for (int i = 0; i < values.Length - 1; i++)
        {
            nodes[i].Next = nodes[i + 1];
        }
        if (cycleIndex >= 0 && cycleIndex < values.Length)
        {
            nodes[^1].Next = nodes[cycleIndex];
        }
        return nodes[0];
    }

    [Fact]
    public void Solve_HasCycle_ReturnsTrue()
    {
        Assert.True(LinkedListCycle.Solve(BuildList([3, 2, 0, -4], cycleIndex: 1)));
    }

    [Fact]
    public void Solve_NoCycle_ReturnsFalse()
    {
        Assert.False(LinkedListCycle.Solve(BuildList([1, 2, 3])));
    }

    [Fact]
    public void Solve_NullHead_ReturnsFalse()
    {
        Assert.False(LinkedListCycle.Solve(null));
    }

    [Fact]
    public void Solve_SingleNodeNoCycle_ReturnsFalse()
    {
        Assert.False(LinkedListCycle.Solve(new ListNode(1)));
    }

    [Fact]
    public void Solve_SingleNodeSelfLoop_ReturnsTrue()
    {
        var node = new ListNode(1);
        node.Next = node;
        Assert.True(LinkedListCycle.Solve(node));
    }

    [Fact]
    public void Solve_TwoNodeCycle_ReturnsTrue()
    {
        Assert.True(LinkedListCycle.Solve(BuildList([1, 2], cycleIndex: 0)));
    }
}
