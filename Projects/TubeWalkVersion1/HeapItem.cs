//DSA Group Coursework Version 1 - hard coded using an Adjacency Matrix 
//Samuel Paul Jones May 2023

// adapted from Week 8
// Tutorial Exercises: Heaps - HeapItem class
//Lecturer: Dr Paul Howells


using System;


namespace TubeWalkVersion1
{
    public class HeapItem
    {
        private int key;                //current distance to "value" tube station
        private TubeStation value;

        //minHeap acts as a Priority Queue of key-value pairs
        //where the value is a TubeStation vertex
        //and the key is the current shortest distance to that TubeStation vertex

        public HeapItem(int key, TubeStation value)
        {
            this.key = key;
            this.value = value;
        }
        public int GetKey()
        {
            return key;
        }
        public TubeStation GetValue()
        {
            return value;
        }


    }

}
