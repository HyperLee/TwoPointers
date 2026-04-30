namespace TwoPointers.OppositePointers;

/// <summary>
/// LeetCode 15. 三數之和。
/// 排序後固定第一個數，於右側區間以對撞指針尋找另外兩數，並跳過重複值以避免重複組合。
/// </summary>
/// <remarks>
/// 時間複雜度：O(n²)；空間複雜度：O(log n)（排序）或 O(1)（不計輸出）。
/// 範例：
///  - <c>[-1,0,1,2,-1,-4]</c> → <c>[[-1,-1,2],[-1,0,1]]</c>
///  - <c>[0,0,0,0]</c>        → <c>[[0,0,0]]</c>
///  - <c>[1,2,-2,-1]</c>      → <c>[]</c>
/// </remarks>
public static class ThreeSum
{
    /// <summary>找出陣列中所有不重複的三元組 (a, b, c)，使 a + b + c = 0。</summary>
    /// <param name="nums">輸入整數陣列。</param>
    /// <returns>所有不重複的三元組組合。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="nums"/> 為 <c>null</c>。</exception>
    /// <example>
    /// <code>
    /// var result = ThreeSum.Solve(new[] { -1, 0, 1, 2, -1, -4 });
    /// // [[-1,-1,2], [-1,0,1]]
    /// </code>
    /// </example>
    public static IList<IList<int>> Solve(int[] nums)
    {
        ArgumentNullException.ThrowIfNull(nums);

        var result = new List<IList<int>>();
        if (nums.Length < 3)
        {
            return result;
        }

        // 排序是「對撞指針 + 去重」的前提
        int[] sorted = [.. nums];
        Array.Sort(sorted);

        int n = sorted.Length;
        for (int i = 0; i < n - 2; i++)
        {
            // 第一個數已大於 0，後續和必定 > 0，可提早結束
            if (sorted[i] > 0)
            {
                break;
            }

            // 跳過第一個數的重複值，避免產生重複三元組
            if (i > 0 && sorted[i] == sorted[i - 1])
            {
                continue;
            }

            int left = i + 1;
            int right = n - 1;
            int complement = -sorted[i];

            while (left < right)
            {
                int sum = sorted[left] + sorted[right];
                if (sum == complement)
                {
                    result.Add([sorted[i], sorted[left], sorted[right]]);

                    // 跳過 left / right 的重複值
                    while (left < right && sorted[left] == sorted[left + 1])
                    {
                        left++;
                    }
                    while (left < right && sorted[right] == sorted[right - 1])
                    {
                        right--;
                    }

                    left++;
                    right--;
                }
                else if (sum < complement)
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }
        }

        return result;
    }
}
