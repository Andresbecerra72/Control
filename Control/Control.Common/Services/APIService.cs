namespace Control.Common.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Models;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using System.Text;

    //esta clase conecta el backend API con el Frontend
    public class ApiService   
    {
                                   
        //metodo asyn (asincrono) es comunicacion con el api, se consume sin seguridad
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
   
            //usa token para obtener lista, este metodo se consume con seguridad
            public async Task<Response> GetListAsync<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken)
            {
                try
                {
                    var client = new HttpClient
                    {
                        BaseAddress = new Uri(urlBase),
                    };

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);

                    var url = $"{servicePrefix}{controller}";
                    var response = await client.GetAsync(url);
                    var result = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = result,
                        };
                    }

                    var list = JsonConvert.DeserializeObject<List<T>>(result);
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


            //este metodo permite obtener el token
            public async Task<Response> GetTokenAsync(
                string urlBase,
                string servicePrefix,
                string controller,
                TokenRequest request)
            {
                try
                {
                    var requestString = JsonConvert.SerializeObject(request);
                    var content = new StringContent(requestString, Encoding.UTF8, "application/json");
                    var client = new HttpClient
                    {
                        BaseAddress = new Uri(urlBase)
                    };

                    var url = $"{servicePrefix}{controller}";
                    var response = await client.PostAsync(url, content);
                    var result = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = result,
                        };
                    }

                    var token = JsonConvert.DeserializeObject<TokenResponse>(result);
                    return new Response
                    {
                        IsSuccess = true,
                        Result = token
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

        //POST PASSANGER metodo para crear un pasajero por APP movil
        public async Task<Response> PostAsync<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            T model,
            string tokenType,
            string accessToken)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                var url = $"{servicePrefix}{controller}";
                var response = await client.PostAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                var obj = JsonConvert.DeserializeObject<T>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = obj,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        //PUT PASSANGER metodo para editar 
        public async Task<Response> PutAsync<T>(
    string urlBase,
    string servicePrefix,
    string controller,
    int id,
    T model,
    string tokenType,
    string accessToken)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                var url = $"{servicePrefix}{controller}/{id}";
                var response = await client.PutAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                var obj = JsonConvert.DeserializeObject<T>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = obj,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        //DELETE metodo para eliminar registro de pasajeros
        public async Task<Response> DeleteAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            int id,
            string tokenType,
            string accessToken)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                var url = $"{servicePrefix}{controller}/{id}";
                var response = await client.DeleteAsync(url);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                return new Response
                {
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }



    }


}