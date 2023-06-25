using System;
namespace T6
{
	public abstract class BaseManager
	{
		public BaseManager() { }
		abstract public void AddNew();
		abstract public void Update();
		abstract public void Delete();
        abstract public void Find();
		abstract public void PrintList(Employee[] arr);
		abstract public void PrintToFile(string filePath);
    }
}

