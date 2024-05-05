namespace Prim
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Prime p = new Prime();
			p.prime();
		}
	}

	internal class Prime
	{
		int maxPrime = 1000;
		int[] p = new int[1000];

		public void prime()
		{
			p[0] = 2;
			int n = 0;
			for (int i = 3; i < maxPrime; i += 2)
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
					Console.Write($"{i}\t");
				};

			}
		}
	}
}
