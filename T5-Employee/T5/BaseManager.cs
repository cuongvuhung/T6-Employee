using System;
namespace T6
{
	public abstract class BaseManager
	{
		public BaseManager() { }
		public BaseManager(String name) { }
	    abstract public void AddNew();
		abstract public void Update();
		abstract public void Delete();
        abstract public void Find();
    }
}

