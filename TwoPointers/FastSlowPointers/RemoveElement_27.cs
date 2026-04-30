namespace TwoPointers.FastSlowPointers;

/// <summary>
/// LeetCode 27. 移除元素。
/// 快慢指針同向：fast 掃描，遇到非 val 元素就寫入 slow。
/// </summary>
/// <remarks>
/// 時間複雜度：O(n)；空間複雜度：O(1)。
/// 為何能 O(1)？將保留元素覆寫回原陣列前段，僅使用兩個整數索引，無額外資料結構。
/// 範例：
///  - <c>[3,2,2,3], val=3</c>     → 回傳 2，前 2 元素 = [2,2]
///  - <c>[0,1,2,2,3,0,4,2], val=2</c> → 回傳 5，前 5 元素為 {0,1,3,0,4} 任意順序
///  - <c>[], val=0</c>            → 回傳 0
/// </remarks>
public static class RemoveElement
{
    /// <summary>就地移除所有等於 <paramref name="val"/> 的元素，回傳剩餘元素個數。</summary>
    /// <param name="nums">輸入陣列；前 k 個位置會被覆寫為保留元素。</param>
    /// <param name="val">欲移除的數值。</param>
    /// <returns>剩餘元素個數 k。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="nums"/> 為 <c>null</c>。</exception>
    /// <example>
    /// <code>
    /// int[] nums = [0,1,2,2,3,0,4,2];
    /// int k = RemoveElement.Solve(nums, 2); // k = 5
    /// </code>
    /// </example>
    public static int Solve(int[] nums, int val)
    {
        ArgumentNullException.ThrowIfNull(nums);

        int slow = 0;
        for (int fast = 0; fast < nums.Length; fast++)
        {
            if (nums[fast] != val)
            {
                nums[slow] = nums[fast];
                slow++;
            }
        }

        return slow;
    }
}
