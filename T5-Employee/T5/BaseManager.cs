using System;
namespace T5
{
	public abstract class BaseManager
	{
		public BaseManager()
		{ 
		}
		public BaseManager(String name)
		{         
		}
        public string filePath;
        abstract public void AddNew();
		abstract public void Update();
		abstract public void Delete();
		abstract public void Find();
		abstract public void Show();
				
	}
}

