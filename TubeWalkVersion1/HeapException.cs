//adapted from Week 8 Tutorial Exercises: Heaps - HeapException class
//Used for heap exceptions, e.g. extracting from an empty heap
//Lecturer: Dr Paul Howells

//DSA Group Coursework Version 1 - hard coded using an Adjacency Matrix 
//Samuel Paul Jones May 2023

using System;


namespace TubeWalkVersion1
{
    public class HeapException : Exception
    {
        public HeapException(string message) : base(message) { }

    }
}
