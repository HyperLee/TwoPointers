using TwoPointers.Common;
using TwoPointers.FastSlowPointers;
using TwoPointers.OppositePointers;
using TwoPointers.SlidingWindow;

namespace TwoPointers;

/// <summary>
/// 主控台互動式 Menu 入口。
/// 依規格 §6 列出 10 題雙指針 / 滑動窗口示範，並以內建範例執行對應 <c>Solve</c>。
/// </summary>
internal static class Program
{
    // 連續收到空字串輸入的次數；超過上限視為 stdin 無法互動，主動結束以免看似卡住。
    private const int MaxConsecutiveEmptyInputs = 5;

    private static void Main()
    {
        int consecutiveEmpty = 0;

        while (true)
        {
            PrintMenu();
            Console.Write("請選擇：");
            string? input = Console.ReadLine();

            if (input is null)
            {
                // stdin 已關閉（Ctrl+D / EOF / VS Code 沒有把輸入接到程式）。
                // 印出明確訊息再離開，避免使用者誤以為視窗卡住。
                Console.WriteLine();
                Console.WriteLine("[i] 偵測到輸入流已關閉 (EOF)，程式結束。");
                Console.WriteLine("    若您是用 VS Code F5 執行，請確認在『TERMINAL』面板（不是 DEBUG CONSOLE）輸入數字。");
                return;
            }

            string trimmed = input.Trim();
            if (trimmed.Length == 0)
            {
                consecutiveEmpty++;
                Console.WriteLine("[!] 未收到輸入內容。請在『TERMINAL』面板輸入 0~10 後按 Enter。");
                if (consecutiveEmpty >= MaxConsecutiveEmptyInputs)
                {
                    Console.WriteLine($"[i] 已連續 {MaxConsecutiveEmptyInputs} 次收到空輸入，自動結束程式。");
                    return;
                }

                Console.WriteLine();
                continue;
            }

            consecutiveEmpty = 0;

            if (!int.TryParse(trimmed, out int choice))
            {
                Console.WriteLine($"[!] 無效輸入「{trimmed}」，請輸入 0~10 的整數。\n");
                continue;
            }

            Action? action = choice switch
            {
                0 => null,
                1 => Demo167,
                2 => Demo15,
                3 => Demo344,
                4 => Demo11,
                5 => Demo42,
                6 => Demo26,
                7 => Demo27,
                8 => Demo283,
                9 => Demo141,
                10 => Demo3,
                _ => () => Console.WriteLine("[!] 無效選項，請輸入 0~10。\n"),
            };

            if (choice == 0)
            {
                Console.WriteLine("再見！");
                return;
            }

            Console.WriteLine();
            action!.Invoke();
            Console.WriteLine();
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine("=== Two Pointers Demo ===");
        Console.WriteLine("[對撞指針]");
        Console.WriteLine(" 1) 167. 兩數之和 II");
        Console.WriteLine(" 2) 15.  三數之和");
        Console.WriteLine(" 3) 344. 反轉字串");
        Console.WriteLine(" 4) 11.  盛最多水的容器");
        Console.WriteLine(" 5) 42.  接雨水");
        Console.WriteLine("[快慢指針]");
        Console.WriteLine(" 6) 26.  刪除有序陣列中的重複項");
        Console.WriteLine(" 7) 27.  移除元素");
        Console.WriteLine(" 8) 283. 移動零");
        Console.WriteLine(" 9) 141. 環形鏈表");
        Console.WriteLine("[滑動窗口]");
        Console.WriteLine("10) 3.   無重複字元的最長子字串");
        Console.WriteLine(" 0) 離開");
    }

    private static void PrintHeader(string title)
    {
        Console.WriteLine($"--- {title} ---");
    }

    private static void Demo167()
    {
        PrintHeader("167. 兩數之和 II");
        int[] numbers = [2, 7, 11, 15];
        int target = 9;
        Console.WriteLine($"輸入：numbers = [{string.Join(",", numbers)}], target = {target}");
        int[] result = TwoSumII.Solve(numbers, target);
        Console.WriteLine($"輸出（1-based）：[{string.Join(",", result)}]");
    }

    private static void Demo15()
    {
        PrintHeader("15. 三數之和");
        int[] nums = [-1, 0, 1, 2, -1, -4];
        Console.WriteLine($"輸入：nums = [{string.Join(",", nums)}]");
        var result = ThreeSum.Solve(nums);
        Console.WriteLine("輸出：");
        foreach (var triplet in result)
        {
            Console.WriteLine($"  [{string.Join(",", triplet)}]");
        }
    }

    private static void Demo344()
    {
        PrintHeader("344. 反轉字串");
        char[] s = ['h', 'e', 'l', 'l', 'o'];
        Console.WriteLine($"輸入：s = [{string.Join(",", s)}]");
        ReverseString.Solve(s);
        Console.WriteLine($"輸出：s = [{string.Join(",", s)}]");
    }

    private static void Demo11()
    {
        PrintHeader("11. 盛最多水的容器");
        int[] height = [1, 8, 6, 2, 5, 4, 8, 3, 7];
        Console.WriteLine($"輸入：height = [{string.Join(",", height)}]");
        int result = ContainerWithMostWater.Solve(height);
        Console.WriteLine($"輸出：最大盛水量 = {result}");
    }

    private static void Demo42()
    {
        PrintHeader("42. 接雨水");
        int[] height = [0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1];
        Console.WriteLine($"輸入：height = [{string.Join(",", height)}]");
        int result = TrappingRainWater.Solve(height);
        Console.WriteLine($"輸出：總雨量 = {result}");
    }

    private static void Demo26()
    {
        PrintHeader("26. 刪除有序陣列中的重複項");
        int[] nums = [0, 0, 1, 1, 1, 2, 2, 3, 3, 4];
        Console.WriteLine($"輸入：nums = [{string.Join(",", nums)}]");
        int k = RemoveDuplicates.Solve(nums);
        Console.WriteLine($"輸出：k = {k}, 前 k 個元素 = [{string.Join(",", nums.Take(k))}]");
    }

    private static void Demo27()
    {
        PrintHeader("27. 移除元素");
        int[] nums = [0, 1, 2, 2, 3, 0, 4, 2];
        int val = 2;
        Console.WriteLine($"輸入：nums = [{string.Join(",", nums)}], val = {val}");
        int k = RemoveElement.Solve(nums, val);
        Console.WriteLine($"輸出：k = {k}, 前 k 個元素 = [{string.Join(",", nums.Take(k))}]");
    }

    private static void Demo283()
    {
        PrintHeader("283. 移動零");
        int[] nums = [0, 1, 0, 3, 12];
        Console.WriteLine($"輸入：nums = [{string.Join(",", nums)}]");
        MoveZeroes.Solve(nums);
        Console.WriteLine($"輸出：nums = [{string.Join(",", nums)}]");
    }

    private static void Demo141()
    {
        PrintHeader("141. 環形鏈表");
        // 構造環：3 -> 2 -> 0 -> -4 -> (回到 2)
        var n1 = new ListNode(3);
        var n2 = new ListNode(2);
        var n3 = new ListNode(0);
        var n4 = new ListNode(-4);
        n1.Next = n2; n2.Next = n3; n3.Next = n4; n4.Next = n2;
        Console.WriteLine("輸入：3 -> 2 -> 0 -> -4 -> (回到節點 2，形成環)");
        bool hasCycle = LinkedListCycle.Solve(n1);
        Console.WriteLine($"輸出：是否含環 = {hasCycle}");
    }

    private static void Demo3()
    {
        PrintHeader("3. 無重複字元的最長子字串");
        string s = "pwwkew";
        Console.WriteLine($"輸入：s = \"{s}\"");
        int len = LongestSubstringWithoutRepeating.Solve(s);
        Console.WriteLine($"輸出：最長無重複子字串長度 = {len}");
    }
}
