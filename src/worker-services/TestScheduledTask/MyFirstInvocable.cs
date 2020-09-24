using Coravel.Invocable;
using System;
using System.Threading.Tasks;

namespace TestScheduledTask
{
    public class MyFirstInvocable : IInvocable
    {
        public Task Invoke()
        {
            Console.WriteLine("This is scheduled to run! Current time - " + DateTimeOffset.Now);
            return Task.CompletedTask;
        }
    }
}