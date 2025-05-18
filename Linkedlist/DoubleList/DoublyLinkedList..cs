using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleList;

public class DoublyLinkedList<T> where T : IComparable<T>
{
    private DoubleNode<T>? _head;
    private DoubleNode<T>? _tail;

    public DoublyLinkedList()
    {
        _tail = null;
        _head = null;
    }

    public void InsertInOrder(T data)
    {
        DoubleNode<T> newNode = new DoubleNode<T>(data);

        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
            return;
        }

        if (_head.Data == null || newNode.Data.CompareTo(_head.Data) <= 0)
        {
            newNode.Next = _head;
            _head.Prev = newNode;
            _head = newNode;
            return;
        }

        if (_tail.Data == null || newNode.Data.CompareTo(_tail.Data) >= 0)
        {
            _tail.Next = newNode;
            newNode.Prev = _tail;
            _tail = newNode;
            return;
        }

        DoubleNode<T> current = _head;
        while (current.Next != null && current.Next.Data != null && newNode.Data.CompareTo(current.Next.Data) > 0)
        {
            current = current.Next;
        }

        newNode.Next = current.Next;
        newNode.Prev = current;
        if (current.Next != null)
        {
            current.Next.Prev = newNode;
        }
        current.Next = newNode;
    }

    public string ShowForward()  // metodo mostra hacia adelante
    {
        var output = string.Empty;
        var current = _head;
        while (current != null)
        {
            output += $"{current.Data} <=> ";
            current = current.Next;
        }
        return output.Length > 0 ? output.Substring(0, output.Length - 5) : output;
    }

    public string ShowBackward()  // metodo mostra hacia atras
    {
        var output = string.Empty;
        var current = _tail;
        while (current != null)
        {
            output += $"{current.Data} <=> ";
            current = current.Prev;
        }
        return output.Length > 0 ? output.Substring(0, output.Length - 5) : output;
    }

    public bool ElementExists(T data) // metodo existe elemento 
    {
        DoubleNode<T>? current = _head;
        while (current != null)
        {
            if (current.Data != null && current.Data.Equals(data))
            {
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    public void SortDescending()  // ordenar decendente 
    {
        if (_head == null || _head == _tail)
        {
            return; // La lista está vacía o tiene un solo elemento, ya está "ordenada"
        }

        bool swapped;
        do
        {
            swapped = false;
            DoubleNode<T>? current = _head;

            while (current?.Next != null)
            {
                if (current.Data != null && current.Next.Data != null && current.Data.CompareTo(current.Next.Data) < 0)
                {
                    // Intercambiar nodos, no solo los datos
                    DoubleNode<T> node1 = current;
                    DoubleNode<T> node2 = current.Next;

                    DoubleNode<T>? prev1 = node1.Prev;
                    DoubleNode<T>? next2 = node2.Next;

                    if (prev1 != null)
                    {
                        prev1.Next = node2;
                    }
                    else
                    {
                        _head = node2; // Si node1 era la cabeza, ahora node2 es la nueva cabeza
                    }

                    node2.Prev = prev1;
                    node2.Next = node1;
                    node1.Prev = node2;
                    node1.Next = next2;

                    if (next2 != null)
                    {
                        next2.Prev = node1;
                    }
                    else
                    {
                        _tail = node1; // Si node2 era la cola, ahora node1 es la nueva cola
                    }

                    swapped = true;
                }
                else
                {
                    current = current.Next;
                }
            }
        } while (swapped);
    }

    public void ShowFashion()  // metodo mostar moda
    {
        if (_head == null)
        {
            Console.WriteLine("La lista está vacía, no hay moda que mostrar.");
            return;
        }
        Dictionary<T, int> frecuencia = new Dictionary<T, int>();
        DoubleNode<T>? current = _head;
        while (current != null)
        {
            if (current.Data != null)
            {
                if (frecuencia.ContainsKey(current.Data))
                {
                    frecuencia[current.Data]++;
                }
                else
                {
                    frecuencia[current.Data] = 1;
                }
            }
            current = current.Next;
        }
        if (frecuencia.Count > 0)
        {
            int maxFrecuencia = frecuencia.Values.Max();
            var modas = frecuencia.Where(x => x.Value == maxFrecuencia).Select(x => x.Key).ToList();
            Console.WriteLine($"La(s) moda(s): {string.Join(", ", modas)}");
        }
        else
        {
            Console.WriteLine("La lista está vacía, no hay moda que mostrar.");
        }
    }

    public void ShowGraph()  // mostar grafico
    {
        if (_head == null)
        {
            Console.WriteLine("La lista está vacía, no hay gráfico para mostrar.");
            return;
        }

        Dictionary<T, int> frecuencia = new Dictionary<T, int>();
        DoubleNode<T>? current = _head;

        while (current != null)
        {
            if (current.Data != null)
            {
                if (frecuencia.ContainsKey(current.Data))
                {
                    frecuencia[current.Data]++;
                }
                else
                {
                    frecuencia[current.Data] = 1;
                }
            }
            current = current.Next;
        }

        if (frecuencia.Count == 0)
        {
            Console.WriteLine("No hay elementos en la lista para generar el gráfico.");
            return;
        }

        Console.WriteLine("\nGráfico de Frecuencia:");
        foreach (KeyValuePair<T, int> par in frecuencia.OrderBy(key => key.Key))
        {
            Console.WriteLine($"{par.Key}: {new string('*', par.Value)}");
        }
        Console.WriteLine();
    }

    public void DeleteOccurrence(T data)  // metodo elinar una ocurrecia 
    {
        DoubleNode<T>? current = _head;

        while (current != null)
        {
            if (current.Data != null && current.Data.Equals(data))
            {
                if (current == _head)
                {
                    _head = _head.Next;
                    if (_head != null)
                    {
                        _head.Prev = null;
                    }
                    if (_head == null)
                    {
                        _tail = null;
                    }
                }
                else if (current == _tail)
                {
                    _tail = _tail.Prev;
                    if (_tail != null)
                    {
                        _tail.Next = null;
                    }
                    if (_tail == null)
                    {
                        _head = null;
                    }
                }
                else
                {
                    if (current.Prev != null)
                    {
                        current.Prev.Next = current.Next;
                    }
                    if (current.Next != null)
                    {
                        current.Next.Prev = current.Prev;
                    }
                }
                return;
            }
            current = current.Next;
        }
        Console.WriteLine($"El elemento '{data}' no se encontró en la lista para eliminar.");
    }

    public void DeleteAllOccurrence(T data) // metodo eliminar todas las ocurrencias
    {
        DoubleNode<T>? current = _head;
        DoubleNode<T>? next;

        while (current != null)
        {
            next = current.Next;

            if (current.Data != null && current.Data.Equals(data))
            {
                if (current == _head)
                {
                    _head = _head.Next;
                    if (_head != null)
                    {
                        _head.Prev = null;
                    }
                    if (_head == null)
                    {
                        _tail = null;
                    }
                }
                else if (current == _tail)
                {
                    _tail = _tail.Prev;
                    if (_tail != null)
                    {
                        _tail.Next = null;
                    }
                    if (_tail == null)
                    {
                        _head = null;
                    }
                }
                else
                {
                    if (current.Prev != null)
                    {
                        current.Prev.Next = current.Next;
                    }
                    if (current.Next != null)
                    {
                        current.Next.Prev = current.Prev;
                    }
                }
            }
            current = next;
        }
        Console.WriteLine($"Se eliminaron todas las ocurrencias de '{data}' (si existían).");
    }

    public object ShowForwarde()
    {
        throw new NotImplementedException();
    }
}