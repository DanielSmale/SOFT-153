using System;
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


    static List ReadInData()
    {
        StreamReader dataIn = new StreamReader("records.txt");
        List list = new List();
        Node nodeToAdd = new Node();
        char[] firstName = new char[1];
        char[] lastName = new char[1];
        char[] idCharacters = new char[4];



        //Read each value into a character array. Read the id as a char[] and convert to int times digits by 10, 100 , 1000 etc based on poistion
        int i = 0;

        while (!dataIn.EndOfStream)
        {
            char InputData = Convert.ToChar(dataIn.Read());


            if (InputData == ',' || InputData == ' ') // reading in first name
            {
                dataIn.Read(); // to skip the spaces in the file
                ReadInLastName(dataIn, nodeToAdd);
                ReadInId(dataIn, nodeToAdd);
                break;
            }
            else
            {
                Array.Resize(ref nodeToAdd.firstName, i + 1);


                nodeToAdd.firstName[i] = InputData;
            }

            
            if (InputData == ',' || InputData == ' ') // reading in id
            {
                dataIn.Read(); // to skip the space in the file
                break;

            }
            else
            {
                Array.Resize(ref nodeToAdd.lastName, i + 1);
                nodeToAdd.id = InputData;
            }
            i++;

        }

        dataIn.Close();

        return list;
    }
    static void ReadInLastName(StreamReader dataIn, Node nodeToAdd)
    {
        List list = new List();

        int i = 0;
        while (!dataIn.EndOfStream)
        {
            char input = Convert.ToChar(dataIn.Read());


            if (input == ',' || input == ' ') //reading in last name
            {
                dataIn.Read(); // to skip the space in the file
                break;

            }
            else
            {
                Array.Resize(ref nodeToAdd.lastName, i + 1);
                nodeToAdd.lastName[i] = input;
            }

            i++;
        }

    }

    static void ReadInId(StreamReader dataIn, Node nodeToAdd)
    {
        List list = new List();
        char[] idCharacters = new char[3];
        int i = 0;
        while (!dataIn.EndOfStream)
        {
            char input = Convert.ToChar(dataIn.Read());


            if (input == ',' || input == ' ') //reading in last name
            {
                dataIn.Read(); // to skip the space in the file
                break;

            }
            else
            {
                Array.Resize(ref nodeToAdd.lastName, i + 1);
                idCharacters[i] = input;
                nodeToAdd.id = idCharacters[i] * 10;
            }
            
            i++;
        }

    }



    static void Main(string[] args)
    {
        List recordStore = ReadInData();

        PrintList(recordStore);


    }

}
