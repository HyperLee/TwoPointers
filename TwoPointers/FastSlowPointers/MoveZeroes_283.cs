namespace TwoPointers.FastSlowPointers;

/// <summary>
/// LeetCode 283. 移動零。
/// 快慢指針同向：先把所有非零元素依序前移，再將後段補 0。
/// 也可改寫成「交換版」一次完成；本實作採用兩段式以利註解清楚。
/// </summary>
/// <remarks>
/// 時間複雜度：O(n)；空間複雜度：O(1)。
/// 為何能 O(1)？所有非零元素於原陣列上覆寫前段，僅使用兩個整數索引。
/// 範例：
///  - <c>[0,1,0,3,12]</c> → <c>[1,3,12,0,0]</c>
///  - <c>[0]</c>          → <c>[0]</c>
///  - <c>[1,2,3]</c>      → <c>[1,2,3]</c>
/// </remarks>
public static class MoveZeroes
{
    /// <summary>就地將陣列中所有 0 移到尾端，並保持非 0 元素相對順序。</summary>
    /// <param name="nums">輸入陣列。</param>
    /// <exception cref="ArgumentNullException"><paramref name="nums"/> 為 <c>null</c>。</exception>
    /// <example>
    /// <code>
    /// int[] nums = [0,1,0,3,12];
    /// MoveZeroes.Solve(nums); // nums = [1,3,12,0,0]
    /// </code>
    /// </example>
    public static void Solve(int[] nums)
    {
        ArgumentNullException.ThrowIfNull(nums);

        int slow = 0;
        // 第一階段：所有非零元素依序前移
        for (int fast = 0; fast < nums.Length; fast++)
        {
            if (nums[fast] != 0)
            {
                nums[slow] = nums[fast];
                slow++;
            }
        }

        // 第二階段：將剩餘位置補 0
        for (int i = slow; i < nums.Length; i++)
        {
            nums[i] = 0;
        }
    }
}
