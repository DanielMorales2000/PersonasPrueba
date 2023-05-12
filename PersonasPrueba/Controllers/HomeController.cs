using Newtonsoft.Json;
using PersonasPrueba.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PersonasPrueba.Controllers
{
	public class HomeController : Controller
	{
		static string baseUrl = "http://appsdemo.logytechmobile.com/ServicioIntegracionControAcceso/api";
		int peopleCount = 0;
		string filter = "";

		public async Task<ActionResult> Index()
		{
			IEnumerable<People> people = await GetPeople(await Login());

			if (filter != "")
			{
				people = people.Where(x => x.nombre.Contains(filter) == true || x.apellido.Contains(filter) == true).ToList();
			}
			if (peopleCount != 0)
			{
				people = people.Take(peopleCount).ToList();
			}
			return View(people);
		}

		[HttpPost]
		public void Filter(int quantity, string nameFilter )
		{
			peopleCount = quantity;
			filter = nameFilter;

			Index();
		}
			

		public async Task<IEnumerable<People>> GetPeople(string token)
		{
			var request = (HttpWebRequest)WebRequest.Create(baseUrl+ "/Personas/Personas/5");
			request.Headers.Add("Authorization", "Bearer "+token);
			request.Method = "GET";
			//request.ContentType = "application/json";
			request.Accept = "application/json";
			request.PreAuthenticate = true;

			IEnumerable<People> people;

			try
			{
				using (WebResponse response = request.GetResponse())
				{
					using (Stream strReader = response.GetResponseStream())
					{
						if (strReader == null) return null;
						using (StreamReader objReader = new StreamReader(strReader))
						{
							string responseBody = objReader.ReadToEnd();
							people = JsonConvert.DeserializeObject<IEnumerable<People>>(responseBody);
						}
					}
				}
			}
			catch (WebException ex)
			{
				return null;
			}

			return people;
		}

		public async Task<string> Login()
		{
			LoginDataModel data = new LoginDataModel();

			var request = (HttpWebRequest)WebRequest.Create(baseUrl+"/Cuentas/Login");
			request.Method = "POST";
			request.ContentType = "application/json";
			request.Accept = "application/json";

			var loginModel = new LoginModel() { usuario = "f2losada", password = "123456.LM" };
			var json = JsonConvert.SerializeObject(loginModel);

			using (var streamWriter = new StreamWriter(request.GetRequestStream()))
			{
				streamWriter.Write(json);
				streamWriter.Flush();
				streamWriter.Close();
			}

			try
			{
				using (WebResponse response = request.GetResponse())
				{
					using (Stream strReader = response.GetResponseStream())
					{
						if (strReader == null) return null;
						using (StreamReader objReader = new StreamReader(strReader))
						{
							string responseBody = objReader.ReadToEnd();
							data = JsonConvert.DeserializeObject<LoginDataModel>(responseBody);
						}
					}
				}
			}
			catch (WebException ex)
			{
				return null;
			}

			return data.token;
		}
	}
}

