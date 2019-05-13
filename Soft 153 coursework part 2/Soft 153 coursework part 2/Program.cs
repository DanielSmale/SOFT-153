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

 



    static void PrintList(List list)
    {
        Node traversalNode = list.listHead;
        while (traversalNode != null)
        {
            Console.Write(traversalNode.id + " ");
            for (int i = 0; i < traversalNode.firstName.Length; i++)
            {
                Console.Write(traversalNode.firstName[i]);
            }
            Console.Write(" ");

            for (int i = 0; i < traversalNode.lastName.Length; i++)
            {
                Console.Write(traversalNode.lastName[i]);
            }
            Console.Write(" ");

            Console.Write(" <-> ");
            traversalNode = traversalNode.next;
        }
        System.Console.WriteLine("");
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
        int selection = 0; // The users selection of what to sort


        Console.WriteLine("What do you want to sort by: ");
        Console.WriteLine("1: first name");
        Console.WriteLine("2: last name");
        Console.WriteLine("or 3: id");
        selection = Convert.ToInt32(Console.ReadLine());

        if (selection == 1) //Sorting first name
        {
            while (nodeI != null)
            {
                nodeJ = nodeI.prev;

                while (nodeJ != null)
                {

                    if (nodeJ.firstName[0] > nodeI.firstName[0]) // If the first character of the first name is bigger (in ascii) its further in the alphabet swap it
                    {
                        SwapNodes(listToSort, nodeJ, nodeI);
                    }
                    nodeJ = nodeJ.prev;

                }
                nodeI = nodeI.next;
            }
        }

        if (selection == 2) //Sorting last name
        {
            while (nodeI != null)
            {
                nodeJ = nodeI.prev;

                while (nodeJ != null)
                {

                    if (nodeJ.lastName[0] > nodeI.lastName[0]) // If the first character of the last name is bigger (in ascii) its further in the alphabet swap it
                    {
                        SwapNodes(listToSort, nodeJ, nodeI);
                    }
                    nodeJ = nodeJ.prev;

                }
                nodeI = nodeI.next;
            }
        }

        if (selection == 3) // Sorting ID
        {
            while (nodeI != null)
            {
                nodeJ = nodeI.prev;

                while (nodeJ != null)
                {

                    if (nodeJ.id > nodeI.id) // If id is bigger swap it
                    {
                        SwapNodes(listToSort, nodeJ, nodeI);
                    }
                    nodeJ = nodeJ.prev;

                }
                nodeI = nodeI.next;
            }
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




    static void Main(string[] args)
    {
        List list = new List();
        Node newNode = new Node();

        list = ReadInData();



        PrintList(list);
        Console.WriteLine("Sort this data");
        InsertionSort(list);

        PrintList(list);


        Console.ReadKey();

    }

}
