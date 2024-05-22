using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32_Console;

namespace CS_Windows_2
{
	internal static class Prime
	{
		public static void Calculate(object data)
		{
			Window w = (Window)data;
			int max = 1000000;
			int[] p = new int[100000];
			p[0] = 2;
			int n = 0;
			for (int i = 3; i < max; i += 2)
			{
				bool is_prime = true;
				for (int j = 0; j <= n; ++j)
				{
					if (i % p[j] == 0)
					{
						is_prime = false;
						break;
					}

				}
				if (is_prime)
				{
					n++;
					p[n] = i;
					w.PrintStr(i.ToString() + "  ");
					w.CopyAppBuffer();
					Thread.Sleep(1);
				};

			}
		}

	}
}
