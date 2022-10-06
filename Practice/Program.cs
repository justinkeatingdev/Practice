using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq; 
using System.Runtime.InteropServices;

// C# API Coding Challenge Application Programming Interface
//  Please review the following specifications and complete the challenges below.
//  Because the API described below is not real, we are not expecting working code
//  but the expectation is that the code would work if the API did exist.

//Classes that inherit Node are stored in the database in a table call Nodes.
//The table columns are based on the property names within the classes and have matching names.
//The ClassName column is a special column that is automatically populated with the name of the 
//lowest specialization of the Node class.
//CalendarEvent is not the only specialization of the Node class.

//Using the above specifications, write asp.net C# code to accomplish the following tasks:
//-	Obtain a list of all upcoming CalendarEvents. The result is intended to be used in an
//	Upcoming Events widget. The widget will display, at most, 6 events.
//-	For the same Upcoming Events widget in the previous task, obtain a list of upcoming Music events.
//-	Produce a serialized list of CalendarEvents
 // xml / json serialized list export 

namespace Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            int resultsLimit = 6; 

            NodeQuery nodeQuery = new NodeQuery("Nodes"); //creates instant reference for object being created 
  
            nodeQuery.SelectNodes(); //need to run method to  create table, I used obj that was created to run method

            Console.WriteLine("Table: " + nodeQuery.TableName + "\n"); //prints out table name

            List<Node> calendarEvents = nodeQuery.Where("CalendarEvent").Nodes.ToList(); //takes enumeration of nodes and turns to list of nodes
            for (int i = 0; i < calendarEvents.Count() && i < resultsLimit; i++) //.where method check to check for column name to see if it is CalendarEvent 
            {
                    Node node = calendarEvents[i]; //so it can index make it a list so make it a ToList 
                    
                    Console.WriteLine(i + ". " + node.ClassName);

                    Console.WriteLine((node as CalendarEvent).StartDate); // as type CalendarEvet Type so able to use properties/contructors/all 
                    Console.WriteLine((node as CalendarEvent).EndDate);
                    Console.WriteLine((node as CalendarEvent).EventType + "\n");
                    
                
            }

            Console.WriteLine("\n" + "TEST FOR MUSIC ONLY EVENT" + "\n");

            List<Node> calendarEvents2 = nodeQuery.Nodes.ToList();
            for (int i = 0; i < calendarEvents2.Count() ; i++)
            {
                Node node = calendarEvents2[i]; //so it can index make it a list so make it a ToList 


                if ((node as CalendarEvent).EventType == CalendarEventType.Music) //needed tmake sure that it was beign comapred to an enum
                {
                    Console.WriteLine( i+ ". "  + node.ClassName);
                    Console.WriteLine((node as CalendarEvent).StartDate); // as type CalendarEvet Type so able to use properties/contructors/all 
                    Console.WriteLine((node as CalendarEvent).EndDate);
                    Console.WriteLine((node as CalendarEvent).EventType + "\n");
                }

            }


        }
    }


    // Class*
    public class NodeQuery
    {
        // Property
        public string TableName //created to store table name 
        {
            get;
            protected set;
        }



        // Constructor
        public NodeQuery(string tableName)
        {
            TableName = tableName;
        }


        //Enumerable * 
        public IEnumerable<Node> Nodes // type <Node> //Ienumerable is a collection of elemnets > array 
        {
            get;
            protected set;
        }



        // Method*
        public NodeQuery SelectNodes()
        {
           
            // select * from TableName;
            // for each row in the above select statement,
            // create a new node object and add it to Nodes

            // Table: Nodes
            //  ====================================================================
            // | ClassName       | StartDate    | EndDate           | EventType    |
            //  ====================================================================
            // | CalendarEvent   | Today        | 5 days from now   | Music        |
            // | CalendarEvent   | Tomorrow     | 3 days from tmrw  | Sports       |
            //  ====================================================================

            // Sets the results to the Nodes property. this is a list "Nodes" type Node
            // anything of type node will be in list 
            //restricting listg to be all type node 

            //sql data reader , while there are lines in reader , implement calendarEvent contructor 

            Nodes = new List<Node>()
            {
                new CalendarEvent(DateTime.Now, DateTime.Now.AddDays(5), CalendarEventType.Music), //1 //NEW OBJECT THRU CEVANTS WITH PARAMETERS
                new CalendarEvent(DateTime.Now.AddDays(3), DateTime.Now.AddDays(5), CalendarEventType.Sports), //2
                new CalendarEvent(DateTime.Now.AddDays(3), DateTime.Now.AddDays(5), CalendarEventType.Art), //3
                new CalendarEvent(DateTime.Now.AddDays(3), DateTime.Now.AddDays(5), CalendarEventType.Music), //4
                new CalendarEvent(DateTime.Now.AddDays(3), DateTime.Now.AddDays(5), CalendarEventType.Sports), //5
                new CalendarEvent(DateTime.Now.AddDays(3), DateTime.Now.AddDays(5), CalendarEventType.Art), //6 last one 
                new CalendarEvent(DateTime.Now.AddDays(3), DateTime.Now.AddDays(5), CalendarEventType.Music) //7
            }.AsEnumerable(); //return the input an an enumerbale to be set back to Node 

            
            return this;
        }
        // Returns a NodeQuery object which contains all content nodes in the system

        // Method*
        public NodeQuery Where(string sqlWhereCondition)
        {
            // select * from TableName where sqlWhereCondition;
            // for each row in the above select statement
            // create a new node object and add it to Nodes

            if (Nodes != null) //and data at address 
            {
                // SQL WHERE condition in C#
                //results is a REFRENCE / variable 
                // .Where is function of IEnumberables ( returns ienumberable ) for loop witghing all nodes 
                //created local node 
                //lambda inline functgions 

                

                // Set the results to the Nodes property.
                 
                // steps tghrough each node (row) itgself .linq 

                Nodes = Nodes.Where(node => node.ClassName == sqlWhereCondition); //classname is inside of Class Node 
            }
            //rights side is get, left side is set
            // https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.where?view=netcore-3.1#code-try-1


            return this;
        }

        // returns a NodeQuery object with nodes filtered by sqlWhereCondition

        // Property reference allows tgo refernce outside of class NodeQuery
        //multiple access 
        

    }

    // Class*
    public class Node //class / type (reference types 
    {
        // Property*
        public string ClassName
        {
            get;
            set;
        }


        // Constructor
        public Node(string className)
        {
            ClassName = className;
        }
    }


    // Enum*
    public enum CalendarEventType
    {
        Sports,
        Music,
        Art
    }

    // Class*
    public class CalendarEvent : Node //Node is parent Calendar is child ( derived from ) 
    {
        // Property
        public DateTime StartDate 
        { 
            get;
            set; 
        }

        // Property
        public DateTime EndDate
        {
            get;
            set; 
        }
        // Property

        public CalendarEventType EventType //type of property "CalendarEventType 
        { 
            get;
            set; 
        }


        // Constructor
        public CalendarEvent(DateTime startDate, DateTime endDate, CalendarEventType eventType) : base("CalendarEvent") //paramters aqauired contructor chaining
        {
            StartDate = startDate;
            EndDate = endDate;
            EventType = eventType;
        }
    }

    
}