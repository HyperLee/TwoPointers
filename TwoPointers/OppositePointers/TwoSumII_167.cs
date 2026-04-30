namespace TwoPointers.OppositePointers;

/// <summary>
/// LeetCode 167. 兩數之和 II - 輸入有序陣列。
/// 採用對撞指針：左右指針從頭尾向中心移動，依當前和與 target 的比較決定指針移動方向。
/// </summary>
/// <remarks>
/// 時間複雜度：O(n)；空間複雜度：O(1)。
/// 範例：
///  - <c>[2,7,11,15], target=9</c>  → <c>[1,2]</c>
///  - <c>[2,3,4],     target=6</c>  → <c>[1,3]</c>
///  - <c>[-1,0],      target=-1</c> → <c>[1,2]</c>
/// </remarks>
public static class TwoSumII
{
    /// <summary>在升序陣列中找出兩個數，使其和等於 <paramref name="target"/>，回傳 1-based 索引。</summary>
    /// <param name="numbers">升序排序的整數陣列，至少包含 2 個元素。</param>
    /// <param name="target">目標和。</param>
    /// <returns>長度為 2 的 1-based 索引陣列；保證題目存在唯一解。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="numbers"/> 為 <c>null</c>。</exception>
    /// <exception cref="ArgumentException">陣列長度小於 2。</exception>
    /// <exception cref="InvalidOperationException">找不到符合條件的組合。</exception>
    /// <example>
    /// <code>
    /// var result = TwoSumII.Solve(new[] { 2, 7, 11, 15 }, 9); // [1, 2]
    /// </code>
    /// </example>
    public static int[] Solve(int[] numbers, int target)
    {
        ArgumentNullException.ThrowIfNull(numbers);
        if (numbers.Length < 2)
        {
            throw new ArgumentException("陣列長度需至少 2。", nameof(numbers));
        }

        int left = 0;
        int right = numbers.Length - 1;

        while (left < right)
        {
            // 使用 long 避免兩個 int.MaxValue 相加溢位
            long sum = (long)numbers[left] + numbers[right];

            // 對撞指針核心：依大小決定哪一側收斂
            if (sum == target)
            {
                return [left + 1, right + 1];
            }
            else if (sum < target)
            {
                // 和過小，左指針右移以增大總和
                left++;
            }
            else
            {
                // 和過大，右指針左移以減小總和
                right--;
            }
        }

        throw new InvalidOperationException($"No valid pair found for {nameof(target)} = {target}.");
    }
}
