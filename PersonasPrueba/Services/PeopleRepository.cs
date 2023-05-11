using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace PersonasPrueba.Services
{
	public class PeopleRepository
	{
		static string baseUrl = "http://appsdemo.logytechmobile.com/ServicioIntegracionControAcceso/api/Personas/Personas/5";

		public void GetPeople()
		{
			var request = (HttpWebRequest)WebRequest.Create(baseUrl);
			request.Method = "GET";
			request.ContentType = "application/json";
			request.Accept = "application/json";

			try
			{
				using (WebResponse response = request.GetResponse())
				{
					using (Stream strReader = response.GetResponseStream())
					{
						if (strReader == null) return;
						using (StreamReader objReader = new StreamReader(strReader))
						{
							string responseBody = objReader.ReadToEnd();
							// Do something with responseBody
							Console.WriteLine(responseBody);
						}
					}
				}
			}
			catch (WebException ex)
			{
				
			}
		} 
			
	}
}