using DoubleList;
using System.ComponentModel.Design;

var list = new DoublyLinkedList<string>();
var opc = "0";

do
{
    opc = Menu();

    switch (opc)
    {
        case "1":
            Console.Write("Enter the data to insert at the beginning: ");
            var data1 = Console.ReadLine();
            if (data1 != null)
            {
                list.InsertAtBeginning(data1);
            }
            break;
        case "2":
            Console.Write("Enter the data to insert at the end: ");
            var data2 = Console.ReadLine();
            if (data2 != null)
            {
                list.InsertAtEnd(data2);
            }
            break;
        case "3":
            Console.WriteLine(list.GetForward());
            break;
        case "4":
            Console.WriteLine(list.GetBackward());
            break;
        default:
            break;
    }
}
    while(opc != "0");


string Menu()
{
    Console.WriteLine("1. Insert at beginning");
    Console.WriteLine("2. Insert at end");
    Console.WriteLine("3. Show list forward");
    Console.WriteLine("4. show list backward");
    Console.WriteLine("0. Exit");
    Console.Write("choose an option: ");
    return Console.ReadLine() ?? "0";
}
    