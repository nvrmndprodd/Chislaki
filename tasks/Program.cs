using tasks;
using static System.Console;

while(true)
{
    var command = ReadLine();
    
    if (command is not null && command != string.Empty)
    {
        if (command is "first")
        {
            var task = new FirstTask();
            task.Execute();
        }
        
        else if (command is "second")
        {
            var task = new SecondTask();
            task.Execute();
        }
        
        else if (command is "third")
        {
            var task = new ThirdTask();
            task.Execute();
        }
    }
}
