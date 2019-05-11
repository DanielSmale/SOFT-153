using System;
using System.IO;

// single elements in the list 
class Node
{
    public char[] firstName;
    public char[] lastName;
    public int? id;
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

    static List ReadInNodeData()
    {
        List list = new List();
        StreamReader dataIn = new StreamReader("records.txt");

        char[] firstName = new char[1];
        char[] lastName = new char[1];
        char[] idCharacters = new char[1];

        Node nodeToAdd = new Node(); 

        //Read each line into a character array. Read the id as a char[] and convert to int times digits by 10, 100 , 1000 etc based on poistion
        int i = 0;

        while (!dataIn.EndOfStream) // while theres data left 
        {
            
            char input = Convert.ToChar(dataIn.Read());
            nodeToAdd.id = null;
            nodeToAdd.firstName = null;
            nodeToAdd.lastName = null;


          
                if (input == ',' || input == ' ') // reading in first name
                {
                    dataIn.Read(); // to skip the spaces in the file
                    nodeToAdd.lastName = ReadInLastName(dataIn, lastName);  // loook here for problems ******* maybe store values separately and add them later?????????????
                    nodeToAdd.id = ReadInId(dataIn, idCharacters);
                    break;
                }
                else
                {
                    Array.Resize(ref nodeToAdd.firstName, i + 1);
                    nodeToAdd.firstName[i] = input;
                }
            


            InsertBeginning(list, nodeToAdd);

            i++;
        }
        dataIn.Close();


        return list;
    }

    static char[] ReadInLastName(StreamReader dataIn, char[] lastName)
    {
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
                Array.Resize(ref lastName, i + 1);
                lastName[i] = input;
            }

            i++;
        }
        return lastName;
    }

    static int ReadInId(StreamReader dataIn, char[] idCharacters)
    {
        int id = 0; //the id value to return and add to the node

        int i = 0;
        while (!dataIn.EndOfStream)
        {
            char input = Convert.ToChar(dataIn.Read());


            if (input == ',' || input == ' ') //reading in id
            {
                dataIn.Read(); // to skip the space in the file
                break;

            }
            else if (input == '\r' || input == '\n') // if the input is null or carriage return, we are at the end of the line move on
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
        int lengthOfFile = 6; // Number of lines in file

        list = ReadInNodeData();



        PrintList(list);




        Console.ReadKey();

    }

}
