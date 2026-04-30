namespace TwoPointers.FastSlowPointers;

/// <summary>
/// LeetCode 26. 刪除有序陣列中的重複項。
/// 快慢指針同向：慢指針記錄下一個應寫入的位置，快指針掃描整個陣列。
/// </summary>
/// <remarks>
/// 時間複雜度：O(n)；空間複雜度：O(1)。
/// 為何能 O(1)？所有不重複的元素覆寫回原陣列前段，只使用兩個整數索引。
/// 範例：
///  - <c>[1,1,2]</c>          → 回傳 2，前 2 元素 = [1,2]
///  - <c>[0,0,1,1,1,2,2,3,3,4]</c> → 回傳 5，前 5 元素 = [0,1,2,3,4]
///  - <c>[1]</c>              → 回傳 1
/// </remarks>
public static class RemoveDuplicates
{
    /// <summary>就地移除升序陣列中的重複元素，回傳去重後新長度。</summary>
    /// <param name="nums">升序陣列；前 k 個位置會被覆寫為去重結果。</param>
    /// <returns>去重後的有效長度 k。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="nums"/> 為 <c>null</c>。</exception>
    /// <example>
    /// <code>
    /// int[] nums = [0,0,1,1,1,2,2,3,3,4];
    /// int k = RemoveDuplicates.Solve(nums); // k = 5
    /// </code>
    /// </example>
    public static int Solve(int[] nums)
    {
        ArgumentNullException.ThrowIfNull(nums);
        if (nums.Length == 0)
        {
            return 0;
        }

        // 慢指針 slow：下一個應寫入位置；fast：掃描指針
        int slow = 1;
        for (int fast = 1; fast < nums.Length; fast++)
        {
            // 與前一個保留值不同才寫入
            if (nums[fast] != nums[slow - 1])
            {
                nums[slow] = nums[fast];
                slow++;
            }
        }

        return slow;
    }
}
