namespace TwoPointers.Common;

/// <summary>
/// LeetCode 風格的單向鏈表節點，僅作為 141. 環形鏈表測試用最小可用節點類別。
/// </summary>
/// <remarks>
/// 本類別不提供完整鏈表實作，僅含 <see cref="Val"/> 與 <see cref="Next"/>。
/// 環形鏈表的建立方式由測試輔助函式手動串接。
/// </remarks>
public sealed class ListNode
{
    /// <summary>節點儲存的整數值。</summary>
    public int Val { get; set; }

    /// <summary>指向下一個節點的參考；若為 <c>null</c> 表示尾節點（在環形鏈表中可指向其他節點）。</summary>
    public ListNode? Next { get; set; }

    /// <summary>建構鏈表節點。</summary>
    /// <param name="val">節點值。</param>
    /// <param name="next">下一節點參考，預設為 <c>null</c>。</param>
    public ListNode(int val, ListNode? next = null)
    {
        Val = val;
        Next = next;
    }
}
