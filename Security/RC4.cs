using System;

namespace Security
{
	public class RC4
	{
		private int [] s;
		private int [] keep;
		private int i, j;

		public RC4()
		{
			s = new int[256];
			keep = new int[256];
			RC4ini("pwd");
		}
		public byte EnDeCryptSingle(byte plainbyte)
		{
			int temp=0, k=0 ;
			byte cipherby;

			i = (i+1)%256;
			j = (j+s[i])%256;
			
			//' Swap( S(i),S(j) )
			temp = s[i];
			s[i] = s[j];
			s[j] = temp;

			//'Generate Keybyte k
			k = s[(s[i]+s[j])%256];

			//'Plaintextbyte xor Keybyte
			byte bk = (byte) k;
			cipherby = (byte) (plainbyte ^ bk);
			return cipherby;
		}

		private void RC4ini(string Pwd)
		{
			int temp, a, b=0;
			
			//'Save Password in Byte-Array
			for(a=0; a<=255; a++)
			{
				if(b>=Pwd.Length) 
					b=0;

				keep[a]= System.Convert.ToInt32(System.Convert.ToChar(Pwd.Substring(b,1)));
				b++;
			}

			//'INI S-Box
			for (a=0; a<=255; a++)
				s[a] = a;
			
			b=0;
			for (a=0; a<=255; a++)
			{
				b = (b + s[a] + keep[a]) % 256;
				//' Swap( S(i),S(j) )
				temp = s[a];
				s[a] = s[b];
				s[b] = temp;
			}		
		}
	}
}
