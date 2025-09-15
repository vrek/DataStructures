using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures;

public class DuplicatesHashMap
{
    internal static bool FindDuplicate(int[] nums)
    {
        HashSet<int> SeenNumbers = new();
        foreach (int num in nums)
        {
            if (SeenNumbers.Contains(num)) return true;
            SeenNumbers.Add(num);
        }
        return false;
    }
}

public class DuplicatesHashMapPassingTests
{
    [Theory]
    [InlineData(new int[] { 1, 2, 3, 1 }, true)]
    [InlineData(new int[] { 1, 2, 3, 4 }, false)]
    [InlineData(new int[] { 1, 1, 1, 3, 3, 4, 3, 2, 4, 2 }, true)]
    [InlineData(new int[] { }, false)]
    [InlineData(new int[] { 0 }, false)]
    public void GivenValidIntegersFunctionShouldReturnAValidResponse(int[] nums, bool expected)
    {
        bool result = DuplicatesHashMap.FindDuplicate(nums);
        Assert.Equal(expected, result);
    }
   
}