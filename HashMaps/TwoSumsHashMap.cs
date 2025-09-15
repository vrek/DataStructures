namespace DataStructures.HashMaps;

public class TwoSumHashMap
{
    public static int[] TwoSum(int[] nums, int target)
    {
        Dictionary<int, int> numDict = [];
        int index = 0;
        foreach (int num in nums)
        {
            int complement = target - num;
            if (numDict.TryGetValue(complement, out int value)) return [value, index];
            numDict[num] = index;
            index++;
        }
        return [];
    }
}

public class TwoSumHashMapPassingTests
{
    [Theory]
    [InlineData(new int[] { 2, 7, 11, 15 }, 9, new int[] { 0, 1 })]
    [InlineData(new int[] { 3, 2, 4 }, 6, new int[] { 1, 2 })]
    [InlineData(new int[] { 3, 3 }, 6, new int[] { 0, 1 })]
    [InlineData(new int[] { 1, 2, 3, 4 }, 7, new int[] { 2, 3 })]
    [InlineData(new int[] { 5, 5, 5 }, 10, new int[] { 0, 1 })]
    public void GivenValidIntegersFunctionShouldReturnAValidResponse(int[] nums, int target, int[] expected)
    {
        int[] result = TwoSumHashMap.TwoSum(nums, target);
        Assert.Equal(expected, result);
    }
}

public class TwoSumHashMapFailingTests
{
    [Theory]
    [InlineData(new int[] { 1, 2, 3 }, 10)]
    [InlineData(new int[] { 5, 5, 5 }, 20)]
    [InlineData(new int[] { }, 0)]
    [InlineData(new int[] { 0 }, 1)]
    public void GivenInvalidTargetFunctionShouldReturnNull(int[] nums, int target)
    {
        int[] result = TwoSumHashMap.TwoSum(nums, target);
        Assert.Empty(result);
    }
}
