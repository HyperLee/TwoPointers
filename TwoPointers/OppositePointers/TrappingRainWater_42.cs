namespace TwoPointers.OppositePointers;

/// <summary>
/// LeetCode 42. 接雨水（雙指針 O(1) 空間版本）。
/// 維護 leftMax / rightMax，於對撞過程中以較小的最大值決定該側可接到的雨水。
/// </summary>
/// <remarks>
/// 時間複雜度：O(n)；空間複雜度：O(1)。
/// 對照 DP 解法：預計算 leftMax[]、rightMax[] 雖直觀但需要 O(n) 額外空間；
/// 雙指針版本僅以兩個變數 leftMax / rightMax 即可達成相同效果。
/// 為何正確？當 height[left] &lt; height[right] 時，rightMax ≥ height[right] &gt; height[left]，
/// 因此 left 位置能裝多少水僅取決於 leftMax，可放心結算。
/// 範例：
///  - <c>[0,1,0,2,1,0,1,3,2,1,2,1]</c> → <c>6</c>
///  - <c>[4,2,0,3,2,5]</c>             → <c>9</c>
///  - <c>[2,0,2]</c>                   → <c>2</c>
/// </remarks>
public static class TrappingRainWater
{
    /// <summary>計算高度條形圖能接住的雨水總量。</summary>
    /// <param name="height">非負整數高度陣列。</param>
    /// <returns>可接住的雨水單位數。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="height"/> 為 <c>null</c>。</exception>
    /// <example>
    /// <code>
    /// int water = TrappingRainWater.Solve(new[] { 0,1,0,2,1,0,1,3,2,1,2,1 }); // 6
    /// </code>
    /// </example>
    public static int Solve(int[] height)
    {
        ArgumentNullException.ThrowIfNull(height);
        if (height.Length < 3)
        {
            return 0;
        }

        int left = 0;
        int right = height.Length - 1;
        int leftMax = 0;
        int rightMax = 0;
        int total = 0;

        while (left < right)
        {
            if (height[left] < height[right])
            {
                // 左側較低：左側可接雨量取決於 leftMax
                if (height[left] >= leftMax)
                {
                    leftMax = height[left];
                }
                else
                {
                    total += leftMax - height[left];
                }
                left++;
            }
            else
            {
                // 右側較低或等高：右側可接雨量取決於 rightMax
                if (height[right] >= rightMax)
                {
                    rightMax = height[right];
                }
                else
                {
                    total += rightMax - height[right];
                }
                right--;
            }
        }

        return total;
    }
}
