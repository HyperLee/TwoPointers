namespace TwoPointers.SlidingWindow;

/// <summary>
/// LeetCode 3. 無重複字元的最長子字串。
/// 同向雙指針構成可變窗口 [left, right]：右指針擴張，遇重複時左指針收縮直到無重複。
/// </summary>
/// <remarks>
/// 時間複雜度：O(n)；空間複雜度：O(min(n, Σ))，Σ 為字元集大小。
/// 與快慢指針的差異：滑動窗口的兩個指針都是「同向」，但移動規則由窗口內狀態（是否含重複字元）驅動，
/// 而快慢指針通常以固定速度差移動（如 ×1、×2）。本題以 Dictionary 紀錄每個字元最近一次的索引，
/// 可在 O(1) 內把 left 直接跳到衝突字元的下一格。
/// 範例：
///  - <c>"abcabcbb"</c> → 3 ("abc")
///  - <c>"bbbbb"</c>    → 1 ("b")
///  - <c>"pwwkew"</c>   → 3 ("wke")
/// </remarks>
public static class LongestSubstringWithoutRepeating
{
    /// <summary>計算字串中無重複字元的最長子字串長度。</summary>
    /// <param name="s">輸入字串，可為空字串。</param>
    /// <returns>最長無重複子字串長度。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="s"/> 為 <c>null</c>。</exception>
    /// <example>
    /// <code>
    /// int len = LongestSubstringWithoutRepeating.Solve("pwwkew"); // 3
    /// </code>
    /// </example>
    public static int Solve(string s)
    {
        ArgumentNullException.ThrowIfNull(s);
        if (s.Length == 0)
        {
            return 0;
        }

        // 字元 → 最近一次出現的索引
        var lastIndex = new Dictionary<char, int>();
        int left = 0;
        int best = 0;

        for (int right = 0; right < s.Length; right++)
        {
            char c = s[right];
            // 若字元已在當前窗口內，將 left 跳到衝突位置 + 1
            if (lastIndex.TryGetValue(c, out int prev) && prev >= left)
            {
                left = prev + 1;
            }

            lastIndex[c] = right;

            int windowLen = right - left + 1;
            if (windowLen > best)
            {
                best = windowLen;
            }
        }

        return best;
    }
}
