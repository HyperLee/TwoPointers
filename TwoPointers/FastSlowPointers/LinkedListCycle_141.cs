using TwoPointers.Common;

namespace TwoPointers.FastSlowPointers;

/// <summary>
/// LeetCode 141. 環形鏈表。
/// Floyd 龜兔賽跑：慢指針每次走 1 步，快指針每次走 2 步；若有環，兩者必相遇。
/// </summary>
/// <remarks>
/// 時間複雜度：O(n)；空間複雜度：O(1)。
/// 數學說明：若鏈表存在環，兩指針進入環後相對速度為每步 1，兩者距離每步減 1，
/// 故必在環長步數內相遇。若無環，快指針會先抵達 <c>null</c>。
/// 範例：
///  - 無環 [3,2,0,-4]                 → false
///  - 環 [3,2,0,-4]，tail.next = node1 → true
///  - 空鏈表 (head=null)              → false
/// </remarks>
public static class LinkedListCycle
{
    /// <summary>判斷鏈表是否含環。</summary>
    /// <param name="head">鏈表首節點，可為 <c>null</c>。</param>
    /// <returns>若有環回傳 <c>true</c>，否則 <c>false</c>。</returns>
    /// <example>
    /// <code>
    /// var n1 = new ListNode(1);
    /// var n2 = new ListNode(2);
    /// n1.Next = n2; n2.Next = n1; // 形成環
    /// bool hasCycle = LinkedListCycle.Solve(n1); // true
    /// </code>
    /// </example>
    public static bool Solve(ListNode? head)
    {
        if (head is null)
        {
            return false;
        }

        ListNode? slow = head;
        ListNode? fast = head;

        while (fast?.Next is not null)
        {
            slow = slow!.Next;
            fast = fast.Next.Next;

            if (ReferenceEquals(slow, fast))
            {
                return true;
            }
        }

        return false;
    }
}
