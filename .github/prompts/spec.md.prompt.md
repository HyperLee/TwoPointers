# Two Pointers 數組雙指針 — 開發規格書

## 1. 專案概述

本專案以 **C# (.NET 10)** 為基礎，建立一份完整、可執行、可測試的「雙指針 (Two Pointers) 演算法」教學與範例專案。內容涵蓋 LeetCode 中常見的雙指針題型，以程式碼搭配中文註解與獨立 README.md 文件，幫助讀者全面理解雙指針的概念、變形與適用情境。

### 1.1 目標

1. 提供一份**完整的中文教學文件 (README.md)**，系統化介紹「對撞指針」與「快慢指針」兩大主軸，並補充滑動窗口的差異說明。
2. 在 C# 專案中實作經典 LeetCode 雙指針題目，每題：
   - 獨立 `.cs` 檔案（一題一檔）。
   - 含完整 XML 文件註解 + 演算法步驟註解 + 時間/空間複雜度說明。
   - 提供範例輸入輸出，可從 `Program.cs` 互動式 Menu 直接執行驗證。
3. 建立 **xUnit 單元測試專案**，覆蓋每題的正常 / 邊界 / 特殊輸入情境。

### 1.2 非目標

- 不涵蓋鏈表 (Linked List) 完整資料結構實作（僅在「環形鏈表偵測」中提供最小可用節點類別）。
- 不深入動態規劃、貪心等其他主題。
- 不建立 GUI / Web API。

---

## 2. 技術規格

| 項目 | 設定 |
| --- | --- |
| 語言 | C# 14（採用最新語言特性，例如 primary constructor、collection expressions、pattern matching） |
| 執行環境 | .NET 10.0 |
| 主專案 | `TwoPointers/TwoPointers.csproj`（Console App） |
| 測試框架 | xUnit（新增 `TwoPointers.Tests/TwoPointers.Tests.csproj`） |
| Nullable | 啟用 (`<Nullable>enable</Nullable>`) |
| ImplicitUsings | 啟用 |
| Namespace | 檔案範圍 (file-scoped) namespace |
| Coding Style | 遵循 `.editorconfig` 與 `.github/instructions/csharp.instructions.md` |

---

## 3. 專案結構

```
TwoPointers/
├── TwoPointers.sln
├── README.md                      ← 雙指針演算法完整教學文件（新增）
├── TwoPointers/
│   ├── TwoPointers.csproj
│   ├── Program.cs                 ← 互動式 Menu 入口（改寫）
│   ├── Common/
│   │   └── ListNode.cs            ← 環形鏈表用最小節點類別
│   ├── OppositePointers/          ← 對撞指針 (相向而行)
│   │   ├── TwoSumII_167.cs
│   │   ├── ThreeSum_15.cs
│   │   ├── ReverseString_344.cs
│   │   ├── ContainerWithMostWater_11.cs
│   │   └── TrappingRainWater_42.cs
│   ├── FastSlowPointers/          ← 快慢指針 (同向)
│   │   ├── RemoveDuplicates_26.cs
│   │   ├── RemoveElement_27.cs
│   │   ├── MoveZeroes_283.cs
│   │   └── LinkedListCycle_141.cs
│   └── SlidingWindow/             ← 滑動窗口 (對照組)
│       └── LongestSubstringWithoutRepeating_3.cs
└── TwoPointers.Tests/             ← xUnit 測試專案（新增）
    ├── TwoPointers.Tests.csproj
    ├── OppositePointers/
    │   ├── TwoSumII_167_Tests.cs
    │   ├── ThreeSum_15_Tests.cs
    │   ├── ReverseString_344_Tests.cs
    │   ├── ContainerWithMostWater_11_Tests.cs
    │   └── TrappingRainWater_42_Tests.cs
    ├── FastSlowPointers/
    │   ├── RemoveDuplicates_26_Tests.cs
    │   ├── RemoveElement_27_Tests.cs
    │   ├── MoveZeroes_283_Tests.cs
    │   └── LinkedListCycle_141_Tests.cs
    └── SlidingWindow/
        └── LongestSubstringWithoutRepeating_3_Tests.cs
```

---

## 4. 程式碼撰寫規範

每個解題類別共用以下慣例：

1. **靜態類別 + 靜態方法**：例如 `public static class TwoSumII { public static int[] Solve(int[] numbers, int target) { ... } }`，方便測試與 Menu 直接呼叫，無需建立物件。
2. **檔案範圍 namespace**：`namespace TwoPointers.OppositePointers;`。
3. **公開 API 必須附 XML 文件註解**：含 `<summary>`、`<param>`、`<returns>`、`<example>` (含 `<code>` 範例)。
4. **演算法核心區塊以中文行內註解逐步說明**：解釋每一步指針移動的原因。
5. **複雜度註記**：在類別 `<remarks>` 中標註時間複雜度 / 空間複雜度。
6. **Null 與邊界處理**：在入口處檢查 null / 空陣列；以 `is null` / `is not null` 判斷。
7. **使用 `nameof`** 取代字串字面量於錯誤訊息中。
8. **以 pattern matching / switch expression** 取代多重 if 串接（適用時）。

### 4.1 範例骨架

```csharp
namespace TwoPointers.OppositePointers;

/// <summary>
/// LeetCode 167. 兩數之和 II - 輸入有序陣列。
/// 採用對撞指針：左右指針從頭尾向中心移動，依當前和與 target 的比較決定指針移動方向。
/// </summary>
/// <remarks>
/// 時間複雜度：O(n)；空間複雜度：O(1)。
/// </remarks>
public static class TwoSumII
{
    /// <summary>在升序陣列中找出兩個數，使其和等於 <paramref name="target"/>，回傳 1-based 索引。</summary>
    /// <param name="numbers">升序排序的整數陣列。</param>
    /// <param name="target">目標和。</param>
    /// <returns>長度為 2 的 1-based 索引陣列。</returns>
    /// <example>
    /// <code>
    /// var result = TwoSumII.Solve(new[] { 2, 7, 11, 15 }, 9); // [1, 2]
    /// </code>
    /// </example>
    public static int[] Solve(int[] numbers, int target)
    {
        ArgumentNullException.ThrowIfNull(numbers);

        int left = 0;
        int right = numbers.Length - 1;

        while (left < right)
        {
            int sum = numbers[left] + numbers[right];
            // 對撞指針核心：依大小決定哪一側收斂
            if (sum == target)
            {
                return [left + 1, right + 1];
            }
            else if (sum < target)
            {
                left++;
            }
            else
            {
                right--;
            }
        }

        throw new InvalidOperationException($"No valid pair found for {nameof(target)} = {target}.");
    }
}
```

---

## 5. 題目清單與實作要求

### 5.1 對撞指針 (Opposite / Two-End Pointers)

| # | LeetCode | 中文名稱 | 簽章 | 重點 |
| --- | --- | --- | --- | --- |
| 1 | 167 | 兩數之和 II - 輸入有序陣列 | `int[] Solve(int[] numbers, int target)` | 升序陣列 + 左右收斂 |
| 2 | 15 | 三數之和 | `IList<IList<int>> Solve(int[] nums)` | 排序 + 固定一個 + 雙指針，去重 |
| 3 | 344 | 反轉字串 | `void Solve(char[] s)` | 原地交換 |
| 4 | 11 | 盛最多水的容器 | `int Solve(int[] height)` | 較短側收斂 |
| 5 | 42 | 接雨水 | `int Solve(int[] height)` | `leftMax`/`rightMax` 雙指針版本 |

### 5.2 快慢指針 (Fast-Slow / Same-Direction Pointers)

| # | LeetCode | 中文名稱 | 簽章 | 重點 |
| --- | --- | --- | --- | --- |
| 6 | 26 | 刪除有序陣列中的重複項 | `int Solve(int[] nums)` | 慢針記錄寫入位置 |
| 7 | 27 | 移除元素 | `int Solve(int[] nums, int val)` | 同上，過濾值 |
| 8 | 283 | 移動零 | `void Solve(int[] nums)` | 非零前移 + 補零 |
| 9 | 141 | 環形鏈表 | `bool Solve(ListNode? head)` | Floyd 龜兔賽跑 |

### 5.3 滑動窗口 (對照組)

| # | LeetCode | 中文名稱 | 簽章 | 重點 |
| --- | --- | --- | --- | --- |
| 10 | 3 | 無重複字元的最長子字串 | `int Solve(string s)` | 同向雙指針構成可變窗口，與快慢指針的差異對照 |

### 5.4 共同要求

- 每題必須包含 **演算法描述、複雜度、邊界條件、至少 3 組範例輸入輸出** 的 XML 註解區塊。
- 對於原地修改類型 (26/27/283/344)，註解必須說明「為何能 O(1) 空間」。
- 對於 15.三數之和，必須處理重複數值的跳過邏輯，並有對應測試案例。

---

## 6. Program.cs 互動式 Menu 規格

`Program.cs` 改寫為簡單的命令列 Menu，啟動時列出所有題目並接收使用者輸入：

```
=== Two Pointers Demo ===
[對撞指針]
 1) 167. 兩數之和 II
 2) 15.  三數之和
 3) 344. 反轉字串
 4) 11.  盛最多水的容器
 5) 42.  接雨水
[快慢指針]
 6) 26.  刪除有序陣列中的重複項
 7) 27.  移除元素
 8) 283. 移動零
 9) 141. 環形鏈表
[滑動窗口]
10) 3.   無重複字元的最長子字串
 0) 離開
請選擇：
```

行為要求：

- 每個選項以**內建範例輸入**直接執行對應 `Solve`，並把輸入、過程說明、輸出印出。
- 採用 `switch expression` 對應選項，無效輸入需提示後重新顯示 Menu。
- 輸入 `0` 或 `Ctrl+C` 結束程式。
- 程式碼以小型輔助方法封裝列印邏輯，避免 `Main` 過長。

---

## 7. README.md 內容大綱（繁體中文）

新增於專案根目錄 `/README.md`，內容章節：

1. **專案簡介**：說明本專案目標與適用對象。
2. **什麼是雙指針**：定義、為何能將 O(N²) 降為 O(N)。
3. **三大類型總覽**：
   - 對撞指針 (Opposite Pointers)
   - 快慢指針 (Fast-Slow Pointers)
   - 滑動窗口 (Sliding Window) — 與雙指針的關聯與差異
   附對照表 (適用場景 / 移動方向 / 終止條件 / 典型題目)。
4. **對撞指針詳解**：
   - 原理與圖示（以 ASCII 圖描繪指針移動）
   - 模板程式碼（C# 虛擬碼）
   - 題目精解：167、15、344、11、42（逐題附思路、複雜度、關鍵程式碼片段連結）
5. **快慢指針詳解**：
   - 原理與圖示
   - 模板程式碼
   - 題目精解：26、27、283、141（含 Floyd 演算法為何能找環的數學說明）
6. **滑動窗口對照**：以 LeetCode 3 為例，比較與快慢指針的異同。
7. **解題技巧整理**：
   - 看到「有序」優先想對撞指針
   - 看到「原地修改」優先想快慢指針
   - 雙指針 vs 滑動窗口的判斷準則
   - 常見陷阱（去重、整數溢位、空輸入處理）
8. **如何執行專案**：`dotnet build`、`dotnet run --project TwoPointers`、`dotnet test`。
9. **目錄索引**：列出所有題目對應原始碼檔案路徑（含 GitHub 連結）。
10. **參考資料**：LeetCode 題目連結、相關文章。

---

## 8. 測試規格 (xUnit)

### 8.1 專案設定

- 新增 `TwoPointers.Tests/TwoPointers.Tests.csproj`。
- TargetFramework：`net10.0`。
- 套件：`xunit`、`xunit.runner.visualstudio`、`Microsoft.NET.Test.Sdk`、`coverlet.collector`。
- 加入主專案專案參考。
- 將測試專案加入 `TwoPointers.sln`。

### 8.2 測試案例覆蓋要求

每題至少包含以下測試類別（依題目語意取捨）：

1. **典型範例**：LeetCode 題目給的官方測資。
2. **邊界值**：空陣列 / 單一元素 / 全相同元素 / 已排序好結果。
3. **無解 / 特殊路徑**：如 167 找不到組合應拋例外；141 空鏈表回傳 `false`。
4. **大量重複**：如 15 / 26 / 283 重複元素去重正確性。
5. **資料驗證**：null 輸入應拋 `ArgumentNullException`（或視題意決定）。

測試命名遵循 `MethodUnderTest_Scenario_ExpectedResult`，例如：

```csharp
[Fact]
public void Solve_TargetExists_ReturnsOneBasedIndices() { ... }

[Theory]
[InlineData(new[] { 2, 7, 11, 15 }, 9, new[] { 1, 2 })]
[InlineData(new[] { -1, 0 }, -1, new[] { 1, 2 })]
public void Solve_VariousInputs_ReturnsExpected(int[] numbers, int target, int[] expected) { ... }
```

依規範**不寫 `// Arrange / Act / Assert` 註解**。

### 8.3 通過標準

- `dotnet test` 全部通過。
- 每題至少 3 個 `[Fact]` 或對應 `[Theory]` 資料列。

---

## 9. 驗收條件 (Definition of Done)

1. ✅ `dotnet build TwoPointers.sln` 無錯誤、無警告（除 .NET SDK 自帶警告外）。
2. ✅ `dotnet run --project TwoPointers` 能顯示 Menu 並正確執行 10 題範例。
3. ✅ `dotnet test` 全部測試通過。
4. ✅ 所有公開類別 / 方法皆具備 XML 文件註解。
5. ✅ 每個題目 `.cs` 檔案存在於對應分類資料夾，且與規格表 5.1–5.3 一致。
6. ✅ `README.md` 章節完整，繁體中文撰寫，含 ASCII 圖示與題目索引連結。
7. ✅ 無未使用 `using`、無 nullable warning。

---

## 10. 實作階段規劃

| 階段 | 內容 |
| --- | --- |
| Phase 1 | 建立資料夾結構、`Common/ListNode.cs`、調整 `TwoPointers.csproj`（如需要） |
| Phase 2 | 實作對撞指針 5 題 (5.1) |
| Phase 3 | 實作快慢指針 4 題 (5.2) |
| Phase 4 | 實作滑動窗口 1 題 (5.3) |
| Phase 5 | 改寫 `Program.cs` 互動式 Menu |
| Phase 6 | 建立 `TwoPointers.Tests` 專案並撰寫所有測試 |
| Phase 7 | 撰寫根目錄 `README.md` |
| Phase 8 | 執行 `dotnet build` / `dotnet test` 整體驗收 |

---

## 11. 風險與備註

- **.NET 10 / C# 14**：若本機 SDK 尚未支援，需先確認 `dotnet --list-sdks`，必要時調整 `TargetFramework`。
- **15. 三數之和去重**：需特別注意 `nums[i] == nums[i-1]` 與內層 left/right 跳過邏輯，是常見錯誤點。
- **42. 接雨水**：若採用「動態規劃預計算 leftMax/rightMax」雖然直觀但空間 O(n)；本專案要求採用「雙指針 O(1) 空間」版本以契合主題，並在註解中對照兩種解法。
- **141. 環形鏈表**：需提供最小 `ListNode` 類別與在測試中手動串接成環的輔助函式。

---

> 本規格完成後，請審核確認；通過後即依第 10 節 Phase 1 → Phase 8 順序進行實作。
