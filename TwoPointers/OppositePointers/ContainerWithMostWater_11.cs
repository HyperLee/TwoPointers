namespace TwoPointers.OppositePointers;

/// <summary>
/// LeetCode 11. 盛最多水的容器。
/// 對撞指針：每次將「較短的一側」內收，藉此尋找可能更大的面積。
/// </summary>
/// <remarks>
/// 時間複雜度：O(n)；空間複雜度：O(1)。
/// 為何移動較短側？容器面積 = min(左高, 右高) × 寬。寬度必然遞減；若移動較高側，高度上限不變且寬度更小，
/// 面積必不增加；唯有移動較短側才有機會提高高度上限。
/// 範例：
///  - <c>[1,8,6,2,5,4,8,3,7]</c> → <c>49</c>
///  - <c>[1,1]</c>               → <c>1</c>
///  - <c>[4,3,2,1,4]</c>         → <c>16</c>
/// </remarks>
public static class ContainerWithMostWater
{
    /// <summary>給定每個位置的高度，求兩條垂直線與 x 軸構成的容器最大盛水量。</summary>
    /// <param name="height">高度陣列，至少 2 個元素。</param>
    /// <returns>最大盛水面積。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="height"/> 為 <c>null</c>。</exception>
    /// <exception cref="ArgumentException">陣列長度小於 2。</exception>
    /// <example>
    /// <code>
    /// int max = ContainerWithMostWater.Solve(new[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 }); // 49
    /// </code>
    /// </example>
    public static int Solve(int[] height)
    {
        ArgumentNullException.ThrowIfNull(height);
        if (height.Length < 2)
        {
            throw new ArgumentException("高度陣列需至少 2 個元素。", nameof(height));
        }

        int left = 0;
        int right = height.Length - 1;
        int best = 0;

        while (left < right)
        {
            int h = Math.Min(height[left], height[right]);
            int area = h * (right - left);
            if (area > best)
            {
                best = area;
            }

            // 移動較短的一側以期取得更高的高度
            if (height[left] < height[right])
            {
                left++;
            }
            else
            {
                right--;
            }
        }

        return best;
    }
}
