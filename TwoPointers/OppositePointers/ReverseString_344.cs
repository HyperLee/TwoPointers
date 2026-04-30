namespace TwoPointers.OppositePointers;

/// <summary>
/// LeetCode 344. 反轉字串。
/// 對撞指針原地交換字元，左右指針同步向中央靠攏。
/// </summary>
/// <remarks>
/// 時間複雜度：O(n)；空間複雜度：O(1)。
/// 因為僅以兩個 int 索引互相交換陣列元素，無額外資料結構，所以是 O(1) 額外空間。
/// 範例：
///  - <c>['h','e','l','l','o']</c> → <c>['o','l','l','e','h']</c>
///  - <c>['H','a','n','n','a','h']</c> → <c>['h','a','n','n','a','H']</c>
///  - <c>['a']</c> → <c>['a']</c>
/// </remarks>
public static class ReverseString
{
    /// <summary>就地反轉字元陣列。</summary>
    /// <param name="s">待反轉的字元陣列；操作完成後內容會被修改。</param>
    /// <exception cref="ArgumentNullException"><paramref name="s"/> 為 <c>null</c>。</exception>
    /// <example>
    /// <code>
    /// char[] s = ['h','e','l','l','o'];
    /// ReverseString.Solve(s); // s 變成 ['o','l','l','e','h']
    /// </code>
    /// </example>
    public static void Solve(char[] s)
    {
        ArgumentNullException.ThrowIfNull(s);

        int left = 0;
        int right = s.Length - 1;

        while (left < right)
        {
            // 原地交換左右兩端字元
            (s[left], s[right]) = (s[right], s[left]);
            left++;
            right--;
        }
    }
}
