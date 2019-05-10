using System;

// single elements in the list 
class Node
{
    public int data;
    public Node next;
    public Node prev;
}

// this holds the head of the list
class List
{
    public Node listHead;
}

class Program
{

    static void InsertBeginning(List list, Node newNode)
    {
        if (list == null || list.listHead == null)
        {
            list.listHead = newNode;
        }
        else
        {
            newNode.next = list.listHead; //The new nodes first value should be set to the first node in the list definition
            list.listHead.prev = newNode;
            list.listHead = newNode; // the first node is now reset to our new node
            newNode.prev = null; // the prev pointer points to nowhere if its at the front
        }
    }

    static void InsertAtEnd(List list, Node newNode)
    {
        Node traversalNode = list.listHead;
        Node lastNode = Last(list);
        if (list == null || list.listHead == null)
        {
            InsertBeginning(list, newNode);
        }
        else
        {
            lastNode = Last(list);
            lastNode.next = newNode;
            newNode.next = null;
            newNode.prev = lastNode; //the new node points to the old last node
        }
    }

    static Node RemoveBeginning(List list)
    {
        Node returnNode = list.listHead;
        list.listHead = list.listHead.next;
        return returnNode;
    }

    static void InsertAfter(Node node, Node newNode)
    {
        if (node == null || newNode == null)
        {
            Console.WriteLine("One of the nodes were null");
        }

        newNode.next = node.next;
        node.next = newNode; // our new node is now next to the first one
        newNode.prev = node;
        newNode.next.prev = newNode; // Make the next our next node along point to our new node, not its old neighbour. (otherwise wonky link)
    }

    static Node RemoveAfter(Node node)
    {
        if (node == null)
        {
            Console.WriteLine("Node was null");
        }
        node.next = node.next.next;
        return node;
    }

    static bool FindNode(List list, Node nodeToFind)
    {
        Node traversalNode = list.listHead;
        bool nodeFound = false;

        for (int i = 0; i < Length(list); i++)
        {
            if (nodeToFind == traversalNode)
            {
                nodeFound = true;
                return nodeFound;
            }
            traversalNode = traversalNode.next;
        }
        return nodeFound;
    }

    static Node RemoveNode(List list, Node nodeToRemove)
    {
        Node traversalNode = list.listHead;
        for (int i = 0; i < Length(list); i++)
        {
            if (nodeToRemove.next == null) // if we're at the last node
            {
                nodeToRemove.prev.next = null; //Update the previous node to be the new end of the list

            }
            else if (nodeToRemove == traversalNode) //otherwise
            {
                nodeToRemove.next.prev = nodeToRemove.next.prev.prev;
                nodeToRemove.prev.next = nodeToRemove.prev.next.next;
            }
            traversalNode = traversalNode.next;
        }

        return nodeToRemove;
    }

    static void SwapNodes(List list, Node nodeA, Node nodeB)
    {
        if (nodeA == null || nodeB == null)
        {
            Console.WriteLine("One of the nodes was null");
        }


        if (nodeA.prev == null)
        {
            list.listHead = nodeB;
        }
        else
        {
            nodeA.prev.next = nodeB;

        }

        if (nodeB.next != null)
        {
            nodeB.next.prev = nodeA;
        }

        nodeA.next = nodeB.next;
        nodeB.next = nodeA;

        nodeB.prev = nodeA.prev;
        nodeA.prev = nodeB;

    }

    static void AppendLists(List listA, List listB)
    {
        listB.listHead.prev = Last(listA);
        Last(listA).next = listB.listHead;
    }

    static void PrintList(List list)
    {
        Node traversalNode = list.listHead;
        while (traversalNode != null)
        {
            Console.Write(traversalNode.data);
            Console.Write(" <-> ");
            traversalNode = traversalNode.next;
        }
        System.Console.WriteLine("");
    }

    static int Length(List list)
    {
        int i = 0;
        Node traversalNode = list.listHead;
        while (traversalNode != null)
        {
            i++;
            traversalNode = traversalNode.next;
        }
        return i;
    }

    static Node Last(List list)
    {
        Node traversalNode;

        if (list.listHead == null)
        {
            traversalNode = new Node();
        }
        else
        {
            traversalNode = list.listHead;
            while (traversalNode.next != null)
            {
                traversalNode = traversalNode.next;
            }
        }

        return traversalNode;
    }

    static void backwardTraversal(List list) //just to check if prev links work
    {
        Node node = Last(list);

        while (node != null)
        {
            Console.Write(node.data);
            Console.Write(" <-> ");
            node = node.prev;
        }
    }



    static void InsertionSort(List listToSort)
    {
        Node nodeI = listToSort.listHead;
        Node nodeJ = nodeI;
        while (nodeI != null)
        {

            nodeJ = nodeI.prev;

            while (nodeJ != null)
            {

                if (nodeJ.data > nodeI.data) // otherwise do this
                {
                    SwapNodes(listToSort, nodeJ, nodeI);
                }
                nodeJ = nodeJ.prev;

            }
            nodeI = nodeI.next;
        }
    }
       
    static void Main()
    { // do some testing 

        List list = new List();
        Node node, node2, node3, node4, node5;

        Console.WriteLine("Insert beginning");
        for (int i = 0; i < 4; i++)
        {
            node = new Node();
            node.data = i;
            InsertBeginning(list, node);
        }
        PrintList(list);
        Console.WriteLine();

        Console.WriteLine("Insert after the list head 22 then 33");
        node2 = new Node();
        node3 = new Node();
        node2.data = 22;
        node3.data = 33;
        InsertAfter(list.listHead, node2);
        InsertAfter(list.listHead, node3);

        PrintList(list);
        Console.WriteLine();

        Console.WriteLine("Insert at end");
        node4 = new Node();
        node4.data = 44;
        InsertAtEnd(list, node4);
        PrintList(list);
        Console.WriteLine();

        Console.WriteLine("Find node");
        node5 = new Node();
        node5.data = 42;
        InsertAfter(list.listHead.next.next, node5);
        PrintList(list);
        FindNode(list, node5);
        Console.WriteLine("Is " + node5.data + " in the list: " + FindNode(list, node5));
        Console.WriteLine();

        Console.WriteLine("Removing node " + node5.data);
        RemoveNode(list, node5);
        PrintList(list);
        Console.WriteLine();

        Console.WriteLine("Swap nodes " + node2.data + " and " + node3.data);
        SwapNodes(list, node3, node2);
        PrintList(list);
        Console.WriteLine();

        Console.WriteLine("Adding two lists");

        List listB = new List();
        for (int i = 1; i < 5; i++)
        {
            node = new Node();
            node.data = i;
            InsertBeginning(listB, node);
        }
        AppendLists(list, listB);

        PrintList(list);
        backwardTraversal(list);
        Console.WriteLine();
        Console.WriteLine();


        Console.WriteLine("Insertion Sort on random list");
        List randList = new List();
        Node randNode;

        Random randomInt = new Random();

        for (int i = 1; i < 10; i++)
        {
            randNode = new Node();
            randNode.data = randomInt.Next(20);
            InsertBeginning(randList, randNode);

        }

        List testList = new List();
        Node testNode1 = new Node();
        Node testNode2 = new Node();
        Node testNode3 = new Node();
        Node testNode4 = new Node();

        Node testNode5 = new Node();
        Node testNode6 = new Node();
        Node testNode7 = new Node();


        testNode1.data = 1;
        testNode2.data = 7;
        testNode3.data = 2;
        testNode4.data = 9;
        testNode5.data = 3; // our current erroring test case
        testNode6.data = 8;
        testNode7.data = 1;


        InsertAtEnd(testList, testNode1);
        InsertAtEnd(testList, testNode2);
        InsertAtEnd(testList, testNode3);
        InsertAtEnd(testList, testNode4);
        InsertAtEnd(testList, testNode5);
        InsertAtEnd(testList, testNode6);
        InsertAtEnd(testList, testNode7);





        PrintList(randList);
        Console.WriteLine("Sort");
        InsertionSort(randList);

        PrintList(randList);

        Console.WriteLine("End");
        Console.ReadKey();
    }
}
