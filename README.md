# Two Pointers 數組雙指針 — 教學與範例專案

> 以 **C# 14 / .NET 10** 實作的雙指針 (Two Pointers) 演算法教學專案，內容涵蓋 LeetCode 經典題型、互動式 Demo 與 xUnit 測試。

---

## 目錄

1. [專案簡介](#1-專案簡介)
2. [什麼是雙指針](#2-什麼是雙指針)
3. [三大類型總覽](#3-三大類型總覽)
4. [對撞指針詳解](#4-對撞指針詳解)
5. [快慢指針詳解](#5-快慢指針詳解)
6. [滑動窗口對照](#6-滑動窗口對照)
7. [解題技巧整理](#7-解題技巧整理)
8. [如何執行專案](#8-如何執行專案)
9. [目錄索引](#9-目錄索引)
10. [參考資料](#10-參考資料)

---

## 1. 專案簡介

本專案目標是用一份**完整、可執行、可測試**的 .NET 專案，系統化呈現「雙指針」家族的三大主軸：對撞指針、快慢指針，以及作為對照組的滑動窗口。

適用對象：

- 想系統性學習 LeetCode 雙指針題型的工程師。
- 想看到完整 C# 實作（含中文註解、XML doc、單元測試）的學習者。
- 準備技術面試、需要快速複習常見題型的朋友。

每題皆為 **一題一檔**，可獨立閱讀，並附 `Program.cs` 互動式 Menu 直接執行驗證。

---

## 2. 什麼是雙指針

「雙指針」指的是**用兩個索引（或節點參考）在線性結構（陣列、字串、鏈表）上協同移動**的技巧。其核心價值在於：

> 透過「指針之間的距離 / 速度差」攜帶額外資訊，把樸素的 O(N²) 雙重迴圈降到 O(N)。

之所以能 O(N²) → O(N)，關鍵是「**單調性**」：當指針依某種規則移動時，每個指針至多前進 N 步，總移動次數仍是線性。

---

## 3. 三大類型總覽

| 類型 | 移動方向 | 終止條件 | 適用場景 | 典型題目 |
| --- | --- | --- | --- | --- |
| 對撞指針 (Opposite) | `left → ←  right` | `left >= right` | 已排序陣列、需要左右收斂的搜尋／面積／反轉 | 167, 15, 344, 11, 42 |
| 快慢指針 (Fast-Slow) | `slow →  fast →`（同向不同速） | `fast` 走完或抵 `null` | 原地修改、鏈表環檢測、找中點 | 26, 27, 283, 141 |
| 滑動窗口 (Sliding Window) | `left →  right →`（同向） | `right` 走完 | 子陣列／子字串、連續區間最佳化 | 3, 76, 209, 438 |

---

## 4. 對撞指針詳解

### 4.1 原理

兩個指針一個從頭、一個從尾，依當前狀態決定哪一側內收：

```
索引:   0   1   2   3   4   5
值:   [ 2 , 7 , 11, 15, 19, 25]
        ^                       ^
        L ───────────────────── R
                ↓ sum 太小，L++
            ^                   ^
            L ─────────────── R
                    ↓ sum 太大，R--
            ^               ^
            L ─────────── R
```

### 4.2 模板

```csharp
int left = 0, right = arr.Length - 1;
while (left < right)
{
    var v = Evaluate(arr[left], arr[right]);
    if (v == target) { /* 命中 */ break; }
    else if (v < target) left++;
    else right--;
}
```

### 4.3 題目精解

| LeetCode | 題目 | 思路 | 複雜度 | 原始碼 |
| --- | --- | --- | --- | --- |
| 167 | 兩數之和 II | 升序 + 左右收斂 | O(n) / O(1) | [TwoSumII_167.cs](TwoPointers/OppositePointers/TwoSumII_167.cs) |
| 15  | 三數之和 | 排序 + 固定一個 + 雙指針 + 跳過重複 | O(n²) / O(1) | [ThreeSum_15.cs](TwoPointers/OppositePointers/ThreeSum_15.cs) |
| 344 | 反轉字串 | 原地交換 | O(n) / O(1) | [ReverseString_344.cs](TwoPointers/OppositePointers/ReverseString_344.cs) |
| 11  | 盛最多水的容器 | 較短側內收 | O(n) / O(1) | [ContainerWithMostWater_11.cs](TwoPointers/OppositePointers/ContainerWithMostWater_11.cs) |
| 42  | 接雨水 | 維護 leftMax / rightMax，較低側結算 | O(n) / O(1) | [TrappingRainWater_42.cs](TwoPointers/OppositePointers/TrappingRainWater_42.cs) |

> **15. 三數之和的去重關鍵**：
> - 外層 `i > 0 && nums[i] == nums[i-1]` 跳過。
> - 內層命中後，`while (left < right && nums[left] == nums[left+1]) left++;` 與 `right` 對稱處理。

> **42. 為何雙指針能 O(1) 空間？**
> 當 `height[left] < height[right]` 時，`rightMax ≥ height[right] > height[left]`，因此左側位置可裝多少水僅取決於 `leftMax`，可立即結算，不需 DP 預存陣列。

---

## 5. 快慢指針詳解

### 5.1 原理

兩個指針同向移動但**速度／規則不同**：

- **慢指針 (slow)**：通常代表「下一個應寫入的位置」或「龜」。
- **快指針 (fast)**：通常負責掃描或「兔」（每次走 2 步）。

```
[陣列原地修改]
原始: [0, 1, 0, 3, 12]
       s
       f →

完成: [1, 3, 12, 0, 0]
              s
                       f
```

```
[Floyd 龜兔賽跑：偵測環]
slow:  ●─→●─→●─→●
fast:  ●───→●───→● ... 若有環，快慢兩者必在環中相遇
```

### 5.2 模板

```csharp
int slow = 0;
for (int fast = 0; fast < nums.Length; fast++)
{
    if (Keep(nums[fast]))
    {
        nums[slow++] = nums[fast];
    }
}
return slow; // 有效長度
```

### 5.3 題目精解

| LeetCode | 題目 | 思路 | 複雜度 | 原始碼 |
| --- | --- | --- | --- | --- |
| 26  | 刪除有序陣列重複項 | slow 紀錄寫入位置 | O(n) / O(1) | [RemoveDuplicates_26.cs](TwoPointers/FastSlowPointers/RemoveDuplicates_26.cs) |
| 27  | 移除元素 | 過濾值，同模板 | O(n) / O(1) | [RemoveElement_27.cs](TwoPointers/FastSlowPointers/RemoveElement_27.cs) |
| 283 | 移動零 | 非零前移 + 補零 | O(n) / O(1) | [MoveZeroes_283.cs](TwoPointers/FastSlowPointers/MoveZeroes_283.cs) |
| 141 | 環形鏈表 | Floyd 龜兔賽跑 | O(n) / O(1) | [LinkedListCycle_141.cs](TwoPointers/FastSlowPointers/LinkedListCycle_141.cs) |

### 5.4 Floyd 演算法為何能找環？

設環長為 C，無環段長為 L，慢指針進入環時走了 L 步，快指針已多走 L 步且位於環中某處。之後每步「快比慢多走 1 步」，兩者相對距離每步 -1，最多 C 步必相遇 → 證明若有環必相遇，且時間 O(L + C) = O(n)。

---

## 6. 滑動窗口對照

滑動窗口同樣是「同向雙指針」，但移動規則由**窗口內狀態**驅動，而非固定速度差：

- 右指針 `right` 持續擴張 → 把新元素納入窗口。
- 當窗口違反條件（如出現重複字元），左指針 `left` 收縮直到條件再次成立。

以 [LongestSubstringWithoutRepeating_3.cs](TwoPointers/SlidingWindow/LongestSubstringWithoutRepeating_3.cs) 為例：

```
s = "pwwkew"
       ↑
窗口從 "p" → "pw" → "pww" (違反) → 收縮 → "ww" (違反) → "w" → "wk" → "wke" → "kew"
最長: 3
```

| 比較項 | 快慢指針 | 滑動窗口 |
| --- | --- | --- |
| 移動規則 | 固定速度差（×1, ×2 / 條件覆寫） | 由窗口狀態觸發收縮 |
| 典型用途 | 原地修改、環檢測 | 連續子陣列／子字串最佳化 |
| 是否需輔助結構 | 否 | 通常需 Hash / Counter |

---

## 7. 解題技巧整理

1. **看到「有序」(sorted) → 優先想對撞指針**：167、15、11 都是排序後左右收斂。
2. **看到「原地修改」 → 優先想快慢指針**：26、27、283 共用 slow=寫入位置 模板。
3. **雙指針 vs 滑動窗口**：
   - 求「索引組合 / 配對」→ 雙指針。
   - 求「連續區間最值 / 計數」→ 滑動窗口。
4. **常見陷阱**：
   - **去重**：15. 三數之和的內外雙層 skip。
   - **整數溢位**：167 兩數相加時用 `long` 避免邊界值溢位。
   - **空輸入 / null**：每題在入口處以 `ArgumentNullException.ThrowIfNull` 處理。
   - **單調性破壞**：移動指針的規則若不滿足單調性，整體就退化成 O(N²)。

---

## 8. 如何執行專案

### 環境需求

- .NET 10 SDK
- macOS / Linux / Windows 任一

### 指令

```bash
# 建置整個方案
dotnet build TwoPointers.sln

# 執行互動式 Demo
dotnet run --project TwoPointers

# 執行所有單元測試
dotnet test TwoPointers.sln
```

啟動 Demo 後會看到：

```
=== Two Pointers Demo ===
[對撞指針]
 1) 167. 兩數之和 II
 2) 15.  三數之和
 ...
請選擇：
```

輸入對應數字即可看到輸入、過程說明與輸出。輸入 `0` 結束。

### 在 VS Code 內執行（F5）的注意事項

按下 **F5** 啟動 Debug 後，VS Code 會同時開啟 **DEBUG CONSOLE** 與一個新的 **TERMINAL** 面板。本專案的 `Console.ReadLine()` 只會接收 **TERMINAL** 面板的輸入：

> ⚠️ 若把數字打在 **DEBUG CONSOLE** 面板，程式會收不到（或只收到空 Enter），看起來像「選了沒反應、又跳回 menu」。請務必把焦點切到 **TERMINAL** 面板再輸入。

`.vscode/launch.json` 已提供兩種設定：

| 名稱 | console | 適用情境 |
| --- | --- | --- |
| `.NET Launch (TwoPointers, integrated terminal)` | `integratedTerminal` | 預設；在 VS Code 內建 TERMINAL 面板執行。 |
| `.NET Launch (TwoPointers, external terminal)` | `externalTerminal` | 備援；開獨立的系統終端機視窗，最不容易因面板焦點問題踩坑。 |

如果使用內建終端機仍遇到輸入沒反應，請從左側「執行與偵錯」面板切換到 *external terminal* 設定再 F5。

---

## 9. 目錄索引

```
TwoPointers/
├── README.md
├── TwoPointers.sln
├── TwoPointers/
│   ├── Program.cs
│   ├── Common/ListNode.cs
│   ├── OppositePointers/
│   │   ├── TwoSumII_167.cs
│   │   ├── ThreeSum_15.cs
│   │   ├── ReverseString_344.cs
│   │   ├── ContainerWithMostWater_11.cs
│   │   └── TrappingRainWater_42.cs
│   ├── FastSlowPointers/
│   │   ├── RemoveDuplicates_26.cs
│   │   ├── RemoveElement_27.cs
│   │   ├── MoveZeroes_283.cs
│   │   └── LinkedListCycle_141.cs
│   └── SlidingWindow/
│       └── LongestSubstringWithoutRepeating_3.cs
└── TwoPointers.Tests/   ← xUnit 70+ 測試案例
    ├── OppositePointers/
    ├── FastSlowPointers/
    └── SlidingWindow/
```

| 題號 | 檔案 |
| --- | --- |
| 167 | [TwoPointers/OppositePointers/TwoSumII_167.cs](TwoPointers/OppositePointers/TwoSumII_167.cs) |
| 15  | [TwoPointers/OppositePointers/ThreeSum_15.cs](TwoPointers/OppositePointers/ThreeSum_15.cs) |
| 344 | [TwoPointers/OppositePointers/ReverseString_344.cs](TwoPointers/OppositePointers/ReverseString_344.cs) |
| 11  | [TwoPointers/OppositePointers/ContainerWithMostWater_11.cs](TwoPointers/OppositePointers/ContainerWithMostWater_11.cs) |
| 42  | [TwoPointers/OppositePointers/TrappingRainWater_42.cs](TwoPointers/OppositePointers/TrappingRainWater_42.cs) |
| 26  | [TwoPointers/FastSlowPointers/RemoveDuplicates_26.cs](TwoPointers/FastSlowPointers/RemoveDuplicates_26.cs) |
| 27  | [TwoPointers/FastSlowPointers/RemoveElement_27.cs](TwoPointers/FastSlowPointers/RemoveElement_27.cs) |
| 283 | [TwoPointers/FastSlowPointers/MoveZeroes_283.cs](TwoPointers/FastSlowPointers/MoveZeroes_283.cs) |
| 141 | [TwoPointers/FastSlowPointers/LinkedListCycle_141.cs](TwoPointers/FastSlowPointers/LinkedListCycle_141.cs) |
| 3   | [TwoPointers/SlidingWindow/LongestSubstringWithoutRepeating_3.cs](TwoPointers/SlidingWindow/LongestSubstringWithoutRepeating_3.cs) |

---

## 10. 參考資料

- LeetCode 167：<https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/>
- LeetCode 15： <https://leetcode.com/problems/3sum/>
- LeetCode 344：<https://leetcode.com/problems/reverse-string/>
- LeetCode 11： <https://leetcode.com/problems/container-with-most-water/>
- LeetCode 42： <https://leetcode.com/problems/trapping-rain-water/>
- LeetCode 26： <https://leetcode.com/problems/remove-duplicates-from-sorted-array/>
- LeetCode 27： <https://leetcode.com/problems/remove-element/>
- LeetCode 283：<https://leetcode.com/problems/move-zeroes/>
- LeetCode 141：<https://leetcode.com/problems/linked-list-cycle/>
- LeetCode 3：  <https://leetcode.com/problems/longest-substring-without-repeating-characters/>
- Floyd's Tortoise and Hare：<https://en.wikipedia.org/wiki/Cycle_detection#Floyd's_tortoise_and_hare>
