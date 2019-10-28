using System;
using System.Collections.Generic;

namespace FactoryMethodDesignPattern
{
    public enum Actions
    {
        CourierUPS,
        CourierFedEx,
        CourierAramax
    }

    public interface ICourier
    {
        string WhoAmI();
    }

    public abstract class CourierBase : ICourier
    {
        //Etc
        public string MyName { get; set; }
        virtual public string WhoAmI()
        {
            return null;
        }
    }

    public class CourierUPS : CourierBase
    {
        override public string WhoAmI()
        {
            MyName = "I'm UPS";
            return MyName;
        }
    }

    public class CourierFedEx : CourierBase
    {
        override public string WhoAmI()
        {
            MyName = "I'm FedEx";
            return MyName;
        } 
         public void Send()
        {
           
        }
    }

    public class CourierAramax : CourierBase
    {
        override public string WhoAmI()
        {
            MyName = "I'm Aramax";
            return MyName;
        }
    }
    public interface ICourierFactory
    {
        ICourier CreateInstance(Actions enumModuleName);
    }
    //public class CourierFactory : ICourierFactory
    //{
    //    public CourierFactory()
    //    {

    //    }

    //    public static readonly IDictionary<Courier, Func<IShip>> Creators = new Dictionary<Courier, Func<IShip>>()
    //        {
    //        { Courier.UPS, () => new CourierUPS() },
    //        { Courier.FedEx, () => new CourierFedEx() },
    //        { Courier.Aramax, () => new CourierAramax() }
    //        };

    //    public IShip CreateInstance(Courier enumModuleName)
    //    {
    //        return Creators[enumModuleName]();
    //    }

    //}

    public class CourierFactory : ICourierFactory
    {
        public readonly IDictionary<Actions, ICourier> _factories;
        public CourierFactory()
        {
            _factories = new Dictionary<Actions, ICourier>();
            foreach (Actions action in Enum.GetValues(typeof(Actions)))
            {
                string objectToInstantiate = "FactoryMethodDesignPattern." + action + ", FactoryMethodDesignPattern";
                var objectType = Type.GetType(objectToInstantiate);
                ICourier factory = (ICourier)Activator.CreateInstance(objectType);
                _factories.Add(action, factory);
            }
        }
        public ICourier CreateInstance(Actions action)
        {
            return _factories[action];
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var CourierFactory = new CourierFactory();
            var Courier = CourierFactory.CreateInstance(Actions.CourierFedEx);
            Console.WriteLine(Courier.WhoAmI());
        }
    }
}