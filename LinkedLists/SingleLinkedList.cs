using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LinkedLists;

internal class SingleLinkedList
{
    public class Node
    {
        public int Value;
        public Node? Next;
        public Node(int value)
        {
            Value = value;
            Next = null;
        }
    }

    public Node? Head;
    
    public SingleLinkedList()
    {
        Head = null;
    }

    public void InsertAtStart(int value)
    {
        Node newNode = new(value);
        if (Head == null)
        {
            Head = newNode;
            return;
        }
        Node current = Head;
        while (current.Next != null)
        {
            current = current.Next;
        }
        current.Next = newNode;
    }

    public int GetLength()
    {
        int length = 0;
        Node? current = Head;
        while (current != null)
        {
            length++;
            current = current.Next;
        }
        return length;
    }
}

public class SingleLinkedListTests
{
    [Fact]
    public void GivenValidIntegersFunctionShouldInsertAtStart()
    {
        SingleLinkedList list = new();
        list.InsertAtStart(1);
        list.InsertAtStart(2);
        list.InsertAtStart(3);
        Assert.NotNull(list.Head);
        Assert.Equal(1, list.Head.Value);
        Assert.NotNull(list.Head.Next);
        Assert.Equal(2, list.Head.Next.Value);
        Assert.NotNull(list.Head.Next.Next);
        Assert.Equal(3, list.Head.Next.Next.Value);
        Assert.Null(list.Head.Next.Next.Next);
    }

    [Theory]
    [MemberData(nameof(LinkedListLengthTestData.Cases), MemberType = typeof(LinkedListLengthTestData))]
    public void GivenVariousInputs_InsertAtStartShouldBuildCorrectLength(int[] values, int expectedLength)
    {
        SingleLinkedList list = new();

        foreach (var value in values)
            list.InsertAtStart(value);

        int actualLength = list.GetLength();
        Assert.Equal(expectedLength, actualLength);
    }


}

public static class LinkedListLengthTestData
{
    public static IEnumerable<object[]> Cases =>
        new List<object[]>
        {
            new object[] { new int[] { }, 0 },
            new object[] { new int[] { 42 }, 1 },
            new object[] { new int[] { 1, 2, 3 }, 3 },
            new object[] { Enumerable.Range(1, 10).ToArray(), 10 },
            new object[] { Enumerable.Range(1, 100).ToArray(), 100 },
        };
}

