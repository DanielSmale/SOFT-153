﻿using System;
using System.IO;

// single elements in the list 
class Node
{
    public char[] firstName;
    public char[] lastName;
    public int id;
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

    static void RemoveAfter(Node node) // the node passed in is the first one
    {
        if (node == null)
        {
            Console.WriteLine("Node was null");
        }
        node.next = node.next.next;
    }

    static void RemoveNode(List list, Node nodeToRemove)
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
    }


    static void PrintList(List list)
    {
        Node traversalNode = list.listHead;
        while (traversalNode != null)
        {
            Console.Write(traversalNode.id);
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
        Node traversalNode = list.listHead;
        while (traversalNode.next != null)
        {
            traversalNode = traversalNode.next;
        }
        return traversalNode;
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

    static void InsertionSort(List listToSort)
    {
        Node nodeI = listToSort.listHead;
        Node nodeJ = nodeI;
        while (nodeI != null)
        {

            nodeJ = nodeI.prev;

            while (nodeJ != null)
            {

                if (nodeJ.id > nodeI.id) // otherwise do this
                {
                    SwapNodes(listToSort, nodeJ, nodeI);
                }
                nodeJ = nodeJ.prev;

            }
            nodeI = nodeI.next;
        }
    }

    static List ReadInData()
    {
        List list = new List();
        StreamReader dataIn = new StreamReader("records.txt");

        char[] firstName = new char[1];
        char[] lastName = new char[1];
        char[] idCharacters = new char[1];
        int id = 0;


        Node nodeToAdd;


        //Read each line into a character array. Read the id as a char[] and convert to int times digits by 10, 100 , 1000 etc based on poistion
        int i = 0;
        char input = 'a'; // just initialise input to a value it will be replace anyway
        while (!dataIn.EndOfStream) // while theres data left 
        {
            nodeToAdd = new Node();

            i = 0;
            while (input != ',')
            {
                input = Convert.ToChar(dataIn.Read());

                if (input == ',' || input == ' ')
                {

                }
                else
                {
                    Array.Resize(ref firstName, i + 1);
                    firstName[i] = input;
                }
                i++;
            }
            input = Convert.ToChar(dataIn.Read());  // This skips the comma in the file


            i = 0;
            while (input != ',')
            {
                input = Convert.ToChar(dataIn.Read());

                if (input == ',' || input == ' ')
                {

                }
                else
                {
                    Array.Resize(ref lastName, i + 1);
                    lastName[i] = input;
                }

                i++;
            }
            input = Convert.ToChar(dataIn.Read());  // This skips the comma in the file



            i = 0;
            while (input != '\n')
            {
                input = Convert.ToChar(dataIn.Read());

                if (input == '\r' || input == '\n')
                {

                }
                else
                {
                    Array.Resize(ref idCharacters, i + 1);
                    idCharacters[i] = input;
                }
                i++;

            }

            int multiplicant = 1;
            id = 0;
            for (int j = idCharacters.Length - 1; j > -1; j--) //go until j = 0 if its negative 1 drop out
            {
                id = id + (idCharacters[j] - 48) * multiplicant; //ascii values start at 48 = 0, so take away 48 to change to normal numbers

                multiplicant = multiplicant * 10; // multiplicant is use to to times the digits by ie the first digit at the highest index time by 1 the next 10 and so forth...
            }


            nodeToAdd.firstName = firstName;
            nodeToAdd.lastName = lastName;
            nodeToAdd.id = id;


            InsertBeginning(list, nodeToAdd);


        }

        dataIn.Close();


        return list;
    }

    static char[] ReadInLastName(StreamReader dataIn, char[] lastName)
    {
        char input = 'a';

        int i = 0;
        while (input != ',')
        {
            input = Convert.ToChar(dataIn.Read());

            if (input == ',' || input == ' ') //reading in last name
            {
                dataIn.Read(); // to skip the space in the file
                break;

            }
            else
            {
                Array.Resize(ref lastName, i + 1);
                lastName[i] = input;
            }

            i++;
        }
        return lastName;
    }

    static int ReadInId(StreamReader dataIn, char[] idCharacters)
    {
        int id = 0;//the id value to return and add to the node
        char input = 'a';


        int i = 0;
        while (input != ',')
        {
            input = Convert.ToChar(dataIn.Read());


            if (input == '\r') // if the input carriage return (or \r), we are at the end of the line move on
            {
                break;
            }
            else
            {
                Array.Resize(ref idCharacters, i + 1);
                idCharacters[i] = input;
            }


            i++;
        }


        int multiplicant = 1;
        for (int j = idCharacters.Length - 1; j > -1; j--) //go until j = 0 if its negative 1 drop out
        {
            id = id + (idCharacters[j] - 48) * multiplicant; //ascii values start at 48 = 0, so take away 48 to change to normal numbers

            multiplicant = multiplicant * 10; // multiplicant is use to to times the digits by ie the first digit at the highest index time by 1 the next 10 and so forth...
        }

        return id;
    }



    static void Main(string[] args)
    {
        List list = new List();
        Node newNode = new Node();

        list = ReadInData();



        PrintList(list);




        Console.ReadKey();

    }

}
