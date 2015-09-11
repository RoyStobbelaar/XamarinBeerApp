using System;

namespace BierApp
{
	public class Beer
	{
		public string name{ get; set; }
		public int rating{ get; set; }
		public bool approval{ get; set; }
		public string imagePath{ get; set; }

		public Beer(){}

		public Beer (string name)
		{
			this.name = name;
		}
	}
}