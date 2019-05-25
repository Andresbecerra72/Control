namespace Control.Common.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Models;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    //esta clase conecta el backend API con el Frontend
    public class ApiService   ////////////****pendiente verificar el servicio API ** verificar codigo
    {
        //metodo asyn (asincrono) es comunicacion con el api
        public async Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller)
        {                                           //urlBase es la direccion del API htps://controlweb.azurewebsites.net
            try
            {
                var client = new HttpClient  
                {
                    BaseAddress = new Uri(urlBase)
                };
                var url = $"{servicePrefix}{controller}"; //concatena el prefijo con el controlardor //api/Passanger
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)//pregunta si logro la comunicacion
                {
                    return new Response
                    {
                        IsSuccess = false,//cuando no se logro la comunicacion
                        Message = result,
                    };
                }
                //aqui es cuando funciona la comunicacion .. y se recube el json
                var list = JsonConvert.DeserializeObject<List<T>>(result); //convierte el string en un Json
                return new Response
                {
                    IsSuccess = true,
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}