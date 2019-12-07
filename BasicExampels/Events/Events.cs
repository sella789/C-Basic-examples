using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicExampels
{
    public class Events
    {
        // Lets start with delegate

        // This is how we declare a delegate
        // The BasicDelegate is used to referance a method
        // that returns nothing (void) and gets no parameters
        public delegate void BasicDelegate();

        // Here we create a data member for the class that
        // can conatin a referance to a method that answers the
        // BasicDelegate conditions the mehod must return nothing
        // and must not get any parameters
        public BasicDelegate EmptyMethod;

        public void InvokeEmotyMethod()
        {
            EmptyMethod?.Invoke();
        }

        // Lets declare a new delgate that gets paramets
        public delegate void DelegateThatGetsIntParameter(int intger);

        // Now we will declare a data member for the delegate
        public DelegateThatGetsIntParameter MethodThatGetsIntParameter;

        // We will delacre another delegate that returns an integer
        public delegate int DelegateThatReturnsAnInteger();

        // Create a DM for it
        public DelegateThatReturnsAnInteger MethodTahtReturnsAnInteger;

        // Well lets say i want to invoke a few functions at once
        // Not just one and unlimeted amount how will i do that
        // Delegate is a type which means it can be used in data structurs
        // like arrays or lists so lets declare a delegate list
        // we will use BasicDelegate for this example
        public List<BasicDelegate> BasicDelegateFunctions = new List<BasicDelegate>();

        // Now in method examples we will see how we add to the list

        // We will now declare an event using the BasicDelegate
        public event BasicDelegate BasicDelegateEvent;

        // go now to MethodesExamples to see how to use it

        public void BasicDelegateEventInvoker()
        {
            BasicDelegateEvent?.Invoke();
        }

        // More ways to declare events

        // Action is a built in delegate in C# that is the equal of our basic delegate
        public event Action LikeBasicDelegate;

        // Action is generic evrything you put in the <> will be a prameter
        public event Action<int> LikeDelegateThatGetsIntParameter;

        // While we can make an event for function with return values WE WILL NEVER DO SO
        // for two main resons ONE its not logical on an oop basis to do so
        // TWO it has unexpected results (feel free to play with it)
    }

    public class MethodesExamples
    {
        Events events;

        public MethodesExamples()
        {
            // init events;
            events = new Events();

            // Here we can see that we can put MyEmptyMethod in to the
            // Empty method variable in events becuse MyEmptyMethod is
            // a function that returns nothing (void) and dosent have
            // parameters
            events.EmptyMethod = MyEmptyMethod;

            // Here we invoke the data member in events
            // That has a referance to our EmptyMethod
            // we use ? to check if it null if it is not
            // null then we invoke
            // This is equal to calling EmptyMetod()
            events.EmptyMethod?.Invoke();

            // Equals to events.EmptyMethod?.Invoke(); becuse
            // events.EmptyMethod points to MyEmptyMethod;
            MyEmptyMethod();

            // Or we can use InvokeEmotyMethod in Events
            events.InvokeEmotyMethod();

            // As we can see all of the ways work

            events.MethodThatGetsIntParameter = MultiplyAnIntegerByMinusOne;

            // Here we are invoking the second delegate data member and as we can see
            // it works (all of the ways we showed before works as well)
            events.MethodThatGetsIntParameter?.Invoke(3);

            // We wiil now check the delgeate that returns an integer
            events.MethodTahtReturnsAnInteger = ReturnOne;

            var result = events.MethodTahtReturnsAnInteger?.Invoke();

            Console.WriteLine(result);

            // As we can see it all works a delegate is just a contract of how the
            // method signature is suppused to look like

            // Now we have added the three MyEmptyMethods to the list
            events.BasicDelegateFunctions.Add(MyEmptyMethod);
            events.BasicDelegateFunctions.Add(MyEmptyMethodTwo);
            events.BasicDelegateFunctions.Add(MyEmptyMethodThree);

            // The delegate is in a list so we cant just invoke them like
            // we did before we need to iterate them

            foreach(var function in events.BasicDelegateFunctions)
            {
                function?.Invoke();
            }

            // The we invoked them now works but it has its drawbacks
            // The major one being that if one of the methoeds takes
            // while to comlpete the next one wont be invoked until
            // it is completed so how can we improve that

            Parallel.ForEach(events.BasicDelegateFunctions, (function) => function?.Invoke());

            // This is a parallel for each that means that evry function
            // will be executed asynchronaly and synchrunaly

            // This is what an event is behind the scenes

            // lets go throgh how to declere an event and how to use it *return to the Events class

            // This is how we add a function to an event
            events.BasicDelegateEvent += MyEmptyMethod;

            // if you will try now to invoke the event from MethodesExamples class
            // you will see it can't be done this is becuse only the class
            // contaning the event can invoke it

            // event has a method in his api that invokes the event so we can call it
            // And if you will runc the program it will work
            events.BasicDelegateEventInvoker();
        }

        public void MyEmptyMethod()
        {
            Console.WriteLine("MyEmptyMethod was called");
        }

        public void MyEmptyMethodTwo()
        {
            Console.WriteLine("MyEmptyMethodTwo was called");
        }

        public void MyEmptyMethodThree()
        {
            Console.WriteLine("MyEmptyMethodThree was called");
        }

        public void MultiplyAnIntegerByMinusOne(int toMultiply)
        {
            int result = -1 * toMultiply;
            Console.WriteLine(result);
        }

        public int ReturnOne()
        {
            return 1;
        }
    }
}
