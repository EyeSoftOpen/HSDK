namespace EyeSoft.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ObjectEventsExtensions
    {
        private static readonly IDictionary<object, Dictionary<string, Delegate>> instancesEventsDictionary =
            new Dictionary<object, Dictionary<string, Delegate>>();

        public static TEventHandler Register<TEventHandler>(this IObjectExtender<object> obj, TEventHandler action)
        {
            var instance = obj.Instance;

            var eventType = typeof(TEventHandler);

            var eventName =
                instance
                    .GetType()
                    .GetEvents()
                    .Single(eventObj => eventObj.EventHandlerType == eventType).Name;

            if (!instancesEventsDictionary.ContainsKey(obj))
            {
                instancesEventsDictionary
                    .Add(instance, new Dictionary<string, Delegate>());
            }

            var eventDictionary = instancesEventsDictionary[instance];

            eventDictionary.Add(eventName, (Delegate)(object)action);

            return action;
        }

        public static void Unregister(this IObjectExtender<object> obj, string eventName)
        {
            var instance = obj.Instance;

            if (!instancesEventsDictionary.ContainsKey(instance))
            {
                return;
            }

            if (!instancesEventsDictionary[instance].ContainsKey(eventName))
            {
                return;
            }

            var type = instance.GetType();
            var eventObj = type.GetEvent(eventName);

            var message = $"The type {type.Name} does not contain an event with the specified name {eventName}.";

            var delegateObj = instancesEventsDictionary[instance][eventName];
            eventObj.RemoveEventHandler(instance, delegateObj);

            instancesEventsDictionary[instance].Remove(eventName);

            if (instancesEventsDictionary[instance].Count == 0)
            {
                instancesEventsDictionary.Remove(instance);
            }
        }

        public static bool HasHandlers(this IObjectExtender<object> obj)
        {
            return instancesEventsDictionary.ContainsKey(obj.Instance);
        }
    }
}